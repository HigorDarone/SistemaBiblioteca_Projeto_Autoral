using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using SistemaBiblioteca_Projeto_Autoral.Data;


namespace SistemaBiblioteca_Projeto_Autoral.Models
{
    public class Catalogo
    {

        private AppDbContext context;

        public Catalogo(AppDbContext context)
        { 
          this.context = context;
        }

        // iniciando a lista de livros como uma lista vazia para evitar null 
        //private List<Livro> livros = new List<Livro>();

        

     
        public string AdicionarLivro(Livro livro)
        {

            if(context.Livros.Any(livros => livros.Nome.Equals(livro.Nome) && livros.Autor.Equals(livro.Autor)))
            {
                return $"Livro '{livro.Nome}' já existe no catálogo.";
            }
             
            context.Livros.Add(livro);
            context.SaveChanges();
          
            // criando o Id em memória para o livro, e incrementando o próximo Id para o próximo livro a ser adicionado
            
            return $"Id '{livro.Id}' Livro '{livro.Nome}' do Autor '{livro.Autor}' adicionado ao catálogo.";
        }

        public string RemoverLivro(int id)
        {
            foreach (var livro in context.Livros)
            {
                if (livro.Id == id)
                {
                    context.Livros.Remove(livro);
                    context.SaveChanges();
                    return $"Livro com ID {id} removido do catálogo.";
                }
            }
            return $"Livro com ID {id} não encontrado no catálogo.";
        }

        public List<Livro> BuscarPorNome(string nome)
        {

            /* //função para buscar livros por nome, ignorando maiúsculas e minúsculas, e retornando uma lista de livros encontrados

             //iniciando um nova lista para salvar os resultados da busca, para não alterar a lista original de livros
             List<Livro> resultados = new List<Livro>();

             foreach (var livro in context.Livros)
             {

                 //contains para encontra o termo em qualquer posição do nome, por exemplo ,
                 //se o nome do livro for "O Senhor dos Anéis", e o usuário buscar por "Senhor", o livro será encontrado
                 if (livro.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                 {
                     resultados.Add(livro);
                     //Console.WriteLine($"Livro encontrado: {livro.Id} - {livro.Nome} - {livro.Autor}");
                 }
            
             }

             return resultados;
            */
            return context.Livros
                .Where(livro => livro.Nome.Contains(nome))
                .ToList();

        }

        public List<Livro> BuscarPorGenero(string genero)
        {
           

            return context.Livros
                .Where(livro => livro.Genero.Contains(genero))
                .ToList();
        }

        public List<Livro> ListarLivros()
        {
            return context.Livros.ToList();
        }

        public Livro BuscarPorId(int id)
        {
            /* Livro resultado = null;
             if (id > 0)
             {
                 foreach (var livro in context.Livros)
                 {
                     if (livro.Id == id)
                     {
                         resultado = livro;

                     }
                 }

             }
             return resultado;
             */

            return context.Livros
                .Where(livro => livro.Id)
                .ToList();

        }
    }
}
