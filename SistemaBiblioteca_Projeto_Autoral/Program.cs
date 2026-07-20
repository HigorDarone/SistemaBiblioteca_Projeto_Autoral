

using SistemaBiblioteca_Projeto_Autoral.Models;

Livro MeuLivro = new Livro("O Senhor dos Anéis", "J.R.R. Tolkien", "HarperCollins", "Fantasia", 49.90m);

Console.WriteLine("{0} - {1}", MeuLivro.Nome, MeuLivro.Autor);
Console.WriteLine("Editora: {0}", MeuLivro.Editora);
Console.WriteLine("Gênero: {0}", MeuLivro.Genero);
Console.WriteLine("Preço: R$ {0:F2}", MeuLivro.Preco);
Console.WriteLine("Disponivel: {0}", MeuLivro.Disponivel);


Usuario MeuUsuario = new Usuario("", "123.456.789-00", "joao.silva@email.com", "senha123");
Console.WriteLine("{0} - {1}", MeuUsuario.Nome, MeuUsuario.Documento);
Console.WriteLine("Email: {0}", MeuUsuario.Email);
Console.WriteLine("Senha: {0}", MeuUsuario.Senha);