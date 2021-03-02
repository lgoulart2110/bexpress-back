using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace BExpress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagemController : ControllerBase
    {
        public readonly string path;

        public ImagemController()
        {
            path = $"{Directory.GetCurrentDirectory()}/Imagens";
        }

        [HttpGet]
        [Route("{imagemName}")]
        public IActionResult ObterImagem(string imagemName)
        {
            var imagePath = Path.Combine(path, imagemName);
            var imagem = System.IO.File.OpenRead(imagePath);
            return File(imagem, "image/jpeg");
        }
    }
}
