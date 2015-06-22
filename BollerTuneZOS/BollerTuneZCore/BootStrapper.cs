using System;
using Communication.Infrastructure.MessageProcessor;
using Communication.MessageProcessor;
using Communication.Serial;
using Infrastructure.Communication;
using Infrastructure.Data.Settings;
using Infrastructure.JoystickApi;
using Infrastructure.Main;
using TinyIoC;
using Communication.Infrastructure;
using Communication;
using Infrastructure;
using JoystickApi;
using Testing;


namespace BollerTuneZCore
{
	public static class BootStrapper
	{
		static BootStrapper ()
		{
		}
		public static void Run()
		{
			//TinyIoCContainer.Current.Register<INetworkConfig,NetworkConfig> ();
			TinyIoCContainer.Current.Register<IMessagePacker,MessagePacker> ();
			TinyIoCContainer.Current.Register<IUDPClientService,UDPClientService> ().AsMultiInstance();
			TinyIoCContainer.Current.Register<IUDPService,UDPService> ();
			TinyIoCContainer.Current.Register<IJoyStickHandler,JoyStickHandler> ();
			TinyIoCContainer.Current.Register<IBTZJoyStickController,BTZJoyStickController> ();
			TinyIoCContainer.Current.Register<JoyStickTesting> ();
			TinyIoCContainer.Current.Register<CommunicationTest> ();
			TinyIoCContainer.Current.Register<ISteeringConfigMessageProcessor,SteeringConfigMessageProcessor> ().AsSingleton();
			TinyIoCContainer.Current.Register<IEngineProcessor,EngineProcessor> ();
		    TinyIoCContainer.Current.Register<ISerialDeviceHelper, SerialDeviceHelper>();
            TinyIoCContainer.Current.Register<IArgumentTranslator, ArgumentTranslator>();
			TinyIoCContainer.Current.Register<BollerTuneZCore.Main> ();
		}
	}
}

