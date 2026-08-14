using SistemaBiblioteca_Projeto_Autoral.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBiblioteca_Projeto_Autoral.Models
{
    public class Usuario
    {
        // propriedade com private set para que só possa ser alterada de dentro da classe, e não de fora da classe
        public int Id { get; private set; }

        private string nome;
        public string Nome 
        { get { return nome;  } 
          set {  nome = Validador.ValidarTexto(value, "Nome"); }
        }

        private string documento;
        public string Documento 
        { get { return documento; }
          private set { documento = Validador.ValidarTexto(value, "Documento"); }
        }

        private string email;
        public string Email 
        { get { return email; }
          private set { email = Validador.ValidarTexto(value, "Email"); }
        }

        private string senha;
        public string Senha 
        { get { return senha; }
          private set { senha = Validador.ValidarTexto(value, "Senha"); }
        }

        public bool EhAdministrador { get; private set; }

        public Usuario (string nome, string documento, string email, string senha)
        {
            Nome = nome;
            Documento = documento;
            Email = email;
            Senha = senha;
        }
    }
}
