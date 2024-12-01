// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BCD.Domain.Entities;
using BCD.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace BCD.Infrastructure.Repository;
public class BusinessRepository : GenericRepository<Business>, IBusinessRepository
{
    protected readonly BCDDbContext _context;
    protected readonly DbSet<Business> _dbSet;
    private readonly BlobContainerClient _containerClient;

    public BusinessRepository(BCDDbContext context, IConfiguration configuration) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Business>();

        var connectionString = configuration["AzureBlobStorage:ConnectionString"];
        var containerName = configuration["AzureBlobStorage:ContainerName"];
        _containerClient = new BlobContainerClient(connectionString, containerName);
        _containerClient.CreateIfNotExists(PublicAccessType.Blob);
    }

    public async Task AddAsync(Business business)
    {
        // Save Images to Azure Blob Storage and Get URLs
        if (business.BusinessPhotos != null && business.BusinessPhotos.Count > 0)
        {
            foreach (var image in business.BusinessPhotos)
            {
                var blobClient = _containerClient.GetBlobClient(Guid.NewGuid() + Path.GetExtension(image.FileName));
                await blobClient.UploadAsync(image.stream, new BlobHttpHeaders { ContentType = image.ContentType }).ConfigureAwait(false);
                image.Url = blobClient.Uri.ToString();
                //imageUrls.Add(blobClient.Uri.ToString());
            }
        }

        await _dbSet.AddAsync(business);
    }
}
