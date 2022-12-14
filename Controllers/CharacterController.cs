using dotnet.rpg.Dtos.Character;
using dotnet.rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dotnet.rpg.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class CharacterController : ControllerBase
	{
    private readonly ICharacterService characterService;

		public CharacterController(ICharacterService characterService)
		{
      this.characterService = characterService;
			
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
		{
			return Ok(await characterService.AddCharacter(newCharacter));
		}

		[HttpDelete]
		public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
		{
			var response = await characterService.DeleteCharacter(id);
			if(!response.Success){
				return NotFound(response);
			}
			return Ok(response);
		}

		[HttpGet("GetAll")]
		public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
		{
		
			//int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
			return Ok(await characterService.GetAllCharacters());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
		{
			return Ok(await characterService.GetCharacterById(id));
		}

		[HttpPut]
		public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
		{
			var response = await characterService.UpdateCharacter(updatedCharacter);
			if(!response.Success){
				return NotFound(response);
			}
			return Ok(response);
		}
	}
}
