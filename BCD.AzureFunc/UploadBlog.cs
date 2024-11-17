using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BCD.AzureFunc;

public class UploadBlog
{
    private readonly ILogger<UploadBlog> _logger;
    private readonly string _containerName = "data-2";
    private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=bcdblobstorageaccount;AccountKey=Ii58Glr9EgXr2XYcpANcHfOnVcBhashyOsi0GDGh/wKDa/+tQ6BAaexel6IkqZO757QTLZme8HkH+ASttAkLUg==;EndpointSuffix=core.windows.net";
    private readonly string _blobName = "blob-test";

    public UploadBlog(ILogger<UploadBlog> logger)
    {
        _logger = logger;
    }

    [Function("UploadBlog")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        BlobContainerClient blobContainerClient = new BlobContainerClient(_connectionString, _containerName);
        //blobContainerClient.Create( (Azure.Storage.Blobs.Models.PublicAccessType.Blob).ConfigureAwait(false);
        BlobClient blobClient = blobContainerClient.GetBlobClient(_blobName);
        
        BlobContentInfo blobContentInfo = await blobClient.UploadAsync("D:\\blob-test.txt", true).ConfigureAwait(false);

        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Blob created successfully! ==> " + blobContentInfo?.BlobSequenceNumber);
    }
}
