using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Models;
using DOTNET_RPG.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNET_RPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacherService _characterService;

        public CharacterController(ICharacherService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = await _characterService.UpdateCharacter(updatedCharacter);

            if(!serviceResponse.Success)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = await _characterService.DeleteCharacter(id);

            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }
    }
}
