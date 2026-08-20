using SistemaBiblioteca_Projeto_Autoral.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SistemaBiblioteca_Projeto_Autoral.Models
{
    public class GerenciadorUsuarios
    {
        private AppDbContext context;

        public GerenciadorUsuarios(AppDbContext context)
        {
            this.context = context;
        }

        public Usuario Login(string email, string senha)
        {
            Usuario UsuarioEncontrado = context.Usuarios.FirstOrDefault(user => user.Email.Equals(email));

            if (UsuarioEncontrado != null)
           {
               if(BCrypt.Net.BCrypt.Verify(senha, UsuarioEncontrado.Senha) == true)
               {
                   return UsuarioEncontrado;
               }
           }
            return null;
        }   

        public string AdicionarUsuario(Usuario usuario)
        {
            if (context.Usuarios.Any(user => user.Documento.Equals(usuario.Documento) || user.Email.Equals(usuario.Email)))
            {
                throw new ArgumentException($"Usuário com documento '{usuario.Documento}' ou email '{usuario.Email}' já existe.");
            }
            context.Usuarios.Add(usuario);
            context.SaveChanges();
            return $"Usuário '{usuario.Nome}' adicionado com sucesso.";
        }
    }
}
