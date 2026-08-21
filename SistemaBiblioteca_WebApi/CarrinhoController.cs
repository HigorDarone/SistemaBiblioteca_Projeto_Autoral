using Microsoft.AspNetCore.Mvc;
using SistemaBiblioteca_Projeto_Autoral.Data;
using SistemaBiblioteca_Projeto_Autoral.Models;
using SistemaBiblioteca_WebApi.DTO;

namespace SistemaBiblioteca_WebApi
{


    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly GerenciadorUsuarios gerenciadorUsuarios;

        private readonly AppDbContext appDbContext;

        private readonly Catalogo catalogo;

        public CarrinhoController(GerenciadorUsuarios gerenciadorUsuarios, AppDbContext appDbContext, Catalogo catalogo)
        {
            this.gerenciadorUsuarios = gerenciadorUsuarios;
            this.appDbContext = appDbContext;
            this.catalogo = catalogo;
        }

        private Carrinho BuscarOuCriarCarrinho(int usuarioId)
        {
            var usuarioLogado = gerenciadorUsuarios.BuscarUsuarioPorId(usuarioId);
            if (usuarioLogado == null)
            {
                return null;
            }
            var carrinho = appDbContext.Carrinhos.FirstOrDefault(c => c.UsuarioLogado.Id == usuarioLogado.Id);
            if (carrinho == null)
            {
                carrinho = new Carrinho(usuarioLogado, appDbContext);
            }
            return carrinho;
        }


        [HttpPost("{idUser}/Adicionar-Item-Carrinho")]
        public IActionResult AdicionarItemCarrinho(int idUser, DTO.ItemCarrinhoRequest itemCarrinhoRequest)
        {
            Carrinho carrinho = BuscarOuCriarCarrinho(idUser);
            if (carrinho == null)
            {
                return NotFound("Carrinho ou Usuário não encontrado.");
            }

            Livro resultadolivroId = catalogo.BuscarPorId(itemCarrinhoRequest.LivroId);

            if (resultadolivroId == null)
            {
                return NotFound("Livro não encontrado.");
            }

            var itemCarrinho = new ItemCarrinho(resultadolivroId, itemCarrinhoRequest.Quantidade);
            carrinho.AdicionarItemCarrinho(itemCarrinho);
            return Ok("Item adicionado ao carrinho com sucesso.");
        }

        [HttpGet("{idUser}/Listar_Itens_Carrinho")]
        public ActionResult<List<ItemCarrinho>> ListarItensCarrinho(int idUser)
        {
            Carrinho carrinho = BuscarOuCriarCarrinho(idUser);
            if (carrinho == null)
            {
                return NotFound("Carrinho ou Usuário não encontrado.");
            }

            List<ItemCarrinho> resultado = carrinho.ListarItensCarrinho();

            var resultadoListaCarrinho = new DTO.ListarCarrinhoResponse(resultado, carrinho.CalcularTotal());
          
            return Ok(resultadoListaCarrinho);
        }


        [HttpDelete("{idUser}/Remover-Item-Carrinho/{idLivro}")]

        public ActionResult<string> RemoverItemCarrinho(int idUser, int idLivro)
        {
            Carrinho carrinho = BuscarOuCriarCarrinho(idUser);
            if (carrinho == null)
            {
                return NotFound("Carrinho ou Usuário não encontrado.");
            }

            string resultado = carrinho.RemoverItemCarrinho(idLivro);

            return Ok(resultado);
        }


        [HttpPatch("{idUser}/Atualizar-Quantidade-Item-Carrinho/{idLivro}")]
        public ActionResult<string> RemoverQuantidadeItemCarrinho(int idUser, int idLivro)
        {
            Carrinho carrinho = BuscarOuCriarCarrinho(idUser);
            if (carrinho == null)
            {
                return NotFound("Carrinho ou Usuário não encontrado.");
            }

            string resultado = carrinho.RemoverQuantidadeItemCarrinho(idLivro);

            return Ok(resultado);
        }

        [HttpPost("{idUser}/Finalizar-Pedido")]
        public ActionResult<Pedido> FinalizarPedido(int idUser)
        {
            Carrinho carrinho = BuscarOuCriarCarrinho(idUser);
            if (carrinho == null)
            {
                return NotFound("Carrinho ou Usuário não encontrado.");
            }
            try
            { 
            Pedido resultado = carrinho.FinalizarCarrinho();
            return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
