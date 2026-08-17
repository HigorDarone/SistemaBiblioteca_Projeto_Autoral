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
            return context.Usuarios.FirstOrDefault(user => user.Email.Equals(email) && user.Senha.Equals(senha));
        }   

        public string AdicionarUsuario(Usuario usuario)
        {
            if (context.Usuarios.Any(user => user.Documento.Equals(usuario.Documento) || user.Email.Equals(usuario.Email)))
            {
                return $"Usuário com documento '{usuario.Documento}' ou email '{usuario.Email}' já existe.";
            }
            context.Usuarios.Add(usuario);
            context.SaveChanges();
            return $"Usuário '{usuario.Nome}' adicionado com sucesso.";
        }
    }
}
