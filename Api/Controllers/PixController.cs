using Api.Dto;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/pix")]
    public class PixController : ControllerBase
    {
        private readonly IPixKeyService _pixKeyService;

        public PixController(IPixKeyService pixKeyService)
        {
            _pixKeyService = pixKeyService;
        }


        // POST /api/pix/chaves
        [HttpPost("chaves")]
        public async Task<IActionResult> CriarChave([FromBody] PixKeyCreateDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _pixKeyService.CreateAsync(dto);


                return CreatedAtAction(
                    nameof(ValidarChave),
                    new { chave = result.ChaveValor },
                    result
                );
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }


        // GET /api/pix/chaves/{chave}/validar
        [HttpGet("chaves/{chave}/validar")]
        public async Task<IActionResult> ValidarChave(string chave)
        {
            var result = await _pixKeyService.ValidateAsync(chave);
            return Ok(result);
        }

        // GET /api/contas/{accountId}/pix/chaves
        [HttpGet("/api/contas/{accountId}/pix/chaves")]
        public async Task<IActionResult> GetByAccountId(Guid accountId)
        {
            var result = await _pixKeyService.GetByAccountIdAsync(accountId);
            return Ok(result);
        }

        // PUT /api/pix/chaves/{id}
        [HttpPut("chaves/{id}")]
        public async Task<IActionResult> EditarChave(Guid id, [FromBody] PixKeyUpdateDto dto)
        {
            var result = await _pixKeyService.UpdateAsync(id, dto);
            return result ? Ok("Chave atualizada") : NotFound();
        }

        // DELETE /api/pix/chaves/{id}
        [HttpDelete("chaves/{id}")]
        public async Task<IActionResult> CancelarChave(Guid id)
        {
            var result = await _pixKeyService.CancelAsync(id);
            return result ? Ok("Chave cancelada") : NotFound();
        }
    }
}
