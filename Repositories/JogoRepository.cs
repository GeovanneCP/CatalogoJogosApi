using CatalogoJogosApi.Configurations;
using CatalogoJogosApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CatalogoJogosApi.Repositories;

public class JogoRepository : IJogoRepository
{
    private readonly IMongoCollection<Jogo> _jogosCollection;

    public JogoRepository(IOptions<MongoDbSettings> settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _jogosCollection = database.GetCollection<Jogo>(settings.Value.CollectionName);
    }

    public async Task<List<Jogo>> ObterTodosAsync()
    {
        return await _jogosCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Jogo?> ObterPorIdAsync(string id)
    {
        return await _jogosCollection.Find(j => j.Id == id).FirstOrDefaultAsync();
    }

    public async Task CriarAsync(Jogo jogo)
    {
        await _jogosCollection.InsertOneAsync(jogo);
    }

    public async Task<bool> AtualizarAsync(string id, Jogo jogo)
    {
        jogo.Id = id;
        var resultado = await _jogosCollection.ReplaceOneAsync(j => j.Id == id, jogo);
        return resultado.ModifiedCount > 0;
    }

    public async Task<bool> RemoverAsync(string id)
    {
        var resultado = await _jogosCollection.DeleteOneAsync(j => j.Id == id);
        return resultado.DeletedCount > 0;
    }

    // Desafio: Filtro por Plataforma e Preço Máximo com Builders<Jogo>.Filter
    public async Task<List<Jogo>> BuscarPorPlataformaEPrecoMaximoAsync(string plataforma, decimal precoMaximo)
    {
        var filterBuilder = Builders<Jogo>.Filter;
        var filtro = filterBuilder.And(
            filterBuilder.Eq(j => j.Plataforma, plataforma),
            filterBuilder.Lte(j => j.Preco, precoMaximo)
        );

        return await _jogosCollection.Find(filtro).ToListAsync();
    }

    // Desafio: Pipeline de Agregação agrupando por Plataforma
    public async Task<List<RelatorioEstoqueDto>> ObterRelatorioEstoquePorPlataformaAsync()
    {
        return await _jogosCollection.Aggregate()
            .Group(
                j => j.Plataforma,
                g => new RelatorioEstoqueDto
                {
                    Plataforma = g.Key,
                    TotalTitulos = g.Count(),
                    ValorTotalInventario = g.Sum(x => x.Preco * (decimal)x.Estoque)
                }
            )
            .ToListAsync();
    }
}