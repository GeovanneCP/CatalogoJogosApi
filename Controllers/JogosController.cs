using CatalogoJogosApi.Models;
using CatalogoJogosApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoJogosApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JogosController : ControllerBase
{
    private readonly IJogoService _jogoService;

    public JogosController(IJogoService jogoService)
    {
        _jogoService = jogoService;
    }

    // 1. POST /api/jogos -> 201 Created
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Jogo jogo)
    {
        var jogoCriado = await _jogoService.CriarAsync(jogo);
        return CreatedAtAction(nameof(ObterPorId), new { id = jogoCriado.Id }, jogoCriado);
    }

    // 2. GET /api/jogos -> 200 OK
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var jogos = await _jogoService.ObterTodosAsync();
        return Ok(jogos);
    }

    // 3. GET /api/jogos/busca?plataforma={plataforma}&precoMaximo={preco}
    [HttpGet("busca")]
    public async Task<IActionResult> BuscarPorFiltro([FromQuery] string plataforma, [FromQuery] decimal precoMaximo)
    {
        var jogos = await _jogoService.BuscarPorPlataformaEPrecoMaximoAsync(plataforma, precoMaximo);
        return Ok(jogos);
    }

    // 4. GET /api/jogos/relatorio-estoque
    [HttpGet("relatorio-estoque")]
    public async Task<IActionResult> ObterRelatorioEstoque()
    {
        var relatorio = await _jogoService.ObterRelatorioEstoquePorPlataformaAsync();
        return Ok(relatorio);
    }

    // 5. GET /api/jogos/{id} -> 200 OK ou 404 Not Found
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(string id)
    {
        var jogo = await _jogoService.ObterPorIdAsync(id);
        if (jogo == null)
            return NotFound(new { mensagem = "Jogo não encontrado." });

        return Ok(jogo);
    }

    // 6. PUT /api/jogos/{id} -> 200 OK ou 404 Not Found
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(string id, [FromBody] Jogo jogo)
    {
        var atualizado = await _jogoService.AtualizarAsync(id, jogo);
        if (!atualizado)
            return NotFound(new { mensagem = "Jogo não encontrado para atualização." });

        return Ok(jogo);
    }

    // 7. DELETE /api/jogos/{id} -> 204 No Content ou 404 Not Found
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(string id)
    {
        var removido = await _jogoService.RemoverAsync(id);
        if (!removido)
            return NotFound(new { mensagem = "Jogo não encontrado para remoção." });

        return NoContent();
    }
}