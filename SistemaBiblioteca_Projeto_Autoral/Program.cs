

using SistemaBiblioteca_Projeto_Autoral.Models;
using System.Xml.XPath;


/*Usuario usuario1 = new Usuario("João Silva", "12345678900", "senha1", "joao.silva@email.com");

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
*/


Catalogo catalogo = new Catalogo();

Usuario usuarioLogado = null;

Carrinho carrinhoUsuario = null;


bool continuar = true;

while (continuar)
{
    Console.WriteLine("Bem-vindo ao Sistema de Biblioteca!");
    Console.WriteLine("1. Cadastrar usuário");
    Console.WriteLine("2. Catalogo De livros");
    Console.WriteLine("3. Adicionar item ao carrinho");
    Console.WriteLine("4. Remover item do carrinho");
    Console.WriteLine("5. Finalizar pedido");
    Console.WriteLine("6. Sair");
    string opcao = Console.ReadLine();
    switch (opcao)
    {
        case "1":
            Console.Write("Digite o nome do usuário: ");
            string nomeUsuario = Console.ReadLine();
         
            Console.Write("Digite o Documento do usuário: ");
            string documento = Console.ReadLine();

            Console.Write("Digite o email do usuário: ");
            string email = Console.ReadLine();

            Console.Write("Digite a senha do usuário: ");
            string senhaUsuario = Console.ReadLine();

            usuarioLogado = new Usuario(nomeUsuario, documento, email, senhaUsuario);

            break;
       

        case "2":
            Console.WriteLine("Escolha uma opção de busca:");
            Console.WriteLine("1. Buscar por nome");
            Console.WriteLine("2. Buscar por gênero");
            Console.WriteLine("3. Adicionar livro ao catálogo");
            Console.WriteLine("4. Remover livro do catálogo");
            string opcaoCatalogo = Console.ReadLine();
            switch (opcaoCatalogo)
            {
                case "1":
                    Console.WriteLine("Digite o nome do livro que deseja buscar:");
                    string nomeBusca = Console.ReadLine();
                    List<Livro> livrosEncontradosNome = catalogo.BuscarPorNome(nomeBusca);
                    foreach (var l in livrosEncontradosNome)
                    {
                        Console.WriteLine($"- {l.Nome} | Autor: {l.Autor} | Editora: {l.Editora} | Preço: {l.Preco}");
                    }
                    break;

                case "2":
                    Console.WriteLine("Digite o Gênero que deseja buscar:");

                    string generoBusca = Console.ReadLine();

                    List<Livro> livrosEncontrados = catalogo.BuscarPorGenero(generoBusca);

                    foreach (var l in livrosEncontrados)
                    {
                        Console.WriteLine($"- {l.Nome} | Autor: {l.Autor} | Editora: {l.Editora} | Preço: {l.Preco}");
                    }
                    break;

                case "3":
                    Console.WriteLine("Digite o nome do livro que deseja adicionar ao catálogo:");
                    string nomeAdicionar = Console.ReadLine();
                    Console.WriteLine("Digite o nome do autor do livro:");
                    string autorAdicionar = Console.ReadLine();
                    Console.WriteLine("Digite a editora do livro:");
                    string editoraAdicionar = Console.ReadLine();
                    Console.WriteLine("Digite o gênero do livro:");
                    string generoAdicionar = Console.ReadLine();
                    Console.WriteLine("Digite o preço do livro:");
                    decimal precoAdicionar = decimal.Parse(Console.ReadLine());

                    Livro livroParaAdicionar = new Livro(nomeAdicionar, autorAdicionar, editoraAdicionar, generoAdicionar, precoAdicionar);
                    catalogo.AdicionarLivro(livroParaAdicionar);
                    Console.WriteLine("Livro adicionado ao catálogo com sucesso!");
                    break;

                case "4":
                    Console.WriteLine("Digite o ID do livro que deseja remover do catálogo:");
                    int IdRemover = int.Parse(Console.ReadLine());
                    catalogo.RemoverLivro(IdRemover);
                    Console.WriteLine("Livro removido do catálogo com sucesso!");
                    break;
            }
            break;
        case "3":
            if (usuarioLogado == null)
            {
                Console.WriteLine("Você precisa estar logado para acessar o catálogo.");
                break;

            }
            Console.WriteLine("Digite o ID do livro que deseja adicionar ao carrinho:");
            int idLivroAdicionar = int.Parse(Console.ReadLine());
 
            Livro resultadoIdlivro = catalogo.BuscarPorId(idLivroAdicionar);

            Console.WriteLine("Digite a quantidade que deseja adicionar ao carrinho:");
            int quantidade = int.Parse(Console.ReadLine());

            ItemCarrinho itemCarrinho = new ItemCarrinho(resultadoIdlivro, quantidade);

            carrinhoUsuario.AdicionarItemCarrinho(itemCarrinho);


            break;
        case "4":
            // Lógica para remover item do carrinho
            break;
        case "5":
            // Lógica para finalizar pedido
            break;
        case "6":
            continuar = false;
            break;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}