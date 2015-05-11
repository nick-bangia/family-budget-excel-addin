using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Microsoft.Win32;
using System.IO;

namespace FamilyBudget.Common.Config
{
    public static class AddInConfiguration
    {
        #region Properties
        private enum ConfigType
        {
            CONFIG_FILE,
            REGISTRY
        }
        private static readonly ConfigType DEFAULT_CONFIG_TYPE = ConfigType.CONFIG_FILE;
        #endregion

        #region Workbook Constants
        private static readonly string FAMILYBUDGET_ADDIN_KEY_PATH = "Software\\Microsoft\\Office\\Excel\\Addins\\FamilyBudget.AddIn";
        private static readonly string REGISTERED_WORKBOOK_PATH = "RegisteredWorkbookPath";
        private static readonly string API_ROOT_URL = "APIRootUrl";
        private static readonly string USERNAME = "Username";
        private static readonly string PASSWORD = "Password";
        #endregion

        #region AddIn Configurations
        public static string RegisteredWorkbookPath
        {
            get
            {
                return GetValue(REGISTERED_WORKBOOK_PATH, DEFAULT_CONFIG_TYPE);
            }
            set
            {
                if (value != null)
                {
                    SetValue(REGISTERED_WORKBOOK_PATH, value, DEFAULT_CONFIG_TYPE);
                }                
            }
        }
        #endregion

        #region API Configurations
        public static class APIConfiguration
        {
            public static string RootUri
            {
                get
                {
                    return GetValue(API_ROOT_URL, DEFAULT_CONFIG_TYPE);
                }

                set
                {
                    SetValue(API_ROOT_URL, value, DEFAULT_CONFIG_TYPE);
                }
            }

            public static string Username
            {
                get
                {
                    return GetValue(USERNAME, DEFAULT_CONFIG_TYPE);
                }

                set
                {
                    SetValue(USERNAME, value, DEFAULT_CONFIG_TYPE);
                }
            }

            public static string Password
            {
                get
                {
                    return GetValue(PASSWORD, DEFAULT_CONFIG_TYPE);
                }

                set
                {
                    SetValue(PASSWORD, value, DEFAULT_CONFIG_TYPE);
                }
            }

            public static class Routes
            {
                public static string GetPaymentMethods
                {
                    get { return GetValue("apiGetPaymentMethods", ConfigType.CONFIG_FILE); }
                }

                public static string AddPaymentMethods
                {
                    get { return GetValue("apiAddPaymentMethods", ConfigType.CONFIG_FILE); }
                }

                public static string UpdatePaymentMethods
                {
                    get { return GetValue("apiUpdatePaymentMethods", ConfigType.CONFIG_FILE); }
                }

                public static string GetAccounts
                {
                    get { return GetValue("apiGetAccounts", ConfigType.CONFIG_FILE); }
                }

                public static string AddAccounts
                {
                    get { return GetValue("apiAddAccounts", ConfigType.CONFIG_FILE); }
                }

                public static string UpdateAccounts
                {
                    get { return GetValue("apiUpdateAccounts", ConfigType.CONFIG_FILE); }
                }

                public static string GetCategories
                {
                    get { return GetValue("apiGetCategories", ConfigType.CONFIG_FILE); }
                }

                public static string AddCategories
                {
                    get { return GetValue("apiAddCategories", ConfigType.CONFIG_FILE); }
                }

                public static string UpdateCategories
                {
                    get { return GetValue("apiUpdateCategories", ConfigType.CONFIG_FILE); }
                }

                public static string GetSubcategories
                {
                    get { return GetValue("apiGetSubcategories", ConfigType.CONFIG_FILE); }
                }

                public static string AddSubcategories
                {
                    get { return GetValue("apiAddSubcategories", ConfigType.CONFIG_FILE); }
                }

                public static string UpdateSubcategories
                {
                    get { return GetValue("apiUpdateSubcategories", ConfigType.CONFIG_FILE); }
                }

                public static string GetLineItems
                {
                    get { return GetValue("apiGetAllLineItems", ConfigType.CONFIG_FILE); }
                }

                public static string SearchLineItems
                {
                    get { return GetValue("apiSearchLineItems", ConfigType.CONFIG_FILE); }
                }

                public static string AddLineItems
                {
                    get { return GetValue("apiAddLineItems", ConfigType.CONFIG_FILE); }
                }

                public static string UpdateLineItems
                {
                    get { return GetValue("apiUpdateLineItems", ConfigType.CONFIG_FILE); }
                }

                public static string DeleteLineItem
                {
                    get { return GetValue("apiDeleteLineItem", ConfigType.CONFIG_FILE); }
                }
            }
        }
        #endregion

        #region Private Methods
        private static string GetValue(string key, ConfigType configType)
        {
            // initialize the return value
            string value = null;

            // depending on the config type, get the value for key appropriately
            if (configType == ConfigType.CONFIG_FILE)
            {
                value = GetConfigValue(key);
            }
            else if (configType == ConfigType.REGISTRY)
            {
                value = GetRegistryValue(key);
            }

            return value;
        }

        private static void SetValue(string key, string value, ConfigType configType)
        {
            // depending on the config type, set the new value for key appropriately
            if (configType == ConfigType.CONFIG_FILE)
            {
                SetConfigValue(key, value);
            }
            else if (configType == ConfigType.CONFIG_FILE)
            {
                SetRegistryValue(key, value);
            }
        }

        private static void SetConfigValue(string key, string value)
        {
            // get the configuration file
            Configuration config = GetConfiguration();
            
            // apply the change to the configuration
            if (config != null && config.AppSettings != null && config.AppSettings.Settings.Count > 0)
            {
                KeyValueConfigurationElement setting = config.AppSettings.Settings[key];

                if (setting != null)
                {
                    // if the key is indexed to a valid setting, set it's value
                    setting.Value = value;

                    // save the configuration
                    config.Save();
                }
            }
        }

        private static string GetConfigValue(string key)
        {
            // get the configuration file
            Configuration config = GetConfiguration();

            // initialize the return value
            string value = String.Empty;

            // get the value for key in the configuration file if the appSettings section exists & has members
            if (config.AppSettings != null && config.AppSettings.Settings.Count > 0)
            {
                KeyValueConfigurationElement setting = config.AppSettings.Settings[key];
                value = setting != null ? setting.Value : String.Empty;
            }
            
            // return the value or String.Empty if not available
            return value;
        }

        private static Configuration GetConfiguration()
        {
            // get & open the correct config file
            ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
            configFile.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FamilyBudget.AddIn.dll.config");
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);

            return config;
        }

        private static string GetRegistryValue(string propertyName)
        {
            // initialize the return string
            string value = String.Empty;

            // Find the registry key for the addIn
            RegistryKey rkFamilyBudgetAddIn = GetAddInRegistryKey(writeEnabled: false);
            
            // get the value represented by the specified name, if we have a valid key
            if (rkFamilyBudgetAddIn != null)
            {
                value = (string) rkFamilyBudgetAddIn.GetValue(propertyName, String.Empty);
                
                // close the key once the value has been retrieved
                rkFamilyBudgetAddIn.Close();
            }

            return value;
        }

        private static void SetRegistryValue(string name, string value)
        {
            RegistryKey rkFamilyBudgetAddIn = GetAddInRegistryKey(writeEnabled: true);

            // set the value of the given property name, if the key is valid
            if (rkFamilyBudgetAddIn != null)
            {
                // only set the value of the property if it isn't null
                if (value != null)
                {
                    rkFamilyBudgetAddIn.SetValue(name, value);
                }                

                // close the key
                rkFamilyBudgetAddIn.Close();
            }
        }

        private static RegistryKey GetAddInRegistryKey(bool writeEnabled)
        {
            RegistryKey currentUserKey = Registry.CurrentUser.OpenSubKey(AddInConfiguration.FAMILYBUDGET_ADDIN_KEY_PATH, writeEnabled);
            RegistryKey localMachineKey = Registry.LocalMachine.OpenSubKey(AddInConfiguration.FAMILYBUDGET_ADDIN_KEY_PATH, writeEnabled);
            RegistryKey rkFamilyBudgetAddIn = currentUserKey != null ? currentUserKey : (localMachineKey != null ? localMachineKey : null);

            return rkFamilyBudgetAddIn;
        }
        #endregion
    }
}
