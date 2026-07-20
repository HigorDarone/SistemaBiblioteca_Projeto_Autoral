# Roadmap do Projeto — Sistema de Livraria (C#/.NET)

## Visão geral

Sistema em C# simulando uma livraria: cadastro de usuário, catálogo de livros, carrinho de compras e histórico de pedidos. Começa como aplicativo de console e evolui em fases, primeiro validando a lógica de negócio, depois adicionando persistência em banco de dados.

O projeto é dividido em fases porque cada uma testa uma coisa de cada vez — evita misturar bug de lógica com bug de banco de dados ou de interface, o que tornaria muito mais difícil descobrir onde está o problema quando algo dá errado.

## Fase 1 — Modelagem do domínio (em andamento)

Objetivo: criar as classes que representam as entidades do sistema, aplicando encapsulamento (properties com `get`/`set` público ou privado conforme a regra de negócio de cada uma) e validação de dados obrigatórios.

Status atual:

- **Livro** — concluído. Propriedades: `Id`, `Nome`, `Autor`, `Editora`, `Genero`, `Preco`, `Disponivel`. Construtor obrigando os dados essenciais. Validação de texto (não vazio) e de número (não negativo) via `Validador`. `Disponivel` com `private set` porque só deve mudar como consequência de uma venda ou cancelamento, nunca por acesso direto externo.
- **Usuario** — concluído. Propriedades: `Id`, `Nome` (set público), `Documento`, `Email`, `Senha` (todos com `private set`, porque são dados sensíveis que não devem mudar livremente de fora da classe). Mesma validação de texto obrigatório via `Validador`.
- **Validador** — criado. Classe estática (`static class`) com `ValidarTexto` e `ValidarNumero`, reutilizável entre `Livro`, `Usuario` e as próximas classes, evitando duplicar a mesma lógica de validação em cada uma.
- **Catalogo** — pendente. Guarda a lista de todos os livros cadastrados. Métodos: `BuscarPorNome`, `BuscarPorGenero`. Não guarda lógica de livro individual, só gerencia a coleção (responsabilidade separada do `Livro`).
- **Carrinho** — pendente. Sabe de quem é (usuário dono) e guarda os livros selecionados com quantidade, sempre refletindo o preço atual do livro (não congelado ainda). Métodos: `AdicionarItem`, `RemoverItem`, `CalcularTotal`, `Finalizar` (que cria e devolve um `Pedido`).
- **ItemPedido** — pendente. Guarda `Livro`, quantidade e o preço pago no momento da compra (congelado — não muda mesmo que o preço do livro mude depois).
- **Pedido** — pendente. Guarda o usuário, a lista de `ItemPedido`, data, valor total e status (pendente, concluído, cancelado).

Critério de conclusão da fase: todas as classes compilam sem erro, têm construtor completo, e cada decisão de `set` público/privado pode ser explicada com uma razão de negócio (não só "porque sim").

## Fase 2 — Orquestração em memória (Program.cs)

Objetivo: montar um fluxo funcional no console (cadastro → login → navegar catálogo → montar carrinho → finalizar pedido → ver histórico de pedidos), usando listas em memória (`List<Usuario>`, `List<Livro>`, etc.) simulando um banco de dados temporário.

Por que antes do banco de dados: permite testar a lógica de negócio isoladamente. Se algo der errado nessa fase, o problema só pode estar na lógica em si, não numa conexão de banco — o que facilita muito debugar.

Inclui: tratamento de exceções com `try`/`catch` na hora de ler dados digitados pelo usuário, aplicando de forma amigável as validações já escritas na Fase 1 (mostrando mensagem de erro em vez de deixar o programa quebrar).

## Fase 3 — Persistência com banco de dados

Objetivo: substituir as listas em memória por Entity Framework Core conectado a um banco real (SQLite pra desenvolvimento local mais simples, ou SQL Server, mais alinhado com o que vagas de estágio C#/.NET pedem), sem alterar a lógica de negócio já validada na Fase 2.

Inclui: criação de uma classe `DbContext`, migrations (comandos que geram as tabelas a partir das classes), e CRUD real conectado ao banco.

## Fase 4 — Melhorias futuras

- Hash de senha: substituir o armazenamento em texto puro por um hash seguro (ex: biblioteca BCrypt.Net-Next), já que senha nunca deveria ficar salva de forma legível.
- Expor o sistema como Web API (ASP.NET Core), testável via Swagger — formato mais alinhado com o que aparece nas vagas de estágio backend, sem precisar construir uma interface visual (frontend).
- Testes automatizados (xUnit) para validar as regras de negócio sem precisar testar manualmente toda vez.

## Por que documentar isso

Ter esse roadmap no README do GitHub mostra pra quem for avaliar o projeto (recrutador, ou você mesmo revisando depois de um tempo) que houve raciocínio de arquitetura por trás das decisões, não só código resolvendo um problema pontual. Isso é um diferencial real em processo seletivo de estágio, e também serve de guia rápido pra explicar o projeto numa entrevista técnica sem precisar decorar tudo.
