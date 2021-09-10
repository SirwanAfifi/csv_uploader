using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using csv_uploader.Hubs;
using csv_uploader.Models;
using Microsoft.AspNetCore.SignalR;

namespace csv_uploader.Services
{
    public class CSVUploaderService
    {
        private readonly IHubContext<UploaderHub, IUploaderHub> _hubContext;
        public CSVUploaderService(IHubContext<UploaderHub, IUploaderHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task ParseFile(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                // Validation

                // Find GeoCode

                // Save To DB

                await _hubContext.Clients.All.BroadcastMessage(user.firstName, $"Updated at: {DateTime.Now}");
            }
        }
    }
}