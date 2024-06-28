using ChatApp_SingleR.Repos;
using ChatModelsLibrary.DTOs;
using ChatModelsLibrary.Models;
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

        [HttpGet("group-chats")]
        public async Task<IActionResult> GetAllChatsAsync()
        {
            return Ok(await _chatRepo.GetGroupChatsAsync());
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync() 
            => Ok(await _chatRepo.GetAvailableUserAsync());

        [HttpPost("individual")]
        public async Task<IActionResult> GetIndividualAsync(RequestChatDTO requestChatDTO)
            => Ok(await _chatRepo.GetIndividualChatsAsync(requestChatDTO));
    }
}
