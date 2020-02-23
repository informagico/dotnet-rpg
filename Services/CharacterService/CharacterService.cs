

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
	public class CharacterService : ICharacterService
	{
		private static List<Character> characters = new List<Character> {
			new Character(),
			new Character { Id = 1, Name = "Sam"}
		};

		public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
		{
			ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
			// serviceResponse.Data = (characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
			serviceResponse.Data = (_dBContext.Characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList(); // MAGO:

			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
		{
			ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
			// serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
			serviceResponse.Data = _mapper.Map<GetCharacterDto>(_dBContext.Characters.Where(c => c.Id == id).FirstOrDefault()); // MAGO:

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
		{
			Character character = _mapper.Map<Character>(newCharacter);
			// character.Id = characters.Max(c => c.Id) + 1;
			// characters.Add(_mapper.Map<Character>(character));
			_dBContext.Characters.Add(_mapper.Map<Character>(character)); // MAGO:
			_dBContext.SaveChanges(); // MAGO:

			ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
			// serviceResponse.Data = (characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
			serviceResponse.Data = (_dBContext.Characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList(); // MAGO:

			return serviceResponse;
		}

		public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
		{
			ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

			try
			{
				// Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
				Character character = _dBContext.Characters.Where(c => c.Id == updatedCharacter.Id).FirstOrDefault(); // MAGO:
				character.Name = updatedCharacter.Name;
				character.Class = updatedCharacter.Class;
				character.Defense = updatedCharacter.Defense;
				character.HitPoints = updatedCharacter.HitPoints;
				character.Intelligence = updatedCharacter.Intelligence;
				character.Strength = updatedCharacter.Strength;

				_dBContext.Characters.Update(character); // MAGO:
				_dBContext.SaveChanges(); // MAGO:

				serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
				serviceResponse.Data = null;
			}

			return serviceResponse;
		}

		public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
		{
			ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

			try
			{
				// Character character = characters.First(c => c.Id == id);
				// characters.Remove(character);
				Character character = _dBContext.Characters.Where(c => c.Id == id).FirstOrDefault(); // MAGO:
				_dBContext.Characters.Remove(character);
				_dBContext.SaveChanges();

				// serviceResponse.Data = (characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
				serviceResponse.Data = (_dBContext.Characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
			}
			catch (Exception ex)
			{
				serviceResponse.Success = false;
				serviceResponse.Message = ex.Message;
				serviceResponse.Data = null;
			}

			return serviceResponse;
		}

		private readonly IMapper _mapper;
		private readonly SQLiteDBContext _dBContext;

		public CharacterService(IMapper mapper, SQLiteDBContext dBContext)
		{
			_dBContext = dBContext;
			_mapper = mapper;
		}
	}
}
