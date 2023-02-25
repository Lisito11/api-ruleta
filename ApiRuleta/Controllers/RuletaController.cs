using System;
using ApiRuleta.Helpers;
using ApiRuleta.Models;
using ApiRuleta.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiRuleta.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RuletaController : ControllerBase
	{
        private readonly IRuletaService _ruletaService;

        public RuletaController(IRuletaService ruletaService)
		{
            _ruletaService = ruletaService;
		}

        [HttpGet("girar")]
        public IActionResult GetRandomNumberAndColor()
        {
            var response = _ruletaService.GetRandomNumberAndColor();
            return Ok(response);
        }

        [HttpPost("apostar")]
        public async Task<IActionResult> MakeBet([FromBody] Bet betUser)
        {
            var response = await _ruletaService.MakeBet(betUser);
            return Ok(response);
        }
    }
}

