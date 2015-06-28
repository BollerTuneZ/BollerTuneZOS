using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Data;
using Plugin.Infrastructure.API.DataAccess;
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
        //SaveSettings
        private PluginSteeringSettings _steeringSettingsRemote;
        private static Socket _socket;
        private Action<object> _actionSteeringConfig;
        private Action<object> _actionSteeringMotorConfig;
        private Action<object> _actionEngineConfig;
        private Action<object> _actionSaveSettings;
        private SteeringConfigDto _steeringDto;
        private SteeringMotorConfigDto _steeringMotorConfig;
        private EngineConfigDto _engineConfig;
        private IPluginSettingsRepository _settingsRepository;

        public SettingsService(IPluginSettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public void Start()
        {
            Initialize();
            Console.WriteLine("SettingsService Initialized");
        }

        public void Stop()
        {
            _socket.Disconnect();
        }

        void Initialize()
        {
            _steeringSettingsRemote = _settingsRepository.LoadSteeringSettings();
            _socket = IO.Socket("http://192.168.2.118:8080");
            _socket.On(Socket.EVENT_ERROR, (data) =>Console.WriteLine("WebInterface Socket Error {0}",data));
            _socket.On(Socket.EVENT_CONNECT,
                () =>
                {
                    EngineSpeedRangeDto obj = new EngineSpeedRangeDto
                    {
                        SteeringSpeedMax_MaxDOM = 100,
                        SteeringSpeedMax_MinDOM = -100,
                        SteeringSpeedMin_MaxDOM = 100,
                        SteeringSpeedMin_MinDOM = -100
                    };
                    var jObj = new JObject
                        {
                            {"SteeringSpeedMax_MaxDOM", 10000},
                            {"SteeringSpeedMax_MinDOM", -10000},
                            {"SteeringSpeedMin_MaxDOM", 10000},
                            {"SteeringSpeedMin_MinDOM", -10000},
                        };
                    JObject jObject = JObject.FromObject(obj);
                    _socket.Emit("SteeringMotorConfigDOM", jObj);
                    Console.WriteLine("Send SteeringMotorConfigDOM");
                });
            _actionSteeringConfig = OnSteeringConfig;
            _socket.On("SteeringConfig", _actionSteeringConfig);
            _actionSteeringMotorConfig = OnSteeringMotorConfig;
            _socket.On("SteeringMotorConfig", _actionSteeringMotorConfig);
            _actionEngineConfig = OnEngineConfig;
            _socket.On("EngineConfig", _actionEngineConfig);
            _actionSaveSettings = OnSettingsSave;
        }

        void SaveSteeringSettings()
        {
            _steeringSettingsRemote.SteeringCenter = _steeringDto.SteeringCenter;
            _steeringSettingsRemote.SteeringMax = _steeringDto.SteeringRangeMax;
            _steeringSettingsRemote.SteeringMin = _steeringDto.SteeringRangeMin;
            _steeringSettingsRemote.SteeringPositionDiffTolerance = _steeringDto.SteeringToleranz;
            _settingsRepository.SaveSteeringSettings(_steeringSettingsRemote);
        }
        
        private void OnSettingsSave(object o)
        {
            SaveSteeringSettings();
            _settingsRepository.SaveSteeringSettings(_steeringSettingsRemote);
        }

        #region Config Events
        private void OnEngineConfig(object o)
        {
            try
            {
                string configStr = Convert.ToString(o);
                if (String.IsNullOrEmpty(configStr))
                {
                    Console.WriteLine("Error configStr is null");
                    return;
                }
                _engineConfig = JsonConvert.DeserializeObject<EngineConfigDto>(configStr);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (_steeringMotorConfig == null)
            {
                Console.WriteLine("Error could not Deserialize steeringconfig ");
                return;
            }
        }

        private void OnSteeringMotorConfig(object o)
        {
            try
            {
                string configStr = Convert.ToString(o);
                if (String.IsNullOrEmpty(configStr))
                {
                    Console.WriteLine("Error configStr is null");
                    return;
                }
                _steeringMotorConfig = JsonConvert.DeserializeObject<SteeringMotorConfigDto>(configStr);
                SaveSteeringSettings();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (_steeringMotorConfig == null)
            {
                Console.WriteLine("Error could not Deserialize steeringconfig ");
                return;
            }
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
                SaveSteeringSettings();
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
        }
        #endregion
    }
}
