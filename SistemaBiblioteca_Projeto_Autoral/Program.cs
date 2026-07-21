

using SistemaBiblioteca_Projeto_Autoral.Models;
using System.Xml.XPath;

Livro MeuLivro = new Livro( "O Senhor dos Anéis", "J.R.R. Tolkien", "HarperCollins", "Fantasia", 49.90m);


Livro livro1 = new Livro("Era Uma Vez Um Boi", "Autor A", "Editora X", "Infantil", 29.90m);
Livro livro2 = new Livro("Era Uma Vez Um Sapo", "Autor B", "Editora X", "Infantil", 24.90m);

Livro livro3 = new Livro("Dom Casmurro", "Machado de Assis", "Editora Y", "Romance", 39.90m);
Livro livro4 = new Livro("Dom Quixote", "Miguel de Cervantes", "Editora Y", "Romance", 59.90m);

Livro livro5 = new Livro("O Senhor dos Anéis 5", "J.R.R. Tolkien", "Editora Z", "Fantasia", 89.90m);
Livro livro6 = new Livro("O Senhor das Moscas", "William Golding", "Editora Z", "Ficção", 44.90m);




Catalogo catalogo = new Catalogo();

catalogo.AdicionarLivro(MeuLivro);
catalogo.AdicionarLivro(livro1);
catalogo.AdicionarLivro(livro2);
catalogo.AdicionarLivro(livro3);
catalogo.AdicionarLivro(livro4);
catalogo.AdicionarLivro(livro5);
catalogo.AdicionarLivro(livro6);


List<Livro> resultadosNome = catalogo.BuscarPorGenero("");
foreach (var livro in resultadosNome)
{
    Console.WriteLine($"Livro encontrado: {livro.Id} - {livro.Nome} - {livro.Autor}");
}

catalogo.RemoverLivro(1);
Console.WriteLine("----------------------------------");
List<Livro> resultadosAtualizados = catalogo.BuscarPorGenero("");
foreach (var livro in resultadosAtualizados)
{
    Console.WriteLine($"Livro encontrado: {livro.Id} - {livro.Nome} - {livro.Autor}");
}