using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Configuration;
using DB;
using DB.Meta;
using DI;
using SimpleJSON;

namespace CarX_NormalCarNames
{
    public class LocalizationManager
    {
        public static ConfigFile LocalizationConfigFile;
        public static bool IsInitialized { get; private set; }

        private static ConfigEntry<string> _localizationString;
        private static Dictionary<string, string> _localizationDictionary;
        public static string Get(string key)
        {
            if (!IsInitialized)
                return key;

            return _localizationDictionary.TryGetValue(key, out var str) ? str : key;
        }

        public static void Init()
        {
            LocalizationConfigFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "CarNames_Translations.cfg"), true);
            _localizationString = LocalizationConfigFile.Bind("Data", "JsonData", LongDefault(), "Key/Value Pairs of car names, left is in game car name, right is irl car name.");

            _localizationDictionary = JSON.Parse<Dictionary<string, string>>(_localizationString.Value);

            if (_localizationDictionary.Count >= 1)
                IsInitialized = true;
        }

        private static string LongDefault()
        {
            return
                "{\"Horizon GT4\":\"Nissan Skyline\", \"Ninja SX \":\"Ninja SX\", \"Hachi-Roku\": \"Toyota AE86\", \"Veneom GT500CR\":\"Veneom GT500CR\", \"Thunderstrike\": \"Dodge Charger\", \"Godzilla R3\":\"Nissan Skyline R33 V-spec\", \"Bimmy P30\": \"BMW E30 M3\", \"Falcon RZ\":\"Mazda RX7\", \"Wellington S20\":\"Nissan Silvia S13\", \"Wanderer L30\":\"Toyota Supra RZ\", \"STG 440\":\"STG 440\", \"Hornet GT\":\"Chevrolet Camaro\", \"Spector RS\":\"Nissan Silvia S15\", \"Steel DM\":\"Steel DM\", \"Piranha X\":\"Nissan 350z\", \"Burner JDM\":\"Toyota Chaser V\", \"Thor 8800\":\"Thor 8800\", \"Black Jack X22\":\"Hoonicorn v1\", \"Phoenix NX\": \"Nissan 180sx\", \"Asura M1\":\"Toyota GT86\", \"Judge\":\"Judge\", \"Caravan G6\":\"Volvo\", \"Voodoo\":\"Dodge Viper\", \"Raven RV8\":\"Maloo R8\", \"DTM 46\":\"BMW E46 M3\", \"Syberia SWI\": \"Subaru WRX STI\", \"EVA MR\":\"Mitsubishi Evo 9\", \"Atlas GT\":\"Nissan GT-R\", \"Panther M5\": \"MazdaMX5\", \"Cobra GT530\":\"Mustang GT350\", \"UDM 3\":\"BMW M3 E92 GTS\", \"Loki 4M\":\"BMW M4\", \"Magnum RT\":\"Dodge Challenger RT\", \"VZ 210\": \"VAZ 2107\", \"Samurai II\":\"Toyota Mark 2\", \"Fujin SX\":\"Nissan Silvia S14\", \"Rabe\":\"Rabe\", \"Mifune\":\"Toyota Altezza\", \"SpeedLine GT\":\"Audi R8\", \"Wütend\": \"BMW M3 E36\", \"Void\":\"Void\", \"Falcon FC 90-s\":\"Mazda RX7 FC\", \"Interceptor\":\"Interceptor\", \"Cobra \":\"Cobra \", \"Midnight\":\"Nissan S30Z\", \"Last Prince\":\"Nissan Skyline R32\", \"Hunter\":\"BMW M2\", \"Pirate\":\"Nissan Laurel C33\", \"Owl\":\"Owl\", \"Shark GT\":\"BMW M5 E60\", \"Python RX\":\"Python RX\", \"Panther M5 90-s\": \"Mazda MX5 90\", \"EVA X\":\"Mitsubishi Evo X\", \"Dakohosu\":\"Dakohosu\", \"Kanniedood\":\"Datsun 620\", \"Betsy\":\"Betsy\", \"Sorrow\":\"Lexus SC300\", \"SpaceKnight\":\"Lexus LFA\", \"VZ212\":\"VAZ 2102\", \"Black Jack X150\":\"Hoonitruck F150\", \"Syberia WDC\":\"Subaru WRX 2008\", \"Imperior\":\"Mercedes Benz 190E EVO 2\", \"Karnage 7C\":\"Corvette C7\", \"Bug Catcher\":\"Bug Catcher\", \"Mercher KLC\":\"Mercher KLC\", \"Flanker F\":\"Flanker F\", \"Lynx\":\"Mazda RX8\", \"Hakosuka\":\"Nissan Skyline 2000 GTX\", \"Rolla ZR\":\"Toyota Corolla 2019\", \"Warrior\":\"Warrior\", \"Carrot II\":\"Toyota Mark 2 JZX100\", \"Cargo\":\"Drift Truck\", \"Spark ZR\":\"Corvette C6\", \"Shadow XTR\":\"Mustang RTX\", \"Bandit\":\"BMW M5 E34\", \"Warden\":\"Mercedes Benz CLK63\", \"Hachi-Go\": \"Toyota AE86 Coupe\", \"Dacohosu\": \"Toyota Celica\", \"Zismo\":\"Nissan 370z\", \"Eleganto\":\"Lexus RCF\", \"Patron GT\":\"Mercedes Benz AMG GT\", \"Unicorn\":\"Nissan Stagea\", \"Corona\":\"Toyota Mark 2 JZX81\", \"HotRod\":\"Hot Rod\", \"Solar\": \"Mitsubishi Eclipse '99\", \"Vanguard\":\"Audi RS6 Avant C7\", \"Black Fox\":\"Mustang 1990\", \"Nomad GT\":\"Toyota Supra 2020\", \"Inferno\":\"Dodge Charger 2020\"}";
        }
    }
}
