using dotnet.rpg.Dtos.Character;
namespace dotnet.rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        public Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        public Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        public Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        public Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);
        public Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}