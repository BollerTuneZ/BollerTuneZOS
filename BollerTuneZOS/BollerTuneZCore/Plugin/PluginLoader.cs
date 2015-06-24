using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.Plugin;
using Infrastructure.Data;
using Infrastructure.Plugin;
using log4net;
using Plugin.Infrastructure;
using Plugin.Infrastructure.API.DataAccess;

namespace BollerTuneZCore.Plugin
{
    /// <summary>
    /// Jonas Ahlf 23.06.2015 14:34:11
    /// </summary>
    public class PluginLoader :IPluginLoader, IBtzPluginHost
    {
        private const string PluginInterfaceName = "Plugin.Infrastructure.IBtzPlugin";
        private readonly IPluginRepository _pluginRepository;
        private static readonly ILog SLog = LogManager.GetLogger(typeof (PluginLoader));

        private IList<Tuple<IBtzPlugin, Thread>> _loadedPlugins = new List<Tuple<IBtzPlugin, Thread>>(); 

        public PluginLoader(IPluginRepository pluginRepository)
        {
            _pluginRepository = pluginRepository;
        }

        public IList<IBtzPlugin> LoadBtzPlugins()
        {
            var pluginList = _pluginRepository.GetPluginList();
            if (pluginList.PluginInfos == null || !pluginList.PluginInfos.Any())
            {
                SLog.Warn("No Plugins found");
                return null;
            }
            IList<IBtzPlugin> tempPlugins = new List<IBtzPlugin>();
            foreach (var pluginInfo in pluginList.PluginInfos)
            {
                var tempPlugin = LoadPluginFromInfo(pluginInfo);
                if (tempPlugin != null)
                {
                    tempPlugins.Add(tempPlugin);
                }
            }
            return tempPlugins;
        }

        public void StartPlugin(IBtzPlugin plugin)
        {
            try
            {
                if (!plugin.Initialize())
                {
                    SLog.ErrorFormat("Plugin {0} initialization failed",plugin.GetIdentity().Name);
                    return;
                }
                Thread tempPluginThread = new Thread(plugin.OnStart);
                tempPluginThread.Start();
                _loadedPlugins.Add(new Tuple<IBtzPlugin, Thread>(plugin,tempPluginThread));
                SLog.DebugFormat("Plugin loaded and started {0}",plugin.GetIdentity().Name);
            }
            catch (Exception e)
            {
                SLog.ErrorFormat("Could not load/start Plugin {0}",e);
            }
        }

        public void ClosePlugin(string plugin)
        {
            try
            {
                int idToKill = -1;
                foreach (var loadedPlugin in _loadedPlugins)
                {
                    if (loadedPlugin.Item1.GetIdentity().Name == plugin)
                    {
                        loadedPlugin.Item1.OnStop();
                        loadedPlugin.Item2.Abort();
                        idToKill = _loadedPlugins.IndexOf(loadedPlugin);
                        break;
                    }
                }
                if (idToKill != -1)
                {
                    SLog.DebugFormat("Removing Plugin from loaded Plugins {0}",idToKill);
                    _loadedPlugins.RemoveAt(idToKill);
                }
            }
            catch (Exception e)
            {
                SLog.ErrorFormat("Error at killing plugin {0}",e);
            }
        }

        IBtzPlugin LoadPluginFromInfo(PluginInfo info)
        {
            var fileInfo = new FileInfo(info.ExecutiveLibrary);
            if (!fileInfo.Extension.Equals(".dll"))
            {
                SLog.ErrorFormat("ExecutiveLibrary {0} is not Type is wrong expected .dll got {1}",info.ExecutiveLibrary,fileInfo.Extension);
                return null;
            }
            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom(fileInfo.FullName);
            }
            catch (Exception e)
            {
                SLog.ErrorFormat("Could not load Assembly {0}, {1}",info.ExecutiveLibrary,e);
                return null;
            }
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsPublic)
                {
                    if (!type.IsAbstract)
                    {
                        Type typeInterface = type.GetInterface(PluginInterfaceName, true);

                        if (typeInterface == null)
                        {
                            SLog.ErrorFormat("Interfacetype is null {0}",info.ExecutiveLibrary);
                            return null;
                        }
                        var plugin = (IBtzPlugin) Activator.CreateInstance(assembly.GetType(type.ToString()));

                        if (plugin == null)
                        {
                            SLog.ErrorFormat("Plugin is NULL {0}",info.Name);
                        }
                        else
                        {
                            plugin.Host = this;
                        }
                        typeInterface = null;
                        return plugin;
                    }
                }
            }
            return null;
        }

        public IPluginSettingsRepository GetSettingsRepository()
        {
            var repo = TinyIoC.TinyIoCContainer.Current.Resolve<IPluginSettingsRepository>();
            return repo;
        }
    }
}
