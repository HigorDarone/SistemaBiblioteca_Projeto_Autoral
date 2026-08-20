using Microsoft.AspNetCore.Mvc;
using SistemaBiblioteca_Projeto_Autoral.Models;

namespace SistemaBiblioteca_WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly Catalogo catalogo;

        public LivrosController(Catalogo catalogo)
        {
            this.catalogo = catalogo;
        }

        [HttpGet("Listar-Livros")]
        public ActionResult<List<Livro>> Livros()
        {
            var livros = catalogo.ListarLivros();
            return Ok(livros);
        }

        [HttpGet("Buscar-Por-Nome")]
        public ActionResult<List<Livro>> BucarPoNome(string nome)
        {
            var livro = catalogo.BuscarPorNome(nome);         
            return Ok(livro);
        }

        [HttpGet("Buscar-Por-Genero")]

        public ActionResult<List<Livro>> BuscarPorGenero(string nomegenero)
        {
            var genero = catalogo.BuscarPorGenero(nomegenero);
            return Ok(genero);
        }

        [HttpGet("Listar-Livro-Por-Id")]

        public ActionResult<Livro> BuscarPorId(int id)
        {
            var resultado = catalogo.BuscarPorId(id);
            if(resultado != null)
            {
                return Ok(resultado);
            }
            return NotFound();
        }

        [HttpPost("Adicionar-Livro")]
        public ActionResult<string> AdicionarLivro(Livro livro)
        {
            var resultado = catalogo.AdicionarLivro(livro);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> RemoverLivro(int id)
        {
            var resultado = catalogo.RemoverLivro(id);
            return Ok(resultado);
        }
    }
}       
