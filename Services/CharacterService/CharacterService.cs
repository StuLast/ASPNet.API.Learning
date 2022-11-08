using dotnet.rpg.Dtos.Character;
using AutoMapper;

namespace dotnet.rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private readonly IMapper _mapper;
    public CharacterService(IMapper mapper)
    {
      _mapper = mapper;
      
    }
    private static List<Character> characters = new List<Character>(){
        new Character(),
        new Character {Id = 1, Name = "Sam"}
    };
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var response = new Models.ServiceResponse<List<GetCharacterDto>>();
        Character character = _mapper.Map<Character>(newCharacter);
        character.Id = characters.Max(c => c.Id) + 1;
        characters.Add(character);
        
        response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return response;
    }

       public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
      var response = new Models.ServiceResponse<List<GetCharacterDto>>();
      try 
      { 
        Character character = characters.First(c => c.Id != id);
        characters.Remove(character);
        response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      } catch(Exception err) {
        {
          response.Success = false;
          response.Message = $"{err.Message}: Unable to find or delete the character.";
        }
      }
      response.Message = "Your character has been deleted";
      return response;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
      var response = new Models.ServiceResponse<List<GetCharacterDto>>();
      response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return response;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
      var response = new Models.ServiceResponse<GetCharacterDto>();
      var character = characters.FirstOrDefault(c => c.Id == id);
      response.Data = _mapper.Map<GetCharacterDto>(character);
      return response;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      var response = new Models.ServiceResponse<GetCharacterDto>();
      try {
      Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

       //_mapper.Map(updatedCharacter, character);

      character.Name = updatedCharacter.Name;
      character.HitPoints = updatedCharacter.HitPoints;
      character.Strength = updatedCharacter.Strength;
      character.Defence = updatedCharacter.Defence;
      character.Intelligence = updatedCharacter.Intelligence;
      character.Class = updatedCharacter.Class;

      response.Data = _mapper.Map<GetCharacterDto>(character);
      } catch(Exception err) {
        {
          response.Success = false;
          response.Message = $"{err.Message}: Unable to find or update the character.";
        }
      }
      response.Message = "Your character has been saved";
      return response;
    }

  
  }
}