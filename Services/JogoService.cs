using CatalogoJogosApi.Models;
using CatalogoJogosApi.Repositories;

namespace CatalogoJogosApi.Services;

public class JogoService : IJogoService
{
    private readonly IJogoRepository _jogoRepository;
    private readonly ILogger<JogoService> _logger;

    public JogoService(IJogoRepository jogoRepository, ILogger<JogoService> logger)
    {
        _jogoRepository = jogoRepository;
        _logger = logger;
    }

    public async Task<List<Jogo>> ObterTodosAsync()
    {
        _logger.LogInformation("A obter todos os jogos da base de dados");
        return await _jogoRepository.ObterTodosAsync();
    }

    public async Task<Jogo?> ObterPorIdAsync(string id)
    {
        _logger.LogInformation("A procurar jogo pelo Id: {JogoId}", id);
        return await _jogoRepository.ObterPorIdAsync(id);
    }

    public async Task<Jogo> CriarAsync(Jogo jogo)
    {
        _logger.LogInformation("A registar novo jogo: {Titulo} para a plataforma {Plataforma}", jogo.Titulo, jogo.Plataforma);
        await _jogoRepository.CriarAsync(jogo);
        return jogo;
    }

    public async Task<bool> AtualizarAsync(string id, Jogo jogo)
    {
        _logger.LogInformation("A atualizar dados do jogo Id: {JogoId}", id);
        return await _jogoRepository.AtualizarAsync(id, jogo);
    }

    public async Task<bool> RemoverAsync(string id)
    {
        _logger.LogInformation("A remover jogo Id: {JogoId}", id);
        return await _jogoRepository.RemoverAsync(id);
    }

    public async Task<List<Jogo>> BuscarPorPlataformaEPrecoMaximoAsync(string plataforma, decimal precoMaximo)
    {
        _logger.LogInformation("A filtrar jogos pela plataforma {Plataforma} com preço máximo {PrecoMaximo}", plataforma, precoMaximo);
        return await _jogoRepository.BuscarPorPlataformaEPrecoMaximoAsync(plataforma, precoMaximo);
    }

    public async Task<List<RelatorioEstoqueDto>> ObterRelatorioEstoquePorPlataformaAsync()
    {
        _logger.LogInformation("A gerar relatório consolidado de inventário por plataforma");
        return await _jogoRepository.ObterRelatorioEstoquePorPlataformaAsync();
    }
}