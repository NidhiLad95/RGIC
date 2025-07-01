using CRUDOperations;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace RGIC.Infrastructure
{
    public class Utilities
    {
        static string? environment;
        private static readonly Random random = new();
        private readonly ICrudOperationService _crudOperationService;
        private static readonly char[] separator = ['_', ' '];

        public Utilities(ICrudOperationService crudOperationService)
        {
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";
            _crudOperationService = crudOperationService;
        }

        public static string ToTitleCase(string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        public static string GetAppSettingValue(string key)
        {
            string appsettingvalue;
            if (environment == "Staging")
            {
                appsettingvalue = "staging";
                //IConfigurationRoot configuration = new ConfigurationBuilder(). 
                //    SetBasePath(AppDomain.CurrentDomain.BaseDirectory).
                //AddJsonFile($"appsettings.Staging.json").
                //Build();
                //appsettingvalue = configuration.GetValue<string>(key) ?? "";
                ////appsettingvalue = _dataProtectionService.Decrypt(appsettingvalue);
            }
            else
            {
                appsettingvalue = "else";
                //IConfigurationRoot configuration = new ConfigurationBuilder().
                //SetBasePath(AppDomain.CurrentDomain.BaseDirectory).
                //AddJsonFile($"appsettings.json").
                //Build();
                //appsettingvalue = configuration.GetValue<string>(key) ?? "";
            }
            return appsettingvalue;
        }

        public static string? GetDataFromXML(string fileName, string xmlHeaderTag, string key)
        {
            string cmlErrorMessageFilePath = Path.Combine("APIHelpers", "TextHelper", fileName);
            XDocument xdoc = XDocument.Load(cmlErrorMessageFilePath);

            string? value = xdoc?.Descendants(xmlHeaderTag)
                              .Select(node => (string?)node.Element(key))
                              .FirstOrDefault();

            string? message = "msg"; //string.IsNullOrWhiteSpace(value) ? MessageHelper.NoMessage : value;

            return message;

        }




        public static void LogDetails<T>(string message)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            ILog _logger = LogManager.GetLogger(typeof(T));
            _logger.Info(message);

        }

        public static IDictionary<string, object> GetValues(object obj)
        {
            return obj.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(obj))!;
        }



        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public static IDictionary<string, object> GetDynamicValues(dynamic row)
        {
            var result = (IDictionary<string, object>)row;

            return result;
        }


        private static string ToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string[] words = input.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = char.ToUpper(words[i][0]) + words[i][1..].ToLower();
            }

            return string.Join("", words);
        }
        private static void ReplaceKey(JToken token, string oldKey, string newKey)
        {
            if (token is JObject jsonObject)
            {
                var propertiesToRename = jsonObject.Properties().Where(property => property.Name == oldKey).ToList();
                foreach (var property in propertiesToRename)
                {
                    property.Replace(new JProperty(newKey, property.Value));
                }

                foreach (var property in jsonObject.Properties())
                {
                    ReplaceKey(property.Value, oldKey, newKey);
                }
            }
            else if (token is JArray jsonArray)
            {
                foreach (var item in jsonArray)
                {
                    ReplaceKey(item, oldKey, newKey);
                }
            }
        }
    }
}
