using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Communication.Infrastructure;
using Infrastructure.Communication;

namespace Communication.Ethernet
{
    /// <summary>
    /// Ethernet IBtzSocket
    /// Jonas Ahlf 01.07.2015 21:49:12
    /// </summary>
    public class EthernetDevice : IBtzSocket
    {

        private Socket _clientSocket;

        public EthernetDevice()
        {
        }

        public EthernetDevice(Socket clientSocket)
        {
            _clientSocket = clientSocket;
        }

        public void SendData(byte[] payload)
        {
            throw new NotImplementedException();
        }

        public byte[] ReceiveData()
        {
            throw new NotImplementedException();
        }
    }
}
