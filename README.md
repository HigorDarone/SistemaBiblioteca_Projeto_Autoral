# Sistema de Loja de Livros (C#)

Sistema de loja de livros online, feito em C# para praticar orientação a objetos, com evolução planejada para Entity Framework Core e Web API.

Veja o roadmap completo do projeto, com todas as fases e decisões de arquitetura, em [`roadmap-projeto-livraria.md`](./roadmap-projeto-livraria.md).

## Status atual

**Semana 1**

- Criadas as classes `Usuario` e `Livro`, com construtores obrigando o preenchimento dos dados essenciais.
- Aplicado encapsulamento pensando em validação: alguns campos (`Documento`, `Email`, `Senha` em `Usuario`; `Disponivel` em `Livro`) usam `private set` para impedir alteração sem passar pelas regras da própria classe.
- Criada uma pasta `Utils` com uma classe estática `Validador`, reaproveitável entre as classes já feitas. Ela verifica se um campo de texto foi preenchido (usando `string.IsNullOrWhiteSpace`) e se um número não é negativo, evitando repetir a mesma validação em cada propriedade.

**Parte 2**

- Criada a classe `Catalogo`, que inicializa e guarda a lista de livros internamente (protegida, sem exposição direta) e oferece `AdicionarLivro` (com checagem de duplicata por nome e autor), `RemoverLivro`, `BuscarPorNome` e `BuscarPorGenero` (busca parcial, encontrando qualquer livro cujo nome ou gênero contenha o termo digitado, sem diferenciar maiúsculas de minúsculas).
- Criada a classe `Carrinho`, cujo construtor recebe o `Usuario` dono do carrinho. Ela trabalha junto com a classe `ItemCarrinho` (que guarda um `Livro` e sua quantidade) e oferece `AdicionarItemCarrinho` (juntando quantidade se o livro já estiver no carrinho, em vez de duplicar), `RemoverItemCarrinho`, `RemoverQuantidadeItemCarrinho`, `CalcularTotal` e `FinalizarCarrinho`.
- Criadas as classes `Pedido` e `ItemPedido`. O `Pedido` recebe no construtor o usuário logado e a lista de itens, registrando automaticamente a data de criação e o status inicial ("Pendente"). Cada `ItemPedido` guarda o `Livro`, a quantidade e o preço unitário pago naquele momento — esse preço fica congelado, mesmo que o preço do livro mude depois no catálogo.
- `FinalizarCarrinho` (dentro de `Carrinho`) verifica se existem itens no carrinho antes de prosseguir (lançando uma exceção se estiver vazio), converte cada item do carrinho num `ItemPedido` (preservando o preço do momento), cria e devolve um `Pedido` novo, e esvazia o carrinho em seguida.

Com isso, a Fase 1 do roadmap (modelagem do domínio) está concluída — todas as classes principais testadas manualmente no `Program.cs`.

**Parte 3**

- Montado o fluxo interativo no `Program.cs`: um menu em loop (`while` controlado por uma variável booleana `continuar`) com sete opções, simulando as principais ações do sistema.
- Opção 1 (cadastrar usuário): recebe nome, documento, email e senha, cria um `Usuario` e já assume esse usuário como o logado no momento — ainda não é um login de verdade, já que ainda não existe persistência em banco de dados (isso fica pra Fase 3). Já cria também o `Carrinho` vinculado a esse usuário, junto com o cadastro.
- Opção 2 (catálogo): um submenu com buscar por nome, buscar por gênero, adicionar livro, remover livro e listar todos os livros. Ao adicionar um livro, o preço digitado é validado com `TryParse` antes de criar o `Livro` e adicioná-lo ao catálogo.
- Criada uma função `VerificarLogin()`, reaproveitada nas opções que exigem um usuário logado (adicionar, listar e remover item do carrinho, finalizar pedido) — ela verifica se existe um usuário logado e avisa quando não existe, em vez de repetir a mesma checagem em cada opção.
- Opção 3 (adicionar ao carrinho): lista os livros do catálogo, pede o Id do livro e a quantidade desejada, e adiciona o item ao carrinho do usuário logado.
- Opções 4 e 5: listar e remover itens do carrinho.
- Opção 6 (finalizar pedido): chama `FinalizarCarrinho()`, que transforma os itens do carrinho num `Pedido` com o total calculado. O caso de tentar finalizar um carrinho vazio (que lança uma exceção) é tratado com `try`/`catch`, mostrando uma mensagem amigável em vez de derrubar o programa.
- A maioria dos pontos onde o usuário digita um número usa `TryParse`, para evitar que uma entrada inválida quebre o programa.
- Opção 7 encerra o programa.

Com isso, a Fase 2 do roadmap (orquestração em memória) está praticamente concluída — o fluxo completo (cadastro, catálogo, carrinho, finalização de pedido) já funciona de ponta a ponta pelo console. Alguns ajustes pontuais ainda estão sendo testados e corrigidos.

**Próximos passos:** Fase 3 do roadmap — persistência com Entity Framework Core, substituindo as listas em memória por um banco de dados real.

## Tecnologias

Já em uso: C#, .NET 10, orientação a objetos.

Planejadas: Entity Framework Core (persistência em banco de dados), ASP.NET Core Web API.

## Como rodar

1. Abra a pasta `SistemaBiblioteca_Projeto_Autoral` no Visual Studio e rode o projeto (F5), ou, via terminal, entre na pasta do projeto e rode `dotnet run`.
2. O menu interativo aparece no console. Comece pela opção 1 (cadastrar usuário) para poder usar o carrinho, depois explore o catálogo (opção 2) para cadastrar livros antes de adicioná-los ao carrinho.
