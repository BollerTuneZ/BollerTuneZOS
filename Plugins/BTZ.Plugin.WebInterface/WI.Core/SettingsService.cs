using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebSocket4Net;
using WI.Data;
using WI.Infrastructure.Services;
using Quobject.SocketIoClientDotNet.Client;
namespace WI.Core
{
    /// <summary>
    /// Jonas Ahlf 25.06.2015 00:00:25
    /// </summary>
    public class SettingsService : IService
    {

        private static Socket _socket;
        private Action<object> _actionSteeringConfig;
        private SteeringConfigDto _steeringDto;

        public void Start()
        {
            Initialize();
        }

        public void Stop()
        {
            _socket.Disconnect();
        }

        void Initialize()
        {
            _socket = IO.Socket("http://192.168.2.118:8080");
            _socket.On(Socket.EVENT_ERROR, (data) => Console.WriteLine("WebInterface Socket Error {0}",data));
            EventSteeringConfig();
        }


        void EventSteeringConfig()
        {
            _actionSteeringConfig = OnSteeringConfig;
            _socket.On("SteeringConfig", _actionSteeringConfig);
        }

        private void OnSteeringConfig(object o)
        {
          
            try
            {
                string configStr = Convert.ToString(o);
                if (String.IsNullOrEmpty(configStr))
                {
                    Console.WriteLine("Error configStr is null");
                    return;
                }
                _steeringDto = JsonConvert.DeserializeObject<SteeringConfigDto>(configStr);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (_steeringDto == null)
            {
                Console.WriteLine("Error could not Deserialize steeringconfig ");
                return;
            }
            Console.WriteLine(String.Format("Settings changed to: {0},{1},{2}",  _steeringDto.SteeringRangeMax, _steeringDto.SteeringRangeMin, _steeringDto.SteeringCenter));
        }
    }
}
