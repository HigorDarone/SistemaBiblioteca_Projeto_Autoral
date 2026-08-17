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
         
        public string AdicionarLivro(Livro livro)
        {

            if(context.Livros.Any(livros => livros.Nome.Equals(livro.Nome) && livros.Autor.Equals(livro.Autor)))
            {
                return $"Livro '{livro.Nome}' já existe no catálogo.";
            }
             
            context.Livros.Add(livro);
            context.SaveChanges();
                 
            return $"Id '{livro.Id}' Livro '{livro.Nome}' do Autor '{livro.Autor}' adicionado ao catálogo.";
        }


        public string RemoverLivro(int id)
        {
            Livro buscarlivroporId = BuscarPorId(id);

            if (buscarlivroporId == null)
            {
                return $"Livro com ID {id} não encontrado no catálogo.";
            }
            
            context.Livros.Remove(buscarlivroporId);
            context.SaveChanges();
            return $"Livro com ID {id} removido do catálogo.";
        }

        public List<Livro> BuscarPorNome(string nome)
        {
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
      
            return context.Livros
                .Where(livro => livro.Id == id)
                .FirstOrDefault();

        }
    }
}
