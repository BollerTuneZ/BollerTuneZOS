﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using BollerTuneZCore.Res;
using Communication.Infrastructure;
using Communication.Infrastructure.MessageProcessor;
using Communication.Serial;
using Data.Plugin;
using Data.Settings;
using Infrastructure;
using Infrastructure.Communication;
using Infrastructure.Data;
using Infrastructure.Data.Settings;
using Infrastructure.JoystickApi;
using Infrastructure.JoystickApi.JoyStickEventArgs;
using Infrastructure.Main;
using Infrastructure.Plugin;
using Infrastructure.Util;
using log4net;
using System.Threading;


namespace BollerTuneZCore
{
	public class Main
	{
		static readonly ILog SLog = LogManager.GetLogger (typeof(Main));
	    private readonly ISettingsRepository _settingsRepository;
		readonly ISteeringProcessor _steeringProcessor;
		readonly ISteeringConfigMessageProcessor _steeringConfigProcessor;
		readonly IEngineProcessor _engineProcessor;
	    private readonly IBTZJoyStickController _joyStickController;
	    private readonly ISerialDeviceHelper _serialDeviceHelper;
	    private readonly IArgumentTranslator _argumentTranslator;
	    private readonly IPluginLoader _pluginLoader;
	    private readonly IPluginRepository _pluginRepository;
	    private List<BtzArgument> _args;
        BtzArgument _networkType;
	    private IBTZSocket _steeringSocket;
	    private IBTZSocket _engineSocket;
	    private volatile bool SocketsConnected;
	    private SteeringSettings _steeringSettings;
	    public Main(ISteeringProcessor steeringProcessor,
            IEngineProcessor engineProcessor,
            ISerialDeviceHelper serialDeviceHelper,
            IArgumentTranslator argumentTranslator,
            IBTZJoyStickController joyStickController,
            ISettingsRepository settingsRepository,
            IPluginLoader pluginLoader,
            IPluginRepository pluginRepository)
	    {
	        _steeringProcessor = steeringProcessor;
	        _engineProcessor = engineProcessor;
	        _serialDeviceHelper = serialDeviceHelper;
	        _argumentTranslator = argumentTranslator;
	        _joyStickController = joyStickController;
	        _settingsRepository = settingsRepository;
	        _pluginLoader = pluginLoader;
	        _pluginRepository = pluginRepository;
	    }

	    public void Run(string[] args= null)
		{
            _args = new List<BtzArgument>();

	        if (args != null)
	        {
	            foreach (var s in args)
	            {
	                var parsedCommand = _argumentTranslator.GetArgument(s);
	                if (parsedCommand != BtzArgument.Non)
	                {
	                    _args.Add(parsedCommand);
	                }
	            }
	        }
	        Console.Write(String.Format("{0} \n{1}", Properties.Version.Default.ApplicationName,
	            Properties.Version.Default.VersionName));
			Thread.Sleep (2000);
	        while (true)
	        {
	            var input = Console.ReadLine().ToLower();
	            switch (input)
	            {
                    case "init -server":
                        Initialize();
                        break;
                    case "plugin -load -a":
                        LoadAllPlugins();
                        break;
                    case "plugin -i":
                        InstallPlugin();
                        break;
                    case "plugin -run":
                        RunPlugin();
                        break;
                    default:
                        Console.WriteLine(String.Format("Unknown Command {0}",input));
                        break;
	            }
	        }
		}

		void Initialize()
		{
			_serialDeviceHelper.OnDeviceFound += OnSerialDeviceFound;
		    if (!_args.Any())
		    {
                SLog.Debug("No Startarguments set");
		        _networkType = GetConnectionSetting();
                SLog.InfoFormat("Networktype set to {0}",_networkType);
		        if (_networkType == BtzArgument.Serial)
		        {
                    SLog.Info("Start discover Serial Devices");
                    _serialDeviceHelper.StartDiscover();
		        }
		    }
		    else
		    {
		        if (_args.Any(d => d == BtzArgument.Serial))
		        {
		            _networkType = BtzArgument.Serial;
                    _serialDeviceHelper.StartDiscover();
                }
                else if (_args.Any(d => d == BtzArgument.Network))
		        {
		            _networkType = BtzArgument.Network;
		        }
            }
            SLog.Info("Waiting for sockets to connect");
		    while (!SocketsConnected)
		    {
                Thread.Sleep(100);
		    }
            SLog.Info("Sockets connected");
		    if (_networkType == BtzArgument.Serial)
		    {
		        SLog.Debug("Stop searching for serial devices");
                _serialDeviceHelper.StopDiscover();
		    }
            _steeringProcessor.Initialize(_steeringSocket);
            _engineProcessor.Initialize(_engineSocket);
            SLog.Info("Activate Joystick and start Drive...!");
            #region Events abonieren
            _joyStickController.OnEnabled += OnJoyStickEnabled;
            _joyStickController.OnMode += OnJoyStickMode;
            _joyStickController.OnPowerChanged += OnJoyStickPowerChanged;
            _joyStickController.OnShift += OnJoyStickShift;
            _joyStickController.OnSteeringChanged += OnJoyStickSteeringChanged;
            _joyStickController.OnSteeringSensitiv += OnJoyStickSteeringSensitive;
            #endregion
            _joyStickController.Initialize();
            SLog.Info("Service will now start");
            _joyStickController.Run();
            _steeringProcessor.Start();
            _engineProcessor.Start();
		}

        #region BTZController Events
        private void OnJoyStickSteeringSensitive(object sender, EventArgs eventArgs)
        {
            var args = (JoyStickUpDownEventArgs) eventArgs;

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
	        var valueArgs = (JoyStickValueEventArgs) eventArgs;
            _steeringProcessor.SetPosition(valueArgs.Value);
	    }

	    private void OnJoyStickShift(object sender, EventArgs eventArgs)
	    {
	        var args = (JoyStickUpDownEventArgs) eventArgs;
	        State.Shift = ShiftControll.DoShift(args.JoyEvent, State.Shift);
            SLog.DebugFormat("Changed Shift to {0}",State.Shift);
	    }

	    private void OnJoyStickPowerChanged(object sender, EventArgs eventArgs)
	    {
	        var valueArgs = (JoyStickValueEventArgs) eventArgs;
	        var mappedValue = MathHelper.Map(valueArgs.Value, -32767, 32767, 0, 255);

	        var calculatedSpeed = (int) (mappedValue*ShiftControll.ShiftInPercent(State.Shift));
            _engineProcessor.SetSpeed(calculatedSpeed);
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
        BtzArgument GetConnectionSetting()
	    {
	        while (true)
	        {
                Console.WriteLine(CoreRes.EnterNetworkCommand);
	            var input = Console.ReadLine();
	            var command = _argumentTranslator.GetArgument(input);
	            if (command == BtzArgument.Non)
	            {
                    Console.WriteLine(String.Format(CoreRes.BadNetworkCommand, input));
                    continue;
	            }
	            if (command == BtzArgument.Network || command == BtzArgument.Serial)
	            {
	                return command;
	            }
                Console.WriteLine(String.Format(CoreRes.BadNetworkCommand, input));
	        }
	    }

        #region Events
        private void OnSerialDeviceFound(object sender, EventArgs eventArgs)
        {
            if (eventArgs == null)
            {
                SLog.Error("OnSerialDeviceFound Error events args are NULL");
                return;
            }
            var args = (EventArgsDeviceFound) eventArgs;
            if (args.Device.Type == DeviceType.Drive)
            {
                SLog.InfoFormat("Engine Interface found at {0}",args.Device.ComPort);
                _engineSocket = args.Device;
            }else if (args.Device.Type == DeviceType.Steering)
            {
                SLog.InfoFormat("Steering Interface found at {0}", args.Device.ComPort);
                _steeringSocket = args.Device;
            }
            if (_steeringSocket != null && _engineSocket != null)
            {
                SocketsConnected = true;
            }
        }
        #endregion
        #region Member

        #region plugin

	    void RunPlugin()
	    {
            Console.WriteLine("Type in the name of plugin for running");
	        var input = Console.ReadLine();
	        if (String.IsNullOrWhiteSpace(input))
	        {
	            SLog.Warn("Bad Userinput");
                return;
	        }
	        var plugin = _pluginLoader.LoadBtzPlugins().FirstOrDefault(n => n.GetIdentity().Name.ToLower().Equals(input.ToLower()));
	        if (plugin == null)
	        {
	            Console.WriteLine("Could not find plugin with name:{0}",input);
                return;
	        }
            _pluginLoader.StartPlugin(plugin);
	    }

        void LoadAllPlugins()
	    {
	        var plugins = _pluginLoader.LoadBtzPlugins();
	        foreach (var btzPlugin in plugins)
	        {
	            var tempIdentity = btzPlugin.GetIdentity();
	            var tempMessage = String.Format("Plugin: \n Name:{0} \n Author:{1} \n Version:{2}", tempIdentity.Name,
	                tempIdentity.Author, tempIdentity.VersionName);
                Console.WriteLine(tempMessage);
	        }
        }

	    void InstallPlugin()
	    {
	        const string patternInstall = "<pluginName>%<pluginDirectory>%<executiveLibrary>";
            Console.WriteLine("Use pattern {0} to install plugin",patternInstall);

	        while (true)
	        {
	            var input = Console.ReadLine();
	            if (String.IsNullOrWhiteSpace(input))
	            {
	                SLog.Error("Userinput was NULL/Empty");
                    continue;
	            }
	            var split = input.Split('%');
	            if (split.Count() < 3)
	            {
                    SLog.ErrorFormat("Userinput was Invalid {0}, should be {1}",input,patternInstall);
	                continue;
	            }
	            var name = split[0];
                var dir = split[1];
                var executiveLib = split[2];
                
                Console.WriteLine(String.Format("Name:{0} \n Directory:{1} \n ExecutiveLibrary:{2}",name,dir,executiveLib));
                Console.WriteLine("Are these information correct ? Y/N");
	            input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(input))
                {
                    SLog.Error("Userinput was NULL/Empty");
                    continue;
                }
                if (input.ToLower() != "y")
                {
                    Console.WriteLine("Use pattern {0} to install plugin", patternInstall);
                    continue;
                }
	            var tempPluginInfo = new PluginInfo
	            {
	                Name = name,
	                PluginLocation = dir,
	                ExecutiveLibrary = executiveLib
	            };
                if (!_pluginRepository.InstallPlugin(tempPluginInfo, dir))
	            {
	                SLog.WarnFormat("Plugin {0} could not be installed",name);
                    Console.WriteLine("Plugin {0} could not be installed",name);
	            }
                else
                {
                    Console.WriteLine("Plugin {0} installed",name);
                }
	        }
	    }
        #endregion
        #endregion
    }
}

