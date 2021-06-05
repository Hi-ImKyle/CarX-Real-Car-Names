using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace CarX_NormalCarNames
{
    [HarmonyPatch(typeof(FMODBaseScript), "CreateCarNamedNode")]
    public class CarEnginePatch
    {
        private static KeyValuePair<string, string> _kvp;
        public static void Prefix(FMODBaseScript __instance)
        {
            _kvp = LocalizationManager.LocalizationDictionary.FirstOrDefault(x =>
                x.Value == __instance.raceCar.metaInfo.name);

            if (string.IsNullOrWhiteSpace(_kvp.Key))
                return;

            __instance.raceCar.metaInfo.name = _kvp.Key;
        }

        public static void Postfix(FMODBaseScript __instance)
        {
            if (string.IsNullOrWhiteSpace(_kvp.Key))
                return;

            __instance.raceCar.metaInfo.name = _kvp.Value;
        }
    }
}
