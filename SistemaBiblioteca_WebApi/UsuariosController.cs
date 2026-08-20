using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SistemaBiblioteca_Projeto_Autoral.Models;
using SistemaBiblioteca_WebApi.DTO;

namespace SistemaBiblioteca_WebApi
{

    [ApiController]
    [Route("api/[controller]")]

    public class UsuariosController : ControllerBase
        {
        private readonly GerenciadorUsuarios gerenciadorUsuarios;

        DTO.CadastroRequest cadastroRequest;
        public UsuariosController(GerenciadorUsuarios gerenciadorUsuarios)
        {
            this.gerenciadorUsuarios = gerenciadorUsuarios;
        }

        [HttpPost("Cadastrar-Usuario")]

        public ActionResult<string> AdicionarUsuario(DTO.CadastroRequest cadastroRequest)
        {      
            try
            {
                var usuarioCadastro = gerenciadorUsuarios.AdicionarUsuario(new Usuario(cadastroRequest.Nome, cadastroRequest.Documento, cadastroRequest.Email, cadastroRequest.Senha));
                return Ok(usuarioCadastro);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch(InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            ;

           
        }

        [HttpPost("Login")]
        
        public ActionResult<Usuario> Login(DTO.LoginRequest loginRequest)
        {
            var usuarioLogar = gerenciadorUsuarios.Login(loginRequest.Email, loginRequest.Senha);
            if(usuarioLogar != null)
            {
                return Ok(usuarioLogar);
            }

            return Unauthorized();

            
        }


    }

    
}