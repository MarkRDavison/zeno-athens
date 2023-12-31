﻿namespace mark.davison.athens.bff.common.Authentication;

public class AthensCustomZenoAuthenticationActions : ICustomZenoAuthenticationActions
{
    private readonly IHttpRepository _httpRepository;
    private readonly IDateService _dateService;
    private readonly IOptions<AppSettings> _appSettings;

    public AthensCustomZenoAuthenticationActions(
        IHttpRepository httpRepository,
        IDateService dateService,
        IOptions<AppSettings> appSettings
    )
    {
        _httpRepository = httpRepository;
        _dateService = dateService;
        _appSettings = appSettings;
    }

    private Task<User?> GetUser(Guid sub, CancellationToken cancellationToken)
    {
        return _httpRepository.GetEntityAsync<User>(
               new QueryParameters { { nameof(User.Sub), sub.ToString() } },
               HeaderParameters.None,
               cancellationToken);
    }

    private async Task UpsertUserOptions(User user, string token, CancellationToken cancellationToken)
    {
        await _httpRepository.UpsertEntityAsync(
                new UserOptions
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id
                },
                HeaderParameters.Auth(token, user),
                cancellationToken);
    }
    private Task<User?> UpsertUser(UserProfile userProfile, string token, CancellationToken cancellationToken)
    {
        return _httpRepository.UpsertEntityAsync(
                new User
                {
                    Id = Guid.NewGuid(),
                    Sub = userProfile.sub,
                    Admin = false,
                    Created = _dateService.Now,
                    Email = userProfile.email!,
                    First = userProfile.given_name!,
                    Last = userProfile.family_name!,
                    LastModified = _dateService.Now,
                    Username = userProfile.preferred_username!
                },
                HeaderParameters.Auth(token, null),
                cancellationToken);
    }

    public async Task<User?> OnUserAuthenticated(UserProfile userProfile, IZenoAuthenticationSession zenoAuthenticationSession, CancellationToken cancellationToken)
    {
        var token = zenoAuthenticationSession.GetString(ZenoAuthenticationConstants.SessionNames.AccessToken);
        var user = await GetUser(userProfile.sub, cancellationToken);

        if (user == null && !string.IsNullOrEmpty(token))
        {
            user = await UpsertUser(userProfile, token, cancellationToken);

            if (user != null)
            {
                await UpsertUserOptions(user, token, cancellationToken);
            }

            if (!_appSettings.Value.PRODUCTION_MODE && user != null)
            {
                await UpsertTestData(user, token, cancellationToken);
            }
        }

        if (user != null)
        {
            zenoAuthenticationSession.SetString(ZenoAuthenticationConstants.SessionNames.User, JsonSerializer.Serialize(user));
        }

        return user;
    }

    private async Task UpsertTestData(User user, string token, CancellationToken cancellationToken)
    {
        var headers = HeaderParameters.Auth(token, user);

        var defaultProjectId = Guid.NewGuid();

        await _httpRepository.UpsertEntityAsync(
                new Project
                {
                    Id = defaultProjectId,
                    Name = "Default",
                    UserId = user.Id,
                    Colour = "#FF0000"
                },
                headers,
                cancellationToken);

        await _httpRepository.UpsertEntityAsync(
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Project #1",
                    UserId = user.Id,
                    Colour = "#00FF00"
                },
                headers,
                cancellationToken);

        await _httpRepository.UpsertEntityAsync(
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Project #2",
                    UserId = user.Id,
                    Colour = ""
                },
                headers,
                cancellationToken);

        await _httpRepository.UpsertEntityAsync(
                new Project
                {
                    Id = Guid.NewGuid(),
                    Name = "Project #3",
                    UserId = user.Id,
                    Colour = "#0000FF"
                },
                headers,
                cancellationToken);

        await _httpRepository.UpsertEntityAsync(
                new TaskInstance
                {
                    Id = Guid.NewGuid(),
                    Title = "Task #1",
                    ProjectId = defaultProjectId,
                    UserId = user.Id
                },
                headers,
                cancellationToken);

        await _httpRepository.UpsertEntityAsync(
                new TaskInstance
                {
                    Id = Guid.NewGuid(),
                    Title = "Task #2",
                    ProjectId = defaultProjectId,
                    UserId = user.Id,
                    IsFavourite = true
                },
                headers,
                cancellationToken);

        await _httpRepository.UpsertEntityAsync(
                new TaskInstance
                {
                    Id = Guid.NewGuid(),
                    Title = "Task #3",
                    ProjectId = defaultProjectId,
                    UserId = user.Id,
                    IsCompleted = true
                },
                headers,
                cancellationToken);
    }
}
