using CustomerDistance_Calculator.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.Services
{
    public class ConfigService : IConfigService
    {
        private const string _fileName = "config.json";

        public Task<string?> GetSubscriptionKey()
        {
            var config = GetConfig();
            return Task.FromResult(config?.SubscriptionKey);
        }

        public async Task SaveSubscriptionKey(string key)
        {
            ConfigDto? config = GetConfig();
            if(config != null)
            {
                if (config.SubscriptionKey == key) return;
            }
            else
                config = new ();

            config.SubscriptionKey = key;
            await File.WriteAllTextAsync(_fileName, JsonSerializer.Serialize(config));
        }

        private static ConfigDto? GetConfig()
        {
            try
            {
                if (!File.Exists(_fileName))
                    return null;
                string rawText =  File.ReadAllText(_fileName);
                ConfigDto config = JsonSerializer.Deserialize<ConfigDto>(rawText) ?? throw new JsonException();
                return config;
            }
            catch (Exception) {
                return null;
            }
        }
    }
}
