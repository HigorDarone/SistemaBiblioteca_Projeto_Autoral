# Sistema de Loja de Livros (C#)

Sistema de loja de livros online, feito em C# para praticar orientação a objetos, com evolução planejada para Entity Framework Core e Web API.

Veja o roadmap completo do projeto, com todas as fases e decisões de arquitetura, em [`roadmap-projeto-livraria.md`](./roadmap-projeto-livraria.md).

## Status atual

**Semana 1**

- Criadas as classes `Usuario` e `Livro`, com construtores obrigando o preenchimento dos dados essenciais.
- Aplicado encapsulamento pensando em validação: alguns campos (`Documento`, `Email`, `Senha` em `Usuario`; `Disponivel` em `Livro`) usam `private set` para impedir alteração sem passar pelas regras da própria classe.
- Criada uma pasta `Utils` com uma classe estática `Validador`, reaproveitável entre as classes já feitas. Ela verifica se um campo de texto foi preenchido (usando `string.IsNullOrWhiteSpace`) e se um número não é negativo, evitando repetir a mesma validação em cada propriedade.

**Próximos passos:** `Catalogo`, `Carrinho`, `ItemPedido` e `Pedido` (ver roadmap para detalhes de cada um).

## Tecnologias

Já em uso: C#, .NET 10, orientação a objetos.

Planejadas: Entity Framework Core (persistência em banco de dados), ASP.NET Core Web API.

## Como rodar

*(seção a preencher quando o projeto tiver um fluxo executável — Fase 2 do roadmap)*
