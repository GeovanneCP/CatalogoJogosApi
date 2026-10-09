# 🎮 Catálogo de Jogos API

> API RESTful de Alta Performance desenvolvida com **.NET 8 / C#** e persistência em **MongoDB**.  
> Projeto concebido segundo os padrões de arquitetura limpa, princípios SOLID, observabilidade e garantia de qualidade com testes automatizados.

---

## 📌 Sobre o Projeto

O **Catálogo de Jogos API** foi desenvolvido com o objetivo de fornecer uma solução escalável para gestão de catálogo, precificação e inventário de jogos eletrónicos físicos e digitais.

A solução resolve as dores clássicas de controlo de catálogo em e-commerces e lojas especializadas:
* **Cadastro estruturado**: Tipagem forte e metadados no padrão BSON.
* **Consultas otimizadas**: Filtros compostos por plataforma e faixa de preço.
* **Relatórios analíticos de inventário**: Pipelines de agregação do MongoDB em tempo real.
* **Monitorização de infraestrutura**: Endpoints de *Health Check* nativos.
* **Qualidade de software**: Testes unitários isolados com dublês de teste (*Mocks*).

---

## 🏛️ Arquitetura e Boas Práticas

O projeto foi desenhado sob uma arquitetura desacoplada em três camadas principais:

```text
[ Requisições HTTP ]
         │
         ▼
┌──────────────────┐
│   Controllers    │  --> Validação de payload & HTTP Status
└────────┬─────────┘
         │
         ▼
┌──────────────────┐
│     Services     │  --> Regras de negócio & Logging estruturado
└────────┬─────────┘
         │
         ▼
┌──────────────────┐
│   Repositories   │  --> MongoDB Driver & Consultas BSON / Agregações
└────────┬─────────┘
         │
         ▼
[( Banco: MongoDB )]

```

## Principais Padrões Utilizados:

Options Pattern (IOptions): Centralização segura e desacoplada das configurações de conexão no appsettings.json.

Injeção de Dependência (DI): Ciclo de vida adequado para cada serviço (Singleton para o cliente Mongo e Scoped para Services e Repositories).

Mapeamento BSON: Decorators [BsonId], [BsonRepresentation] e [BsonElement] garantindo controle total sobre como o documento NoSQL é serializado.

Logging Estruturado (ILogger): Rastreabilidade de chamadas em pontos críticos do domínio.


## 🛠️ Tecnologias Utilizadas

.NET 8 SDK (C#)

ASP.NET Core Web API

MongoDB Community Server

MongoDB.Driver (v3.x)

Swagger / OpenAPI

xUnit

Moq

## 🚀 Endpoints da APIA

documentação interativa completa pode ser visualizada via Swagger em:
https://localhost:7106/swagger.

MétodoEndpointDescriçãoStatus de SucessoPOST/api/jogosCadastra um novo título no catálogo201

CreatedGET/api/jogosLista todos os jogos cadastrados200 OK

GET/api/jogos/{id}Procura os detalhes de um jogo por ID200 OK / 404 Not Found

PUT/api/jogos/{id}Atualiza as informações de um jogo existente200 OK / 404 Not Found

DELETE/api/jogos/{id}Remove um jogo do catálogo204 No Content / 404 Not Found

GET/api/jogos/buscaFiltro avançado por plataforma e preço máximo200 OK

GET/api/jogos/relatorio-estoqueRelatório consolidado agrupado por plataforma200 OK

GET/healthVerificação de integridade e conectividade com a base de dados200 Healthy

## 🧪 Testes Unitários
O projeto de testes (CatalogoJogosApi.UnitTests) foi estruturado utilizando xUnit e Moq, aplicando o padrão AAA (Arrange, Act, Assert):

ObterPorIdAsync_QuandoJogoExiste_RetornaJogoEsperado

ObterPorIdAsync_QuandoJogoNaoExiste_RetornaNulo

CriarAsync_QuandoChamado_ChamaRepositorioERetornaJogo

Para executar os testes via terminal:

dotnet test

## 👥 Membros do Grupo

Geovanne Coneglian Passos — RM562673

Lucas Silva Gastão Pinheiro — RM563960

Guilherme Soares de Almeida — RM563143
