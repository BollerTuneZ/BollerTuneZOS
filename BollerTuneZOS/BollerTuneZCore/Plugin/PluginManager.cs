using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Plugin;
using Infrastructure.Data;
using Infrastructure.Plugin;
using log4net;
using Plugin.Infrastructure;

namespace BollerTuneZCore.Plugin
{
    /// <summary>
    /// Jonas Ahlf 24.06.2015 22:21:53
    /// </summary>
    public class PluginManager : IPluginManager
    {
        private static readonly ILog SLog = LogManager.GetLogger(typeof (PluginManager));
        private readonly IPluginRepository _pluginRepository;
        private readonly IPluginLoader _pluginLoader;
        private IList<IBtzPlugin> _plugins;
        private static string[] commandList = new[]
            {
                CExit,
                CInstallPlugin,
                CRemovePlugin,
                CRunPlugin
            };
        public PluginManager(IPluginRepository pluginRepository, IPluginLoader pluginLoader)
        {
            _pluginRepository = pluginRepository;
            _pluginLoader = pluginLoader;
        }

        public void EnterPluginManager()
        {
            Initialize();

            
            bool run = true;
            while (true)
            {
                PrintCommands();
                var userInput = Console.ReadLine();
                switch (userInput)
                {
                    case CExit:
                        run = false;
                        break;
                    case CInstallPlugin:
                        InstallPlugin();
                        break;
                    case CRemovePlugin:
                        DeinstallPlugin();
                        break;
                    case CRunPlugin:
                        RunPlugin();
                        break;
                    default:
                        Console.WriteLine("Command {0}, not found",userInput);
                        PrintCommands();
                        continue;
                }
                if(!run){ Console.WriteLine("Exit PluginManager..."); break;}
            }
        }

        void Initialize()
        {
            if (_plugins == null ||!_plugins.Any())
            {
                SLog.Debug("Plugins were not loaded, begin loading plugins");
                _plugins = _pluginLoader.LoadBtzPlugins();
                if (_plugins != null)
                {
                    SLog.DebugFormat("Plugins loaded:{0}",_plugins.Count);
                }
                else
                {
                    SLog.Error("Could not load any Plugins");
                }
            }
        }

        void PrintCommands()
        {
            foreach (var s in commandList)
            {
                Console.WriteLine("Command:{0}",s);
            }
        }

        void InstallPlugin()
        {
            const string patternInstall = "<pluginName>%<pluginDirectory>%<executiveLibrary>";
            Console.WriteLine("Use pattern {0} to install plugin", patternInstall);

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
                    SLog.ErrorFormat("Userinput was Invalid {0}, should be {1}", input, patternInstall);
                    continue;
                }
                var name = split[0];
                var dir = split[1];
                var executiveLib = split[2];

                Console.WriteLine(String.Format("Name:{0} \n Directory:{1} \n ExecutiveLibrary:{2}", name, dir, executiveLib));
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
                    SLog.WarnFormat("Plugin {0} could not be installed", name);
                    Console.WriteLine("Plugin {0} could not be installed", name);
                }
                else
                {
                    Console.WriteLine("Plugin {0} installed", name);
                    _plugins = _pluginLoader.LoadBtzPlugins();
                    break;
                }
            }
        }

        void DeinstallPlugin()
        {
            const string question = "Type in the index for removing Plugin";
            var pluginInfo = GetPluginInfo(question);
            if (pluginInfo == null)
            {
                return;
            }
            Console.WriteLine("Do you whish to delete this Plugin Y/N {0} ?", pluginInfo.Name);
            var readLine = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(readLine))
            {
                Console.WriteLine("Bad input");
                return;
            }
            if (!readLine.ToLower().Equals("y"))
            {
                Console.WriteLine("Removing Plugin {0} canceled", pluginInfo.Name);
                return;
            }
            Console.WriteLine("Removing Plugin {0}...", pluginInfo.Name);
            var deleted = _pluginRepository.DeletePlugin(pluginInfo);

            if (deleted)
            {
                Console.WriteLine("Plugin {0} has been removed", pluginInfo.Name);
            }
            else
            {
                Console.WriteLine("Plugin {0} could not be removed", pluginInfo.Name);
            }

        }

        void RunPlugin()
        {
            const string question = "Type in the index for starting Plugin";
            var plugin = GetPlugin(question);
            if (plugin == null)
            {
                SLog.Error("PluginStart: Plugin is NULL");
                return;
            }
            _pluginLoader.StartPlugin(plugin);
        }

        IBtzPlugin GetPlugin(string question)
        {
            Console.WriteLine("List of Plugins:");

            for (int i = 0; i < _plugins.Count; i++)
            {
                var tempPlugin = _plugins[i];
                var message = "[{0}] {1} , {2}";
                var identity = tempPlugin.GetIdentity();
                Console.WriteLine(message, i, identity.Name, identity.Author);
            }
            Console.WriteLine(question);
            var input = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(input))
            {
                SLog.Warn("Bad Userinput");
                return null;
            }

            int index;
            if (!int.TryParse(input, out index))
            {
                Console.WriteLine("{0} is not a number ;)", input);
                return null;
            }

            if (index < 0 || index > (_plugins.Count - 1))
            {
                Console.WriteLine("Index {0} is not in range of 0 - {1}", index, _plugins.Count);
                return null;
            }
            var plugin = _plugins[index];
            return plugin;
        }

        PluginInfo GetPluginInfo(string question)
        {
            Console.WriteLine("List of Plugins:");
            var pluginInfos = _pluginRepository.GetPluginList().PluginInfos;
            for (int i = 0; i < pluginInfos.Count; i++)
            {
                var tempPluginInfo = pluginInfos[i];
                var message = "[{0}] {1}";
                Console.WriteLine(message, i, tempPluginInfo.Name);
            }
            Console.WriteLine(question);
            var input = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(input))
            {
                SLog.Warn("Bad Userinput");
                return null;
            }

            int index;
            if (!int.TryParse(input, out index))
            {
                Console.WriteLine("{0} is not a number ;)", input);
                return null;
            }

            if (index < 0 || index > (pluginInfos.Count - 1))
            {
                Console.WriteLine("Index {0} is not in range of 0 - {1}", index, pluginInfos.Count);
                return null;
            }
            var plugin = pluginInfos[index];
            return plugin;
        }

        #region Commands

        private const string CInstallPlugin = "install";
        private const string CRunPlugin = "run";
        private const string CRemovePlugin = "remove";
        private const string CExit = "-quit";


        #endregion
    }
}
