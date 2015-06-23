using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;
namespace SocketIOTest
{
    public class SocketIoTest
    {
        public void Test()
        {
            var socket = IO.Socket("http://192.168.2.117:8080");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                Console.WriteLine("Connected");
            });

            socket.On("Lenkung", (data) =>
            {
                Console.WriteLine(data);
            });

        }
    }
}
