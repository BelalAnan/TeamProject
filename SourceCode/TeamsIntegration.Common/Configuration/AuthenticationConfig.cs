// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace TeamsIntegration.Common.Configuration
{
    public class AuthenticationConfig
    {
        public string Instance { get; set; } = "https://login.microsoftonline.com/{0}";
        public string ApiUrl { get; set; } = "https://graph.microsoft.com/";
        public static IConfigurationRoot Configuration;

        public List<Organizations> Organizations { get; set; }

        public static AuthenticationConfig ReadFromJsonFile(string path)
        {
           
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path);

            Configuration = builder.Build();
            return Configuration.Get<AuthenticationConfig>();
        }
        /// <summary>
        /// Gets config value
        /// example "QB_Settings"
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Get(string name)
        {
            string appSettings = Configuration[name];
            return appSettings;
        }
        /// <summary>
        /// Gets config value from spesific section
        /// example "QB_Settings":"Log4NetConfigPath"="conf.txt" 
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string Get(string Section, string Key)
        {
            return Configuration.GetSection(Section).GetSection(Key).Value;
        }

        /// <summary>
        /// Get appSettings value from configuration file
        /// </summary>
        /// <typeparam name="T">Type of the expected value</typeparam>
        /// <param name="key">key of appSettings to get value of</param>
        /// <returns>value of appSettings key</returns>
        /// <exception cref="NullReferenceException">If appSettings configuration key is not found in web.config</exception>
        public static T GetAppSettingsValue<T>(string key, T defaultValue)
        {
            return Configuration.GetValue<T>(key, defaultValue);
        }
        public static T GetSection<T>(string sectionKey)
        {
            return Configuration.GetSection(sectionKey).Get<T>();
        }
    }



}

