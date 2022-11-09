using AutoMapper;
using Microsoft.EntityFrameworkCore;

using dotnet.rpg.Data;
using dotnet.rpg.Dtos.Character;
using System.Security.Claims;


namespace dotnet.rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
      _context = context;
    }
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var response = new Models.ServiceResponse<List<GetCharacterDto>>();
        
        Character character = _mapper.Map<Character>(newCharacter);
        _context.Characters.Add(character);
        await _context.SaveChangesAsync();
        response.Data = await _context.Characters
          .Select(c => _mapper.Map<GetCharacterDto>(c))
          .ToListAsync();
        return response;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
      var response = new ServiceResponse<List<GetCharacterDto>>();
      try 
      { 
        Character character = await _context.Characters.FirstAsync(c => c.Id == id);
        _context.Characters.Remove(character);
        await _context.SaveChangesAsync();
        var dbCharacters = await _context.Characters.ToListAsync();
        response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
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
      var response = new ServiceResponse<List<GetCharacterDto>>();
      var uid = GetUserId();
      var dbCharacters = await _context.Characters
        .Where(c => c.Id == GetUserId())
        .ToListAsync();
      //var dbCharacters = await _context.Characters.ToListAsync();
      response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return response;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
      var response = new ServiceResponse<GetCharacterDto>();
      var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
      response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
      return response;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      var response = new ServiceResponse<GetCharacterDto>();
      try {
      Character character = await _context.Characters
        .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

       //_mapper.Map(updatedCharacter, character);

      character.Name = updatedCharacter.Name;
      character.HitPoints = updatedCharacter.HitPoints;
      character.Strength =  updatedCharacter.Strength;
      character.Defence = updatedCharacter.Defence;
      character.Intelligence = updatedCharacter.Intelligence;
      character.Class = updatedCharacter.Class;

      await _context.SaveChangesAsync();
      var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
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

    private int GetUserId()
    {
        var userIdentifier = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
		return int.Parse(userIdentifier);
	}
 

  }
}