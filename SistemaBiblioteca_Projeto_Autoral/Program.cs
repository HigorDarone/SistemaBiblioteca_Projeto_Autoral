

using SistemaBiblioteca_Projeto_Autoral.Models;
using System.Xml.XPath;


Usuario usuario1 = new Usuario("João Silva", "12345678900", "senha1", "joao.silva@email.com");

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

Console.WriteLine("----------------------------------");

List<ItemCarrinho> carrinho = new List<ItemCarrinho>();

carrinho.Add(new ItemCarrinho(livro1, 1));
carrinho.Add(new ItemCarrinho(livro3, 2));
carrinho.Add(new ItemCarrinho(livro5, 1));


foreach (var item in carrinho)
{
    Console.WriteLine($"Item no carrinho: {item.Livro.Nome} - Quantidade: {item.Quantidade}");
}



Console.WriteLine("--------------ITEM CARRINHO--------------------");


Carrinho carrinhoUsuario = new Carrinho(usuario1);

carrinhoUsuario.AdicionarItemCarrinho(carrinho[0]);
carrinhoUsuario.AdicionarItemCarrinho(carrinho[1]);
carrinhoUsuario.AdicionarItemCarrinho(carrinho[2]);

foreach (var item in carrinhoUsuario.ItensCarrinho)
{
    Console.WriteLine($"Item no carrinho do usuário:{item.Livro.Id} - {item.Livro.Nome} - Quantidade: {item.Quantidade}");
}


carrinhoUsuario.RemoverQuantidadeItemCarrinho(2);

Console.WriteLine("----------------------------------");

foreach (var item in carrinhoUsuario.ItensCarrinho)
{
    Console.WriteLine($"Item no carrinho do usuário:{item.Livro.Id} - {item.Livro.Nome} - Quantidade: {item.Quantidade}");
}


Console.WriteLine("--------------ITEM FINALIZADO--------------------");

Pedido pedidoFinalizado = carrinhoUsuario.FinalizarCarrinho();

Console.WriteLine($"Pedido de: {pedidoFinalizado.UsuarioLogado.Nome}");
Console.WriteLine($"Data: {pedidoFinalizado.DataPedido}");
Console.WriteLine($"Status: {pedidoFinalizado.Status}");
Console.WriteLine($"Total: {pedidoFinalizado.TotalPedido}");

foreach (var item in pedidoFinalizado.ItensPedido)
{
    Console.WriteLine($"- {item.Livro.Nome} | Quantidade: {item.Quantidade} | Preço unitário: {item.PrecoUnitario}");

}

Console.WriteLine("--------------ITEM CARRINHO--------------------");
foreach (var item in carrinhoUsuario.ItensCarrinho)
{
    Console.WriteLine($"Item no carrinho do usuário:{item.Livro.Id} - {item.Livro.Nome} - Quantidade: {item.Quantidade}");
}
