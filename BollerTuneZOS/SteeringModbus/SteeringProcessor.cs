using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication.Infrastructure.MessageProcessor;
using Data.Settings;
using EncoderClient;
using EncoderClient.Data;
using Infrastructure.Data.Settings;
using log4net;
using Encoder = EncoderClient.Encoder;

namespace SteeringModbus
{
    /// <summary>
    /// Fake SteeringProcessor
    /// Jonas Ahlf 28.07.2015 18:29:25
    /// </summary>
    public class SteeringProcessor : ISteeringProcessor
    {

        private static readonly ILog SLog = LogManager.GetLogger(typeof (SteeringProcessor));
        private readonly ISettingsRepository _settingsRepository;
        private SteeringSettings _steeringSettings;
        private EncoderClientService _encoderService;

        public SteeringProcessor(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
            _steeringSettings = _settingsRepository.RetriveSteeringSettings();
        }

        public void Initialize()
        {
            SLog.Debug("Initialize Steering Processor(fake)");
            _encoderService = new EncoderClientService();
            _encoderService.OnEncoderDataReceived += OnEncoderDataReceived;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void SetPosition(int position)
        {
            throw new NotImplementedException();
        }

        public void SetEnabled(bool enabled)
        {
            SLog.DebugFormat("Enabled set to {0}",enabled);
        }

        public void SetEncoderPosition(EncoderType encoderType, int position)
        {
            SLog.DebugFormat("Set Encoder {0} to {1}", encoderType,position);
            switch (encoderType)
            {
                case EncoderType.Steering:
                    _encoderService.SetEncoder(Encoder.Steering, position);
                    break;
                case EncoderType.Motor:
                    _encoderService.SetEncoder(Encoder.Motor, position);
                    break;
                default:
                    return;
            }
        }

        #region Events
        private void OnEncoderDataReceived(EncoderData data)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
