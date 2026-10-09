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


## ⚙️ Configuração e Execução do Projeto

Pré-requisitos:

.NET 8 SDK instalado.

MongoDB Community Server em execução localmente na porta padrão (localhost:27017).

# Restaurar dependências

dotnet restore

# Compilar e iniciar a API

dotnet run --project CatalogoJogosApi


---

## 💡 Exemplos Sugeridos de Payload e Testes

Para facilitar a avaliação da banca e os testes das rotas via Swagger, seguem exemplos com dados completos para cada operação:

### 1. Cadastro de Jogos (`POST /api/jogos`)

**Exemplo 1 (PlayStation 3):**
```json
{
  "titulo": "Gran Turismo 6",
  "plataforma": "PlayStation 3",
  "genero": "Corrida",
  "preco": 120.50,
  "anoLancamento": 2013,
  "estoque": 15
}
```

Exemplo 2 (PlayStation 3 - Preço Menor):

```JSON
{
  "titulo": "God of War III",
  "plataforma": "PlayStation 3",
  "genero": "Ação",
  "preco": 80.00,
  "anoLancamento": 2010,
  "estoque": 10
}
Exemplo 3 (Xbox 360):
```
```JSON
{
  "titulo": "Halo 3",
  "plataforma": "Xbox 360",
  "genero": "FPS",
  "preco": 95.00,
  "anoLancamento": 2007,
  "estoque": 5
}
```
2. Atualização de Dados (PUT /api/jogos/{id})
Parâmetro de Rota (id): ID gerado no cadastro (ex.: 673f8e5d0a1b2c3d4e5f6a7b)

Corpo da Requisição (Request Body):

```JSON
{
  "titulo": "Gran Turismo 6 - Edição de Colecionador",
  "plataforma": "PlayStation 3",
  "genero": "Corrida",
  "preco": 149.90,
  "anoLancamento": 2013,
  "estoque": 8
}
```
3. Busca com Filtros Compostos (GET /api/jogos/busca)
Cenário de Teste: Filtrar títulos de PlayStation 3 que custem até R$ 100,00.

Parâmetros da Query:

plataforma: PlayStation 3

precoMaximo: 100

Resultado Esperado: Retorna apenas o God of War III, excluindo o Gran Turismo 6 pelo critério de preço.

4. Relatório Agregado de Inventário (GET /api/jogos/relatorio-estoque)
Parâmetros: Nenhum (agregação global por plataforma).

Estrutura de Retorno:

```JSON
[
  {
    "plataforma": "PlayStation 3",
    "totalTitulos": 2,
    "valorTotalInventario": 2607.50
  },
  {
    "plataforma": "Xbox 360",
    "totalTitulos": 1,
    "valorTotalInventario": 475.00
  }
]
```
## 👥 Membros do Grupo

Geovanne Coneglian Passos — RM562673

Lucas Silva Gastão Pinheiro — RM563960

Guilherme Soares de Almeida — RM563143
