using System;
using ApiRuleta.Helpers;
using ApiRuleta.Models;
using ApiRuleta.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiRuleta.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
	{
        private readonly IUserService _userService;

        public UserController(IUserService userService)
		{
            _userService = userService;
		}


        [HttpGet("{name}", Name = "UserByName")]
        public async Task<IActionResult> GetByName(string name)
        {

            var response = await _userService.GetByName(name)!;
            return response.Succeeded is false ? NotFound(response) : Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            ResponseBase<User> response = new ResponseBase<User>();

            if (user is null)
            {
                response.Succeeded = false;
                response.Error = "Debe de enviar el usuario";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Debe de enviar correctamente el usuario";
                return BadRequest(response);
            }

            response = await _userService.Create(user);

            return CreatedAtRoute("UserByName", new { name = response.Data!.Name }, response);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name, [FromBody] User user)
        {
            ResponseBase<bool> response = new ResponseBase<bool>();

            if (user is null)
            {
                response.Succeeded = false;
                response.Error = "Debe de enviar un usuario";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                response.Succeeded = false;
                response.Error = "Debe de enviar correctamente el usuario";
                return BadRequest(response);
            }

            response = await _userService.Update(name, user);

            return response.Succeeded == false ? NotFound(response) : NoContent();
        }

    }
}

