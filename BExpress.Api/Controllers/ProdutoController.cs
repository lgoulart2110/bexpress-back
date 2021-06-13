using BExpress.Infra.Entidades;
using BExpress.Infra.Entidades.Dtos;
using BExpress.Infra.Paginacao;
using BExpress.Infra.Servicos.Interfaces;
using BExpress.Infra.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BExpress.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : PadraoController
    {
        private readonly IProdutoService _produtoService;
        private readonly CloudStorageAccount _storageAccount;

        public ProdutoController(IProdutoService produtoService, IConfiguration configuration)
        {
            _produtoService = produtoService;
            _storageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("AzureStorage"));
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public IActionResult ObterProduto(int id)
        {
            try
            {
                var produto = _produtoService.Obter(id);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("produtos")]
        public IActionResult ObterProdutos(int pagina, int quantidadePagina, int? categoriaId = null)
        {
            try
            {
                var produtos = _produtoService.ObterProdutos(categoriaId);
                var paginacao = Paginar<Produto>.Pagine(produtos, pagina, quantidadePagina);
                return Ok(
                    new RetornoPaginacaoDto(
                        paginacao.TotalPaginas,
                        paginacao.QuantidadeTotal,
                        paginacao.Pagina,
                        paginacao.Dados
                    )
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(string nome, int categoriaId, string descricao, string preco, int quantidadeEstoque, IFormFile imagem)
        {
            try
            {
                if (descricao == "undefined") descricao = null;
                var nomeImagem = await SalvarImagemAsync(imagem);
                var produto = new Produto
                {
                    Ativo = true,
                    CategoriaId = categoriaId,
                    DataCadastro = DateTime.Now,
                    Descricao = descricao,
                    Nome = nome,
                    Imagem = nomeImagem,
                    Preco = Formatadores.FormataRealParaDecimal(preco),
                    QuantidadeEstoque = quantidadeEstoque
                };
                _produtoService.Adicionar(produto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        [HttpPut]
        public IActionResult AlterarProduto([FromBody]ProdutoDto produto)
        {
            try
            {
                _produtoService.Alterar(produto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = Constantes.ADMINISTRADOR)]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletarProduto(int id)
        {
            try
            {
                _produtoService.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<string> SalvarImagemAsync(IFormFile imagem)
        {
            if (imagem is null) return "padrao.png";

            var extencao = Path.GetExtension(imagem.FileName);

            var extencoesPermitidas = new List<string>
            {
                ".jpg",
                ".jpeg",
                ".png"
            };

            if (!extencoesPermitidas.Contains(extencao))
                throw new Exception("Imagem não permitida.");

            var novoNome = $"{Guid.NewGuid()}{extencao}";

            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(novoNome);

            Image image = Image.FromStream(imagem.OpenReadStream(), true, true);
            var newImage = new Bitmap(600, 400);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, 600, 400);
            }

            var stream = new MemoryStream();
            newImage.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;

            await blockBlob.UploadFromStreamAsync(stream);

            return novoNome;
        }
    }
}
