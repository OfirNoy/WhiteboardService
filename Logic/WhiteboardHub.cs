using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.SignalR;
using WhiteboardService.DataTypes;

namespace WhiteboardService.Logic
{
    public class WhiteboardHub : Hub
    {   
        public Task<string> ConnectToBoard(string id = "")
        {
            var bid = WebApp.State.CreateWhiteboard(id);

            return Task.FromResult(WebApp.State.GetBoardData(bid ?? id));
        }
        public async Task UpdateBoard(string boardUpdateJson)
        {
            var boardUpdate = JsonSerializer.Deserialize<BoardUpdate>(boardUpdateJson);
            WebApp.State.UpdateBoard(boardUpdate);
            await Clients.Others.SendAsync("ReceiveUpdate", boardUpdateJson);            
        }        
        public Task<string> GetBoardList()
        {
            return Task.FromResult(JsonSerializer.Serialize(WebApp.State.GetBoardList()));
        }
    }
}