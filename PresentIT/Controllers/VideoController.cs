using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PresentIT.Controllers
{
    public class VideoController : Controller
    {
        private readonly IConfiguration _configuration;
        public VideoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile blob)
        {

            //MemoryStream mem = new MemoryStream();

            //string systemFileName = files.FileName;
            string blobstorageconnection = _configuration.GetValue<string>("blobstorage");
            // Retrieve storage account from connection string.
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
            // Create the blob client.
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("videos");
            // This also does not make a service call; it only creates a local object.
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(SpecialFileName);

            await using (var data = blob.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(data);
            }
            
            return Ok(blockBlob.StorageUri.PrimaryUri.ToString());
        }

        static string SpecialFileName
        {
            get
            { 
                return string.Format($"{DateTime.Now:yyyy-MM-dd_hh-mm-ss-tt}.webm");
            }
        }
    }
}
