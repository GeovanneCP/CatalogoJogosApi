using CatalogoJogosApi.Models;

namespace CatalogoJogosApi.Services;

public interface IJogoService
{
    Task<List<Jogo>> ObterTodosAsync();
    Task<Jogo?> ObterPorIdAsync(string id);
    Task<Jogo> CriarAsync(Jogo jogo);
    Task<bool> AtualizarAsync(string id, Jogo jogo);
    Task<bool> RemoverAsync(string id);
    Task<List<Jogo>> BuscarPorPlataformaEPrecoMaximoAsync(string plataforma, decimal precoMaximo);
    Task<List<RelatorioEstoqueDto>> ObterRelatorioEstoquePorPlataformaAsync();
}