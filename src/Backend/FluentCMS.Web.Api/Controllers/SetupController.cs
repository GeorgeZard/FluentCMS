﻿using FluentCMS.Services.Models;

namespace FluentCMS.Web.Api.Controllers;

public class SetupController(ISetupService setupService, IMapper mapper, IAccountService accountService) : BaseGlobalController
{
    public const string AREA = "Setup Management";
    public const string READ = "Read";
    public const string CREATE = "Create";

    [HttpGet]
    [Policy(AREA, READ)]
    public async Task<IApiResult<bool>> IsInitialized(CancellationToken cancellationToken = default)
    {
        return Ok(await setupService.IsInitialized(cancellationToken));
    }

    [HttpPost]
    [Policy(AREA, CREATE)]
    public async Task<IApiResult<bool>> Start(SetupRequest request)
    {
        await accountService.ValidateUserName(request.Username);
        await accountService.ValidatePassword(request.Password);
        var setupTemplate = mapper.Map<SetupTemplate>(request);
        return Ok(await setupService.Start(setupTemplate));
    }

    [HttpGet]
    [Policy(AREA, READ)]
    public async Task<IApiPagingResult<string>> GetTemplates(CancellationToken cancellationToken = default)
    {
        var templates = await setupService.GetTemplates(cancellationToken);
        return OkPaged(templates);
    }
}
