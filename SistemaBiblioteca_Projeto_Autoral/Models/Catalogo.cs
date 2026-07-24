using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SistemaBiblioteca_Projeto_Autoral.Models
{
    public class Catalogo
    {

        // iniciando a lista de livros como uma lista vazia para evitar null 
        private List<Livro> livros = new List<Livro>();

        private int proximoId = 1;

        public string AdicionarLivro(Livro livro)
        {

            //Equals para comparar o nome e o autor do livro, Sensivel a maiúsculas e minúsculas,
            //StringComparison.OrdinalIgnoreCase permite comparar strings ignorando maiúsculas e minúsculas
            foreach (var item in livros)
                if (item.Nome.Equals(livro.Nome, StringComparison.OrdinalIgnoreCase) && item.Autor.Equals(livro.Autor, StringComparison.OrdinalIgnoreCase))
                {
                    return $"Livro '{livro.Nome}' já existe no catálogo.";
                }


            // criando o Id em memória para o livro, e incrementando o próximo Id para o próximo livro a ser adicionado
            livro.DefinirId(proximoId);
            proximoId++;

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

            //função para buscar livros por nome, ignorando maiúsculas e minúsculas, e retornando uma lista de livros encontrados

            //iniciando um nova lista para salvar os resultados da busca, para não alterar a lista original de livros
            List<Livro> resultados = new List<Livro>();

            foreach (var livro in livros)
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
