using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using SimpleJSON;
using UnityEngine.Networking;

namespace CarX_NormalCarNames
{
    public class LocalizationManager
    {
        // Localization Config File
        public static ConfigFile LocalizationConfigFile;

        // To tell whether or not the localization was initialized properly
        public static bool IsInitialized { get; private set; }

        // User config override
        private static ConfigEntry<string> _localizationString;
        public static Dictionary<string, string> LocalizationDictionary { get; private set; }
        public static string Get(string key)
        {
            // If IsInitialized is false, just return the provided key value, meaning we haven't or couldn't set up the names
            if (!IsInitialized)
                return key;

            // Attempt to get the value of a given key, if none is found, the provided key value is returned to prevent cars from having no name
            var value = LocalizationDictionary.FirstOrDefault(x =>
                x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            
            return value.Value ?? key;
        }

        public static void Init()
        {
            // Create the translation config file
            LocalizationConfigFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "CarNames_Translations.cfg"), true);

            // Bind Json Data to give user the option to override any car names
            _localizationString = LocalizationConfigFile.Bind("Data", "JsonData", "{\"CarXCarName1\":\"IrlCarName1\", \"CarXCarName2\":\"IrlCarName2\"}", "Override Key/Value Pairs of car names, left is in game car name, right is irl car name.");

            // Fetch Normal Name set from Github, provided by me; Kyle#0420
            LocalizationDictionary = JSON.Parse<Dictionary<string, string>>(GetDefaultsFromGithub());

            // Check if override json data contains data
            if (!string.IsNullOrWhiteSpace(_localizationString.Value))
            {
                // Populate override dictionary
                var overrideDictionary = JSON.Parse<Dictionary<string, string>>(_localizationString.Value);

                // Loop over the override dictionary where each key from the override dictionary exists within the official dictionary
                var overridableNames = overrideDictionary.Where(kvp => LocalizationDictionary.ContainsKey(kvp.Key)).ToArray();
                foreach (var kvp in overridableNames)
                {
                    // Replace official with user override
                    LocalizationDictionary[kvp.Key] = kvp.Value;
                }

                CarNames.Instance.Log.LogInfo($"Overriding {overridableNames.Length} car names with user provided ones");
            }

            // Return if the name dictionary doesn't have anything in it
            if (!LocalizationDictionary.Any()) 
                return;

            // Count all where the value is different to the key
            var diffCount = LocalizationDictionary.Count(x => !x.Key.Equals(x.Value));

            // Count all where the value is the same to the key
            var sameCount = LocalizationDictionary.Count(x => x.Key.Equals(x.Value));

            // Log counts
            CarNames.Instance.Log.LogInfo($"Loaded {diffCount} different car names, {sameCount} same names");

            // Set IsInitialized to true so plugin knows it can replace names
            IsInitialized = true;
        }

        private static string GetDefaultsFromGithub()
        {
            var jsonPath = Path.Combine(Paths.ConfigPath, "CarNames.json");

            // Create a new UnityWebRequest GET method
            using (var uwr = UnityWebRequest.Get("https://raw.githubusercontent.com/Hi-ImKyle/CarX-Real-Car-Names/main/names.txt"))
            {
                // Send the request
                uwr.SendWebRequest();

                // Wait till the request has finished
                while (!uwr.isDone) { }

                // If it's had an error, return an empty string
                if (!uwr.isHttpError)
                {
                    CarNames.Instance.Log.LogInfo($"Got Updated Car Names from Github");

                    var data = uwr.downloadHandler.text;
                    File.WriteAllText(jsonPath, data);

                    // Else return the downloaded text
                    return data;
                }

                CarNames.Instance.Log.LogError($"Github Fetch Error: {uwr.error}. Trying Local File...");
            }

            if (File.Exists(jsonPath))
            {
                CarNames.Instance.Log.LogInfo($"Got Car Names from Local File");
                return File.ReadAllText(jsonPath);
            }

            CarNames.Instance.Log.LogError($"Failed to get names from Local File");
            return string.Empty;
        }
    }
}
