using AutoMapper;
using DOTNET_RPG.Dtos.Character;
using DOTNET_RPG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNET_RPG.Services.CharacterService
{
    public class CharacterService : ICharacherService
    {

        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Id = 1 , Name = "Sam" }
        };

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;

            characters.Add(character);

            return new ServiceResponse<List<GetCharacterDto>>() { Data = (characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList() };
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>>() { Data = (characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList() };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            return new ServiceResponse<GetCharacterDto>() { Data = _mapper.Map<GetCharacterDto>(characters.Where(c => c.Id == id).First()) };
        }
    }
}
