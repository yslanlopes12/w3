using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/pix")]
    public class PixController : ControllerBase
    {
        private readonly PixKeyRepository _repository;

        public PixController(PixKeyRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("chave")]
        public async Task<IActionResult> CriarChave([FromBody] PixKey key)
        {
            key.Id = Guid.NewGuid();
            key.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(key);
            return Ok(key);
        }

        [HttpGet("chave/{key}")]
        public async Task<IActionResult> BuscarPorChave(string key)
        {
            var result = await _repository.GetByKeyAsync(key);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("chave/{id}")]
        public async Task<IActionResult> CancelarChave(Guid id)
        {
            var linhas = await _repository.CancelAsync(id);
            return linhas > 0 ? Ok("Chave cancelada") : NotFound();
        }
    }
}
