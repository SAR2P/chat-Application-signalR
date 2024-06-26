using ChatApp_SingleR.Repos;
using ChatModelsLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp_SingleR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatRepo _chatRepo;

        public ChatController(ChatRepo chatRepo)
        {
            _chatRepo = chatRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Chat>>> GetAllChatsAsync()
        {
            return Ok(await _chatRepo.GetAllChatsAsync());
        }


    }
}
