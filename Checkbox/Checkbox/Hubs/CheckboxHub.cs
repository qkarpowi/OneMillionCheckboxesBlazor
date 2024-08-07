using Microsoft.AspNetCore.SignalR;
using System.Collections;

namespace Checkbox.Hubs
{
    public class CheckboxHub : Hub
    {
        private readonly ILogger<CheckboxHub> _logger;

        private CheckboxService _service;

        public CheckboxHub(ILogger<CheckboxHub> logger, CheckboxService checkboxService)
        {
            _logger = logger;
            _service = checkboxService;
        }

        public async Task SendMessage(int location, bool isChecked)
        {
            _service.checkboxes.Set(location, isChecked);
            _logger.LogInformation("Checkbox {location} was set to {isChecked}", location, isChecked); 
            await Clients.All.SendAsync("ReceiveMessage", location, isChecked);
        }
    }
}
