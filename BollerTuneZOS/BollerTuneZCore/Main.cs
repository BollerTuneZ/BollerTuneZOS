using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Infrastructure.Main;
using Infrastructure.Plugin;
using Infrastructure.Services;
using log4net;
using System.Threading;


namespace BollerTuneZCore
{
	public class Main
	{
		static readonly ILog SLog = LogManager.GetLogger (typeof(Main));
	    private readonly IArgumentTranslator _argumentTranslator;
	    private List<BtzArgument> _args;
	    private IList<IControllService> _controllerServices; 
	    public Main(IArgumentTranslator argumentTranslator)
	    {
	        _argumentTranslator = argumentTranslator;
            _controllerServices = new List<IControllService>();
	    }

	    public void Run(string[] args= null)
        {
            SLog.InfoFormat("{0} {1} started", Properties.Version.Default.ApplicationName, Properties.Version.Default.VersionName);
            Console.WriteLine("{0} {1}", Properties.Version.Default.ApplicationName, Properties.Version.Default.VersionName);
            #region const commands

	        const string cInitServerDefault = "-run -service default";
	        const string cShutDown = "-shutdown";
	        const string cPlugin = "-pm";
	        const string cStopServiceDefault = "-stop -service default";
            #endregion  
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
	        bool run = true;
	        while (true)
	        {
                Console.WriteLine("Write command: {0},{1},{2},{3}",
                cInitServerDefault,
                cPlugin,
                cShutDown,
                cStopServiceDefault);
	            var input = Console.ReadLine();
	            if (String.IsNullOrWhiteSpace(input))
	            {
	                Console.WriteLine("Userinput was null or empty");
                    continue;
	            }
	            input = input.ToLower();
	            switch (input)
	            {
                    case cInitServerDefault:
                        SLog.Info("Starting BollerTuneZ Service");
	                    var bollerTunezService = TinyIoC.TinyIoCContainer.Current.Resolve<IControllService>("btz_service");
                        _controllerServices.Add(bollerTunezService);
	                    new Thread(bollerTunezService.Start).Start();
                        break;
                    case cStopServiceDefault:
	                    foreach (var controllerService in _controllerServices)
	                    {
	                        controllerService.Stop();
	                    }
                        break;
                    case cPlugin:
	                    var pluginManager = TinyIoC.TinyIoCContainer.Current.Resolve<IPluginManager>();
                        pluginManager.EnterPluginManager();
                        break;
                    case cShutDown:
	                    run = false;
                        break;
                    default:
                        Console.WriteLine(String.Format("Unknown Command {0}",input));
                        break;
	            }
	            if (!run)
	            {
	                Console.WriteLine("Do you really want to shutdown ? Y/N");
	                var input2 = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(input2)) return;
                    if (input2.ToLower() != "y")
	                {
                        Console.WriteLine("Shutdown abourted");
	                    run = true;
	                }
	                if (!run)
	                {
                        SLog.Info("BollerTuneZ OS is going to shutdown..");
                        for (int i = 3; i >= 0; i--)
                        {
                            Console.WriteLine("Shutdown in {0} seconds.", i);
                            Thread.Sleep(1000);
                        }
                        break;
	                }
	            }
	        }
		}
    }
}

