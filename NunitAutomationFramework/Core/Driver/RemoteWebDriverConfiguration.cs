using NUnit.Framework;
using NunitAutomationFramework.Helper.Configuration;
using NunitAutomationFramework.Test;
using System.Collections.Generic;

namespace NunitAutomationFramework.Core.Driver
{
    public static class RemoteWebDriverConfiguration
    {
        public static Dictionary<string, object> GetBrowserStackSettings()
        {
            Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
            browserstackOptions.Add("os", ConfigurationHelper.GetConfigurationByKey(Hooks.BrowserStackConfig, "OS"));
            browserstackOptions.Add("osVersion", ConfigurationHelper.GetConfigurationByKey(Hooks.BrowserStackConfig, "OSVersion"));
            browserstackOptions.Add("browserVersion", ConfigurationHelper.GetConfigurationByKey(Hooks.BrowserStackConfig, "BrowserVersion"));
            browserstackOptions.Add("video", ConfigurationHelper.GetConfigurationByKey(Hooks.BrowserStackConfig, "Video"));
            browserstackOptions.Add("userName", ConfigurationHelper.GetConfigurationByKey(Hooks.BrowserStackConfig, "UserName"));
            browserstackOptions.Add("accessKey", ConfigurationHelper.GetConfigurationByKey(Hooks.BrowserStackConfig, "Accesskey"));
            browserstackOptions.Add("browserName", TestContext.Parameters.Get("Browser"));
            browserstackOptions.Add("resolution", ConfigurationHelper.GetConfigurationByKey(Hooks.BrowserStackConfig, "Resolution"));
            return browserstackOptions;
        }
    }
}