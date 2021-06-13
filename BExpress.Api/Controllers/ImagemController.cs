using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace BExpress.Api.Controllers
{
    [Route("api/[controller]")]
    public class ImagemController : PadraoController
    {
        private readonly CloudStorageAccount _storageAccount;

        public ImagemController(IConfiguration configuration)
        {
            _storageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("AzureStorage"));
        }

        [HttpGet]
        [Route("{imagemName}")]
        public string ObterImagem(string imagemName)
        {
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            //Caso não exista, ele cria
            container.CreateIfNotExistsAsync();

            //Setar permissão de acesso para 'público'
            container.SetPermissionsAsync(
                new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
            );

            //Recupera os arquivos de um container
            var arquivo = container.GetBlockBlobReference(imagemName).Uri.AbsoluteUri;
            return arquivo;
        }
    }
}
