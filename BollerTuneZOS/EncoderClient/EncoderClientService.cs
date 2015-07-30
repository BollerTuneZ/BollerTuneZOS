using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using EncoderClient.Data;
using Newtonsoft.Json;

namespace EncoderClient
{
    /// <summary>
    /// Client verbindung zum Arduino EncoderServer
    /// Jonas Ahlf 28.07.2015 12:58:18
    /// </summary>
    public class EncoderClientService
    {
        private Socket _clientSocket;
        private bool Connected;
        private Identity _arduinoIdentity;
        private volatile bool Run;
        private Thread _encoderServiceThread;
        #region PublicEvents
        public delegate void EncoderDataReceived(EncoderData data);
        public event EncoderDataReceived OnEncoderDataReceived;
        #endregion

        public EncoderClientService()
        {
            _encoderServiceThread = new Thread(EncoderService);
        }

        public bool Connect(IPAddress address,int port)
        {
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(address,port);
                 _clientSocket = new Socket(AddressFamily.InterNetwork, 
                SocketType.Stream, ProtocolType.Tcp );
                _clientSocket.Connect(remoteEP);

                return GetIdentity();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void RunService()
        {
            if (!Run)
            {
                Run = true;
                _encoderServiceThread.Start();
            }
        }

        public void StopService()
        {
            Run = false;
            _encoderServiceThread.Abort();
        }

        public void SetEncoder(Encoder encoder, int value)
        {
            byte[] payload;
            switch (encoder)
            {
                case Encoder.Non:
                    return;
                case Encoder.Steering:
                    payload = Encoding.Default.GetBytes(JsonConvert.SerializeObject(new EncoderSetCommand
                    {
                        ECMODE = "ECS",
                        Value = value
                    }));
                    break;
                case Encoder.Motor:
                    payload = Encoding.Default.GetBytes(JsonConvert.SerializeObject(new EncoderSetCommand
                    {
                        ECMODE = "ECM",
                        Value = value
                    }));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("encoder");
            }

            _clientSocket.Send(payload);
        }

        bool GetIdentity()
        {
            var payload = JsonConvert.SerializeObject(new ArduinoCommand {Command = "SHOW_IDENTITY"});
            byte[] message = Encoding.Default.GetBytes(payload);
            int byteSent = _clientSocket.Send(message);
            byte[] readBuffer = new byte[100];
            int bytesRec = _clientSocket.Receive(readBuffer);
            var readLine = Encoding.ASCII.GetString(readBuffer, 0, bytesRec);
            if (String.IsNullOrWhiteSpace(readLine))
            {
                Console.WriteLine("Could not get Identity");
                return false;
            }
            try
            {
                var identity = JsonConvert.DeserializeObject<Identity>(readLine);
                if (identity == null)
                {
                    return false;
                }
                if (identity.Name == "STEERING")
                {
                    _arduinoIdentity = identity;
                    _arduinoIdentity.ConnectionTime = DateTime.Now;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        void EncoderService()
        {
            
            if (!_clientSocket.Connected)
            {
                Console.WriteLine("Client not Connected abourt EncoderService");
                Run = false;
                return;
            }
            var networkstream = new NetworkStream(_clientSocket);
            var streamReader = new StreamReader(networkstream);
            while (Run)
            {
                var line = streamReader.ReadLine();
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var encoderData = JsonConvert.DeserializeObject<EncoderData>(line);
                if (encoderData != null)
                {
                    OnEncoderDataReceived(encoderData);
                }
            }
        }
    }

    internal class ArduinoCommand
    {
        public string Command { get; set; }
    }

    public enum Encoder
    {
        Non,
        Steering,
        Motor
    }
}
