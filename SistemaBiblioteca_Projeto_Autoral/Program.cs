

using SistemaBiblioteca_Projeto_Autoral.Data;
using SistemaBiblioteca_Projeto_Autoral.Models;
using System.Xml.XPath;

AppDbContext appDbContext = new AppDbContext();

Catalogo catalogo = new Catalogo(appDbContext);

Usuario usuarioLogado = null;

Carrinho carrinhoUsuario = null;



bool continuar = true;

while (continuar)
{
    Console.WriteLine("Bem-vindo ao Sistema de Biblioteca!");
    Console.WriteLine("1. Cadastrar usuário");
    Console.WriteLine("2. Catalogo De livros");
    Console.WriteLine("3. Adicionar item ao carrinho");
    Console.WriteLine("4. Listar itens do carrinho");
    Console.WriteLine("5. Remover item do carrinho");
    Console.WriteLine("6. Finalizar pedido");
    Console.WriteLine("7. Sair");
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

            carrinhoUsuario = new Carrinho(usuarioLogado);

            Console.Write($"Usuário {usuarioLogado.Nome} cadastrado com sucesso!");

            break;


        case "2":
            Console.WriteLine("Escolha uma opção de busca:");
            Console.WriteLine("1. Buscar por nome");
            Console.WriteLine("2. Buscar por gênero");
            Console.WriteLine("3. Adicionar livro ao catálogo");
            Console.WriteLine("4. Remover livro do catálogo");
            Console.WriteLine("5. Listar todos os livros");
            Console.WriteLine("6. voltar");
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

                    if (decimal.TryParse(Console.ReadLine(), out decimal precoAdicionar))
                    {
                        Livro livroParaAdicionar = new Livro(nomeAdicionar, autorAdicionar, editoraAdicionar, generoAdicionar, precoAdicionar);

                        string livroadicionado = catalogo.AdicionarLivro(livroParaAdicionar);

                        Console.WriteLine(livroadicionado);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Digite um numero valido");
                        break;
                    }

                case "4":
                    Console.WriteLine("Digite o ID do livro que deseja remover do catálogo:");
                    Console.WriteLine("Digite 7 para sair:");

                    if (int.TryParse(Console.ReadLine(), out int IdRemover))
                    {
                        catalogo.RemoverLivro(IdRemover);
                        Console.WriteLine("Livro removido do catálogo com sucesso!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Apenas numero sao aceitos");
                        break;
                    }

                case "5":
                    Console.WriteLine("Lista de todos os livros no catálogo:");

                    List<Livro> todosOsLivros = catalogo.ListarLivros();

                    foreach (var l in todosOsLivros)
                    {
                        Console.WriteLine($"- ID: {l.Id} | Nome: {l.Nome} | Autor: {l.Autor} | Editora: {l.Editora} | Preço: {l.Preco}");
                    }
                    break;

                case "6":
                    break;
            }
            break;
        case "3":

            if (VerificarLogin() == false)
            {
                break;
            }
            else
            {
                Console.WriteLine("livros no catálogo:");

                List<Livro> todosOsLivros = catalogo.ListarLivros();

                foreach (var l in todosOsLivros)
                {
                    Console.WriteLine($"- ID: {l.Id} | Nome: {l.Nome} | Autor: {l.Autor} | Editora: {l.Editora} | Preço: {l.Preco}");
                }


                Console.WriteLine("Digite o ID do livro que deseja adicionar ao carrinho:");


                if (int.TryParse(Console.ReadLine(), out int idlivroAdicionar))
                {


                    Livro resultadoIdlivro = catalogo.BuscarPorId(idlivroAdicionar);

                    if (resultadoIdlivro == null)
                    {
                        Console.WriteLine("Livro não encontrado no catálogo.");
                        break;
                    }

                    Console.WriteLine("Digite a quantidade que deseja adicionar ao carrinho:");
                    if (int.TryParse(Console.ReadLine(), out int quantidade))
                    {
                        ItemCarrinho itemCarrinho = new ItemCarrinho(resultadoIdlivro, quantidade);

                        carrinhoUsuario.AdicionarItemCarrinho(itemCarrinho);
                    }
                    else
                    {
                        Console.WriteLine("Digite um Numero valido\n");
                        break;
                    }
                }
                else
                {
                    Console.Write("Digite um Numero valido\n");

                    break;
                }
                break;
            }

        case "4":

            if (VerificarLogin() == false)
            {
                break;
            }
            else
            {

                List<ItemCarrinho> itensCarrinho = carrinhoUsuario.ListarItensCarrinho();
                foreach (var item in itensCarrinho)
                {
                    Console.WriteLine($"- Id: {item.Livro.Id} | Nome: {item.Livro.Nome} | Quantidade: {item.Quantidade} | Preço unitário: {item.Livro.Preco}");
                }

                break;
            }

        case "5":

            if (VerificarLogin() == false)
            {
                break;
            }
            else
            {

                Console.WriteLine("Digite o Id do livro que deseja remover do carrinho\n");

                List<ItemCarrinho> itensCarrinho = carrinhoUsuario.ListarItensCarrinho();

                foreach (var item in itensCarrinho)
                {
                    Console.WriteLine($"- Id: {item.Livro.Id} | Nome: {item.Livro.Nome} | Quantidade: {item.Quantidade} | Preço unitário: {item.Livro.Preco}");
                }


                if (int.TryParse(Console.ReadLine(), out int idlivrocarrinho))
                {
                    carrinhoUsuario.RemoverItemCarrinho(idlivrocarrinho);
                }
                else
                {
                    Console.WriteLine("Digite um Numero valido\n");
                    break;
                }
                break;
            }

        case "6":
            if (VerificarLogin() == false)
            {
                break;
            }
            else
            {

                try
                {
                    Pedido pedidoFinalizado = carrinhoUsuario.FinalizarCarrinho();

                    Console.WriteLine("\nPedido Confirmado\n");
                    Console.WriteLine("\n");
                    Console.WriteLine($"DATA: {pedidoFinalizado.DataPedido} | STATUS: {pedidoFinalizado.Status} | Nome Cliente: {pedidoFinalizado.UsuarioLogado.Nome}");

                    foreach (var item in pedidoFinalizado.ItensPedido)
                    {
                        Console.WriteLine($"- Id: {item.Livro.Id} | Nome: {item.Livro.Nome} | Quantidade: {item.Quantidade} | Preço unitário: {item.Livro.Preco}");
                    }

                    Console.WriteLine($"Total Pedido: {pedidoFinalizado.TotalPedido}");


                    break;
                }
                catch (InvalidOperationException ex) { Console.WriteLine("O carrinho está vázio e não pode ser finalizado"); }
                break;

            }
        case "7":
            continuar = false;
            break;

        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }

    bool VerificarLogin()
    {
        if (usuarioLogado == null)
        {
            Console.WriteLine("Você precisa estar logado para acessar o catálogo.");
            return false;
        }
        return true;
    }
}