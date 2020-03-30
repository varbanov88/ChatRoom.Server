using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatRoom.Hubs
{
    public class ChatHub : Hub
    {
        public async Task AddToGroup(string groupName)
            => await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        public async Task SendFamilyMessage(string groupName, string nickName, string messageText)
            => await SendMessage(groupName, nickName, messageText, "receiveFamilyMessage");


        public async Task SendFriendsMessage(string groupName, string nickName, string messageText)
            => await SendMessage(groupName, nickName, messageText, "receiveFriendsMessage");

        public async Task SendWorkMessage(string groupName, string nickName, string messageText)
            => await SendMessage(groupName, nickName, messageText, "receiveWorkMessage");

        private async Task SendMessage(string groupName, string nickName, string messageText, string receiveMethod)
        {
            var msg = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Nickname = nickName,
                Text = messageText
            };
            await Clients.Group(groupName).SendAsync(receiveMethod, msg);
        }
    }
}
