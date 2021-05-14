using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChattingWithSignalR
{
    [HubName("chathub")]
    public class ChatHub : Hub
    {
        static Dictionary<string, string> connectedUsers = new Dictionary<string, string>();

        public override Task OnConnected()
        {
            Clients.All.connected(Context.ConnectionId, DateTime.Now.ToString());
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                Console.WriteLine(String.Format("Client {0} explicitly closed the connection.", Context.ConnectionId));
                Clients.All.disconnected(connectedUsers[Context.ConnectionId], DateTime.Now.ToString());
            }
            else
            {
                Console.WriteLine(String.Format("Client {0} timed out .", Context.ConnectionId));
                Clients.All.disconnected(connectedUsers[Context.ConnectionId], DateTime.Now.ToString());
            }
            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Connect(string name)
        {
            if (connectedUsers.ContainsKey(Context.ConnectionId))
            {
                connectedUsers[Context.ConnectionId] = name;
            }
            else
            {
                connectedUsers.Add(Context.ConnectionId, name);
            }
            Clients.Caller.connect(name);
        }
        public void Message(string name, string message)
        {
            Clients.All.message(name, message);
        }
    }
}