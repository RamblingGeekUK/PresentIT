using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PresentIT.Controllers
{
    public class DemoController : Controller
    {
        private readonly IConfiguration _configuration;
        public DemoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] IFormFile blob)
        {

            //MemoryStream mem = new MemoryStream();

            //string systemFileName = files.FileName;
            string blobstorageconnection = _configuration.GetValue<string>("blobstorage");
            // Retrieve storage account from connection string.
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
            // Create the blob client.
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("filescontainers");
            // This also does not make a service call; it only creates a local object.
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(SpecialFileName);

            await using (var data = blob.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(data);
            }
            return View("Create");
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
