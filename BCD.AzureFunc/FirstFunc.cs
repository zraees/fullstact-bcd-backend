using BCD.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BCD.AzureFunc;

public class FirstFunc
{
    private readonly IUserService _userService;
    private readonly ILogger<FirstFunc> _logger;

    public FirstFunc(IUserService userService, ILogger<FirstFunc> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [Function("FirstFunc")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        var users = await _userService.GetUsersAsync().ConfigureAwait(false);

        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions! " + users?.Count());
    }
}
