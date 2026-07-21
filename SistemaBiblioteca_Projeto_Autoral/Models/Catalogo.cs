using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SistemaBiblioteca_Projeto_Autoral.Models
{
    public class Catalogo
    {
        private List<Livro> livros = new List<Livro>();

        public string AdicionarLivro(Livro livro)
        {
            foreach (var item in livros)
                if (item.Nome.Equals(livro.Nome, StringComparison.OrdinalIgnoreCase) && item.Autor.Equals(livro.Autor, StringComparison.OrdinalIgnoreCase))
                {
                    return $"Livro '{livro.Nome}' já existe no catálogo.";
                }

            livros.Add(livro);
            return $"Id '{livro.Id}' Livro '{livro.Nome}' do Autor '{livro.Autor}' adicionado ao catálogo.";
        }

        public string RemoverLivro(int id)
        {
            foreach (var livro in livros)
            {
                if (livro.Id == id)
                {
                    livros.Remove(livro);
                    return $"Livro com ID {id} removido do catálogo.";
                }
            }
            return $"Livro com ID {id} não encontrado no catálogo.";
        }

        public List<Livro> BuscarPorNome(string nome)
        {
            List<Livro> resultados = new List<Livro>();

            foreach (var livro in livros)
            {
                if (livro.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                {
                    resultados.Add(livro);
                    //Console.WriteLine($"Livro encontrado: {livro.Id} - {livro.Nome} - {livro.Autor}");
                }

            }

            return resultados;
        }

        public List<Livro> BuscarPorGenero(string genero)
        {
            List<Livro> resultados = new List<Livro>();

            foreach (var livro in livros)
            {
                if (livro.Genero.Contains(genero, StringComparison.OrdinalIgnoreCase))
                {
                    resultados.Add(livro);
                    //Console.WriteLine($"Livros do gênero '{genero}': {livro.Id} - {livro.Nome} - {livro.Autor}");
                }

            }

            return resultados;
        }


    }
}
