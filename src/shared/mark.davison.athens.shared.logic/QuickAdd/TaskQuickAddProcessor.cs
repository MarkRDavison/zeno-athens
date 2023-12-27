namespace mark.davison.athens.shared.logic.QuickAdd;

public static class TaskQuickAddProcessor
{
    public const char ProjectIdentifier = '+';

    public static CreateTaskDto ResolveTaskQuickAddCommand(string command)
    {
        var dto = new CreateTaskDto
        {
            Valid = true
        };

        var tokens = command.Split(
            " ",
            StringSplitOptions.RemoveEmptyEntries |
            StringSplitOptions.TrimEntries);

        var currentScope = QuickAddScopes.Task;

        var scopedTokens = new Dictionary<QuickAddScopes, List<string>> {
            { QuickAddScopes.Task, new() },
            { QuickAddScopes.Project, new() },
        };

        foreach (string token in tokens)
        {
            string currentToken = token;
            if (currentToken.StartsWith(ProjectIdentifier))
            {
                currentScope = QuickAddScopes.Project;
                currentToken = currentToken.Substring(1);
            }

            scopedTokens[currentScope].Add(currentToken);
        }

        dto.Name = string.Join(" ", scopedTokens[QuickAddScopes.Task]);
        dto.ProjectName = string.Join(" ", scopedTokens[QuickAddScopes.Project]);

        if (string.IsNullOrEmpty(dto.Name))
        {
            dto.Valid = false;
        }

        return dto;
    }
}
