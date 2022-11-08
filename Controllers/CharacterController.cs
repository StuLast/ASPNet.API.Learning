using Microsoft.AspNetCore.Mvc;

namespace dotnet.rpg.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CharacterController : ControllerBase
	{
		private static Character knight = new Character();

		[HttpGet(Name = "GetCharacter")]
		public ActionResult<Character> Get()
		{
			return Ok(knight);
		}
	}
}
