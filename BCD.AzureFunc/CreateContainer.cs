using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BCD.AzureFunc;

public class CreateContainer
{
    private readonly ILogger<CreateContainer> _logger;
    private readonly string _containerName = "data-2";
    private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=bcdblobstorageaccount;AccountKey=Ii58Glr9EgXr2XYcpANcHfOnVcBhashyOsi0GDGh/wKDa/+tQ6BAaexel6IkqZO757QTLZme8HkH+ASttAkLUg==;EndpointSuffix=core.windows.net";

    public CreateContainer(ILogger<CreateContainer> logger)
    {
        _logger = logger;
    }

    [Function("CreateContainer")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        var blobContainerClient = new BlobContainerClient(_connectionString, _containerName);

        blobContainerClient.Create();

        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}
