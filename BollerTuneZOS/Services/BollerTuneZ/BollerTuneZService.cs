﻿using System;
using System.Threading;
using Communication.Infrastructure.MessageProcessor;
using Communication.Serial;
using Data.Settings;
using Infrastructure;
using Infrastructure.Communication;
using Infrastructure.JoystickApi;
using Infrastructure.JoystickApi.JoyStickEventArgs;
using Infrastructure.Main;
using Infrastructure.Services;
using Infrastructure.Util;
using log4net;

namespace Services.BollerTuneZ
{
    /// <summary>
    /// Aktuelle Service Klasse für den Bollerwagen aus 2015
    /// Jonas Ahlf 26.06.2015 23:07:05
    /// </summary>
    public class BollerTuneZService : IControllService
    {
        private static readonly ILog SLog = LogManager.GetLogger(typeof (BollerTuneZService));
        private readonly ISteeringProcessor _steeringProcessor;
        private readonly IBtzJoyStickController _joyStickController;
        private readonly IArgumentTranslator _argumentTranslator;
        private IBTZSocket _engineSocket;
        private IBTZSocket _steeringSocket;
        private volatile bool _socketsConnected;
        private SteeringSettings _steeringSettings;
        BtzArgument _networkType;

        public BollerTuneZService(ISteeringProcessor steeringProcessor, IBtzJoyStickController joyStickController, IArgumentTranslator argumentTranslator)
        {
            _steeringProcessor = steeringProcessor;
            _joyStickController = joyStickController;
            _argumentTranslator = argumentTranslator;
        }

        public void Start()
        {
            SLog.Debug("Start -> BollerTuneZ Service");
            Initialize();
        }

        public void Stop()
        {
            SLog.Debug("Stop -> BollerTuneZ Service");
            _joyStickController.Stop();
            _steeringProcessor.Stop();
        }

        #region Initialisation
        void Initialize()
        {
            ConnectToJoystick();
        }

        bool ConnectToJoystick()
        {
            SLog.Info("Activate Joystick");
            #region Events abonieren
            _joyStickController.OnEnabled += OnJoyStickEnabled;
            _joyStickController.OnMode += OnJoyStickMode;
            _joyStickController.OnPowerChanged += OnJoyStickPowerChanged;
            _joyStickController.OnShift += OnJoyStickShift;
            _joyStickController.OnSteeringChanged += OnJoyStickSteeringChanged;
            _joyStickController.OnSteeringSensitiv += OnJoyStickSteeringSensitive;
            #endregion
            return _joyStickController.Initialize();
        }
        #endregion


        #region BTZController Events
        private void OnJoyStickSteeringSensitive(object sender, EventArgs eventArgs)
        {
            var args = (JoyStickUpDownEventArgs)eventArgs;

            if (args.JoyEvent == EventUpDown.Up)
            {
                if (State.SteeringSensitive < 1.0f)
                {
                    State.SteeringSensitive += 0.1f;
                }
            }
            else
            {
                if (State.SteeringSensitive > 0.0f)
                {
                    State.SteeringSensitive -= 0.1f;
                }
            }
        }

        private void OnJoyStickSteeringChanged(object sender, EventArgs eventArgs)
        {
            var valueArgs = (JoyStickValueEventArgs)eventArgs;
            _steeringProcessor.SetPosition(valueArgs.Value);
        }

        private void OnJoyStickShift(object sender, EventArgs eventArgs)
        {
            var args = (JoyStickUpDownEventArgs)eventArgs;
            State.Shift = ShiftControll.DoShift(args.JoyEvent, State.Shift);
            SLog.DebugFormat("Changed Shift to {0}", State.Shift);
        }

        private void OnJoyStickPowerChanged(object sender, EventArgs eventArgs)
        {
            var valueArgs = (JoyStickValueEventArgs)eventArgs;
            var mappedValue = MathHelper.Map(valueArgs.Value, -32767, 32767, 0, 255);

            var calculatedSpeed = (int)(mappedValue * ShiftControll.ShiftInPercent(State.Shift));
        }

        private void OnJoyStickMode(object sender, EventArgs eventArgs)
        {
            //throw new NotImplementedException();
        }

        private void OnJoyStickEnabled(object sender, EventArgs eventArgs)
        {
            if (State.Enabled)
            {
                SLog.Debug("DISABLE DRIVE && STEERING");
                State.Enabled = false;
            }
            else
            {
                SLog.Debug("ENABLE DRIVE && STEERING");
                State.Enabled = true;
            }

        }
        #endregion
    }
}
