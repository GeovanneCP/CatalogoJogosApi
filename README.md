🎮 Catálogo de Jogos APIAPI RESTful de Alta Performance desenvolvida com .NET 8 / C# e persistência em MongoDB.Projeto acadêmico concebido segundo os padrões de arquitetura limpa, princípios SOLID, observabilidade e garantia de qualidade com testes automatizados.📌 Sobre o ProjetoO Catálogo de Jogos API foi desenvolvido com o objetivo de fornecer uma solução escalável para gerenciamento de catálogo, precificação e inventário de jogos eletrônicos físicos e digitais.A solução resolve as dores clássicas de controle de catálogo em e-commerces e lojas especializadas:Cadastro estruturado com tipagem forte e metadados no padrão BSON.Consultas otimizadas por plataforma e faixa de preço.Relatórios analíticos de inventário em tempo real utilizando o poder dos pipelines de agregação do MongoDB.Monitoramento de infraestrutura (liveness/readiness) com endpoints de Health Check nativos.Cobertura de regras de negócio com testes unitários isolados por dublês de teste (Mocks).🏛️ Arquitetura e Boas PráticasO projeto foi desenhado sob uma arquitetura desacoplada em três camadas principais, garantindo que o fluxo de dependência aponte sempre para abstrações (DIP - Dependency Inversion Principle):                       [ Requisições HTTP ]
                                │
                                ▼
                       ┌─────────────────┐
                       │   Controllers   │  (Validação de payload & HTTP Status)
                       └────────┬────────┘
                                │
                                ▼
                       ┌─────────────────┐
                       │    Services     │  (Regras de negócio & Logging)
                       └────────┬────────┘
                                │
                                ▼
                       ┌─────────────────┐
                       │  Repositories   │  (MongoDB Driver & Consultas BSON)
                       └────────┬────────┘
                                │
                                ▼
                       [( Banco de Dados: MongoDB )]
Principais Padrões Utilizados:Options Pattern (IOptions<MongoDbSettings>): Centralização segura e desacoplada das strings de conexão e parâmetros da base de dados.Dependency Injection (DI): Injeção de dependências nativa do ASP.NET Core configurando o ciclo de vida adequado para cada serviço (Singleton para o cliente Mongo e Scoped para Services e Repositories).Mapeamento BSON: Decorators [BsonId], [BsonRepresentation] e [BsonElement] garantindo controle total sobre como o documento NoSQL é serializado no banco.Logging Estruturado (ILogger<T>): Rastreabilidade de chamadas em pontos críticos do domínio.🛠️ Tecnologias Utilizadas.NET 8 SDK (C#)ASP.NET Core Web APIMongoDB (Community Server / Atlas)MongoDB.Driver (v3.x / oficial)Swagger / OpenAPI (Documentação interativa de API)xUnit (Framework de testes unitários)Moq (Biblioteca de Mocks e asserções de chamadas)🚀 Endpoints da APIA documentação interativa completa pode ser visualizada via Swagger em: https://localhost:<PORTA>/swaggerMétodoEndpointDescriçãoStatus de SucessoPOST/api/jogosCadastra um novo título no catálogo201 CreatedGET/api/jogosLista todos os jogos cadastrados200 OKGET/api/jogos/{id}Busca os detalhes de um jogo por ID200 OK / 404 Not FoundPUT/api/jogos/{id}Atualiza as informações de um jogo existente200 OK / 404 Not FoundDELETE/api/jogos/{id}Remove um jogo do catálogo204 No Content / 404 Not FoundGET/api/jogos/buscaFiltro dinâmico por plataforma e precoMaximo200 OKGET/api/jogos/relatorio-estoqueRelatório consolidado via Pipeline de Agregação200 OKGET/healthHealth Check da API e conectividade com MongoDB200 OK (Healthy)📊 Exemplos de Consultas Avançadas1. Filtro com Builders<Jogo>.FilterPermite filtrar jogos com preço até um valor limite em uma determinada plataforma:GET /api/jogos/busca?plataforma=PlayStation%203&precoMaximo=100
2. Relatório de Agregação de InventárioAgrupa os jogos por plataforma e calcula dinamicamente a contagem e o valor financeiro do estoque:[
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
🧪 Cobertura de Testes UnitáriosO projeto de testes CatalogoJogosApi.UnitTests valida de forma isolada a camada de serviço (JogoService) utilizando o padrão AAA (Arrange, Act, Assert) e dublês de teste com o Moq:ObterPorIdAsync_QuandoJogoExiste_RetornaJogoEsperadoObterPorIdAsync_QuandoJogoNaoExiste_RetornaNuloCriarAsync_QuandoChamado_ChamaRepositorioERetornaJogoPara executar os testes via terminal:dotnet test
⚙️ Como Executar o Projeto LocalmentePré-requisitos:.NET 8 SDK instalado.MongoDB Community Server rodando na porta padrão 27017 (ou MongoDB Compass/Docker).Passo a passo:Clone o repositório:git clone <URL_DO_REPOSITORIO>
cd CatalogoJogosApi
Verifique o arquivo appsettings.json:"MongoDbSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "CatalogoJogosDb",
  "CollectionName": "Jogos"
}
Restaure as dependências e compile:dotnet restore
dotnet build
Execute a aplicação:dotnet run --project CatalogoJogosApi
Acesse no navegador:Swagger UI: https://localhost:7106/swagger (ou porta configurada)Health Check: https://localhost:7106/health👥 Membros do GrupoGeovanne Coneglian Passos — RM 562673Lucas Silva Gastão Pinheiro — RM 563960Guilherme Soares de Almeida — RM 563143