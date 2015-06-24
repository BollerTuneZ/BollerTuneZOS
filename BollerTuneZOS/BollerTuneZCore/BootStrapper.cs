using System;
using BollerTuneZCore.Plugin;
using BollerTuneZCore.Processors;
using Communication.Infrastructure.MessageProcessor;
using Communication.MessageProcessor;
using Communication.Serial;
using DataAccess.Addon;
using DataAccess.Repositories;
using Infrastructure.Communication;
using Infrastructure.Data;
using Infrastructure.Data.Settings;
using Infrastructure.JoystickApi;
using Infrastructure.Main;
using Infrastructure.Plugin;
using Plugin.Infrastructure.API.DataAccess;
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
            TinyIoCContainer.Current.Register<ISteeringProcessor, SteeringProcessor>();
		    TinyIoCContainer.Current.Register<ISerialDeviceHelper, SerialDeviceHelper>();
            TinyIoCContainer.Current.Register<IArgumentTranslator, ArgumentTranslator>();
            TinyIoCContainer.Current.Register<ISettingsRepository, SettingsRepository>().AsSingleton();
            TinyIoCContainer.Current.Register<IPluginRepository, PluginRepository>().AsSingleton();
            TinyIoCContainer.Current.Register<IPluginLoader, PluginLoader>().AsSingleton();
            TinyIoCContainer.Current.Register<IPluginSettingsRepository, PluginSettingsRepository>().AsMultiInstance();
            TinyIoCContainer.Current.Register<IPluginManager, PluginManager>().AsSingleton();
			TinyIoCContainer.Current.Register<BollerTuneZCore.Main> ();
		}
	}
}

