using Microsoft.AspNetCore.Mvc;
using SpamBotApi.Models;
using SpamBotApi.Models.Dtos;
using SpamBotApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpamBotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiverController : ControllerBase
    {
        private readonly IReceiverService _receiverService;

        public ReceiverController(IReceiverService receiverService)
            => _receiverService = receiverService;

        [HttpGet]
        public async Task<List<GetReceiverDto>> GetAllReceivers()
            => await _receiverService.GetAllReceivers();

        [HttpGet]
        [Route("{id}")]
        public async Task<GetReceiverDto> GetReceiverById(int id)
            => await _receiverService.GetReceiverByIdAsync(id);

        [HttpPut]
        public async Task UpdateReceiver(UpdateReceiverDto receiver)
            => await _receiverService.UpdateReceiverAsync(receiver);

        [HttpPost]
        public async Task CreateReceiver(CreateReceiverDto receiver)
            => await _receiverService.CreateReceiverAsync(receiver);
    }
}
