# Roadmap do Projeto — Sistema de Livraria (C#/.NET)

## Visão geral

Sistema em C# simulando uma livraria: cadastro de usuário, catálogo de livros, carrinho de compras e histórico de pedidos. Começa como aplicativo de console e evolui em fases, primeiro validando a lógica de negócio, depois adicionando persistência em banco de dados.

O projeto é dividido em fases porque cada uma testa uma coisa de cada vez — evita misturar bug de lógica com bug de banco de dados ou de interface, o que tornaria muito mais difícil descobrir onde está o problema quando algo dá errado.

## Fase 1 — Modelagem do domínio (concluída)

Objetivo: criar as classes que representam as entidades do sistema, aplicando encapsulamento (properties com `get`/`set` público ou privado conforme a regra de negócio de cada uma) e validação de dados obrigatórios.

Status atual:

- **Livro** — concluído. Propriedades: `Id`, `Nome`, `Autor`, `Editora`, `Genero`, `Preco`, `Disponivel`. Construtor obrigando os dados essenciais. Validação de texto (não vazio) e de número (não negativo) via `Validador`. `Disponivel` com `private set` porque só deve mudar como consequência de uma venda ou cancelamento, nunca por acesso direto externo.
- **Usuario** — concluído. Propriedades: `Id`, `Nome` (set público), `Documento`, `Email`, `Senha` (todos com `private set`, porque são dados sensíveis que não devem mudar livremente de fora da classe). Mesma validação de texto obrigatório via `Validador`.
- **Validador** — criado. Classe estática (`static class`) com `ValidarTexto` e `ValidarNumero`, reutilizável entre `Livro`, `Usuario` e as próximas classes, evitando duplicar a mesma lógica de validação em cada uma.
- **Catalogo** — concluído. Guarda a lista de todos os livros cadastrados. Métodos: `BuscarPorNome`, `BuscarPorGenero`, `BuscarPorId`, `AdicionarLivro`, `RemoverLivro`, `ListarLivros`. Não guarda lógica de livro individual, só gerencia a coleção (responsabilidade separada do `Livro`).
- **Carrinho** — concluído. Sabe de quem é (usuário dono) e guarda os itens selecionados com quantidade. Métodos: `AdicionarItemCarrinho`, `RemoverItemCarrinho`, `RemoverQuantidadeItemCarrinho`, `CalcularTotal`, `FinalizarCarrinho` (que cria e devolve um `Pedido`).
- **ItemPedido** — concluído. Guarda `Livro`, quantidade e o preço pago no momento da compra (congelado — não muda mesmo que o preço do livro mude depois).
- **Pedido** — concluído. Guarda o usuário, a lista de `ItemPedido`, data, valor total e status (pendente, concluído, cancelado).

Critério de conclusão da fase: todas as classes compilam sem erro, têm construtor completo, e cada decisão de `set` público/privado pode ser explicada com uma razão de negócio (não só "porque sim").

## Fase 2 — Orquestração em memória (Program.cs) — concluída

Objetivo: montar um fluxo funcional no console (cadastro → login → navegar catálogo → montar carrinho → finalizar pedido → ver histórico de pedidos), usando listas em memória (`List<Usuario>`, `List<Livro>`, etc.) simulando um banco de dados temporário.

Por que antes do banco de dados: permite testar a lógica de negócio isoladamente. Se algo der errado nessa fase, o problema só pode estar na lógica em si, não numa conexão de banco — o que facilita muito debugar.

Inclui: tratamento de exceções com `try`/`catch` na hora de ler dados digitados pelo usuário, aplicando de forma amigável as validações já escritas na Fase 1 (mostrando mensagem de erro em vez de deixar o programa quebrar).

## Fase 3 — Persistência com banco de dados — concluída

Objetivo: substituir as listas em memória por Entity Framework Core conectado a um banco real, sem alterar a lógica de negócio já validada na Fase 2. Banco escolhido: MySQL (via provider `Pomelo.EntityFrameworkCore.MySql`), aproveitando o MySQL Workbench já disponível no ambiente.

Inclui:

- Classe `AppDbContext` com um `DbSet<T>` por entidade, e migrations (`Add-Migration`/`Update-Database`) gerando as tabelas a partir das classes já modeladas na Fase 1.
- Construtores privados adicionais (sem parâmetros) em `Livro`, `Carrinho`, `ItemCarrinho`, `Pedido` e `ItemPedido`, exigidos pelo EF Core para reconstruir objetos a partir do banco, sem abrir mão da validação nos construtores públicos usados pelo resto do código.
- `Catalogo` e uma nova classe `GerenciadorUsuarios` reescritas para consultar e persistir dados via LINQ (`Where`, `Any`, `FirstOrDefault`, `Sum`) contra o `AppDbContext`, no lugar de listas em memória.
- Controle de acesso simples: propriedade `EhAdministrador` em `Usuario` (só alterável direto no banco), restringindo adicionar/remover livros do catálogo a administradores.
- `Carrinho` persistido no banco desde sua criação, com `ItemCarrinho` referenciando o carrinho dono via `CarrinhoId` — permitindo que o carrinho sobreviva ao fechar e abrir o programa novamente.
- Login real (busca por email/senha no banco), que reaproveita o carrinho existente do usuário em vez de criar um novo a cada sessão.
- Uso de `Include()` para carregamento adiantado (eager loading) de relacionamentos (`Livro` dentro de `ItemCarrinho`), evitando erros de referência nula ao acessar dados relacionados que o EF Core não carrega automaticamente por padrão.

## Fase 4 — Melhorias futuras (em andamento)

- **Hash de senha — concluído.** Substituído o armazenamento em texto puro por hash com `BCrypt.Net-Next`: a propriedade `Senha` em `Usuario` já grava o hash (`BCrypt.HashPassword`), nunca o texto original. `Login`, em `GerenciadorUsuarios`, busca o usuário só pelo email (traduzível para SQL) e depois compara a senha digitada com o hash salvo usando `BCrypt.Verify` (comparação feita em código, não no banco).
- **Web API (ASP.NET Core) — concluída para Usuários e Livros.** Solução reorganizada em três projetos: o Console original, uma nova biblioteca de classes `SistemaBiblioteca_Dominio` (compartilhando `Data`, `Models` e `Utils` entre os projetos) e um novo `SistemaBiblioteca_WebApi`, com referência de mão única (Console e WebApi dependem de Dominio, nunca o contrário). Documentação e teste interativo via **Scalar** (escolhido no lugar do Swashbuckle, que tinha um conflito de versão conhecido com .NET 10). `LivrosController` expõe listagem, busca por nome/gênero/id, adicionar e remover. `UsuariosController` expõe cadastro e login, recebendo os dados através de classes DTO (`CadastroRequest`, `LoginRequest`) em vez das classes de domínio diretamente — isso evita que a validação e o hash de senha, que rodam dentro do construtor de `Usuario`, disparem durante a própria desserialização do JSON, antes do código do controller poder tratar o erro. Erros de validação e de regra de negócio (usuário duplicado, login inválido) são capturados com `try`/`catch` e convertidos em respostas HTTP apropriadas (`400 Bad Request`, `409 Conflict`, `401 Unauthorized`), em vez de erros genéricos de servidor.
- Corrigido, nesse processo, um bug em que o EF Core, ao ler um `Usuario` já existente do banco, reutilizava o construtor público (o mesmo do cadastro) para remontar o objeto — fazendo a senha, já salva como hash, passar pela propriedade `Senha` de novo e ser hasheada uma segunda vez, quebrando o login. Resolvido com um construtor privado sem parâmetros em `Usuario`, o mesmo padrão já usado em `Carrinho`, `ItemCarrinho` e `Pedido`.
- Expor `Carrinho` e `Pedido` como endpoints na Web API (adicionar/listar/remover item, finalizar pedido, histórico).
- Testes automatizados (xUnit) para validar as regras de negócio sem precisar testar manualmente toda vez.

## Por que documentar isso

Ter esse roadmap no README do GitHub mostra pra quem for avaliar o projeto (recrutador, ou você mesmo revisando depois de um tempo) que houve raciocínio de arquitetura por trás das decisões, não só código resolvendo um problema pontual. Isso é um diferencial real em processo seletivo de estágio, e também serve de guia rápido pra explicar o projeto numa entrevista técnica sem precisar decorar tudo.
