using SistemaBiblioteca_Projeto_Autoral.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBiblioteca_Projeto_Autoral.Models
{
    public class Livro
    {


        // Propriedades que posso acessar e alterar de fora da classe 
        public int Id { get; private set; }

        private string nome;
        public string Nome 
        { get { return nome; }
            set { nome = Validador.ValidarTexto(value, "Nome"); }
        
        }

        private string autor;
        public string Autor 
        { get { return autor; }
          set { autor = Validador.ValidarTexto(value, "Autor"); }
        }

        private string editora;
        public string Editora 
        { get { return editora; }
           set { editora = Validador.ValidarTexto(value, "Editora"); }
        }

        private string genero;
        public string Genero 
        { get{ return genero; }
          set { genero = Validador.ValidarTexto(value, "Genero"); }
        }

        // Propriedade escondida do preço, que só pode ser acessada e alterada de dentro da classe
        private decimal preco;

        // Propriedade pública para acessar e alterar o preço, com validação para não permitir valores negativos
        public decimal Preco
        {
            get { return preco; }
            set { preco = Validador.ValidarNumeroDecimal(value, "Preco"); }
        }

        //Colocando private set para que a propriedade só possa ser alterada de dentro da classe, assim podendo mudar apenas quando o
        // livro for para um pedido ou quando for devolvido, e não de fora da classe
        public bool Disponivel { get; private set; }
  

        public Livro(string nome, string autor, string editora, string genero, decimal preco)
        {
            Nome = nome;
            Autor = autor;
            Editora = editora;
            Genero = genero;
            Preco = preco;
            Disponivel = true; // Inicialmente o livro está disponível
        }

       
    }
}
