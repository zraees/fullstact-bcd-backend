using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;

namespace BCD.AzureFunc;

public class GetBlogs
{
    private readonly ILogger<GetBlogs> _logger;
    private readonly string _containerName = "data-2";
    private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=bcdblobstorageaccount;AccountKey=Ii58Glr9EgXr2XYcpANcHfOnVcBhashyOsi0GDGh/wKDa/+tQ6BAaexel6IkqZO757QTLZme8HkH+ASttAkLUg==;EndpointSuffix=core.windows.net";
   
    public GetBlogs(ILogger<GetBlogs> logger)
    {
        _logger = logger;
    }

    [Function("GetBlogs")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        BlobContainerClient blobContainerClient = new BlobContainerClient(_connectionString, _containerName);
        var blobs = blobContainerClient.GetBlobs();

        foreach (var item in blobs)
        {
            Console.WriteLine("Blob Name is " + item.Name);
        }

        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Total blobs fetched " + blobs?.Count());
    }
}
