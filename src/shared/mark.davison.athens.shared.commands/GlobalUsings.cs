﻿global using mark.davison.athens.shared.commands.Scenarios.CreateTaskInstance;
global using mark.davison.athens.shared.models.dtos.Scenarios.Commands.CreateTaskInstance;
global using mark.davison.athens.shared.models.Entities;
global using mark.davison.athens.shared.utilities.EntityExtensions;
global using mark.davison.athens.shared.validation;
global using mark.davison.athens.shared.validation.Entities;
global using mark.davison.common.persistence.EntityDefaulter;
global using mark.davison.common.server.abstractions.Authentication;
global using mark.davison.common.server.abstractions.Repository;
global using mark.davison.common.server.CQRS;
global using mark.davison.common.server.CQRS.Processors;
global using mark.davison.common.server.CQRS.Validators;
global using Microsoft.Extensions.DependencyInjection;
global using System.Diagnostics.CodeAnalysis;
