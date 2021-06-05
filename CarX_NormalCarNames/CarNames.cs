using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using DB;
using DB.Meta;
using DI;
using GameOverlay;
using HarmonyLib;

namespace CarX_NormalCarNames
{
    [BepInPlugin("com.kingfisher.carnames", "Car Names", "1.2.3")]
    public class CarNames : BaseUnityPlugin
    {
        // This class' instance
        public static CarNames Instance;

        // Makes the Plugin Logger available via the plugin instance above
        public ManualLogSource Log => Logger;

        public Harmony Harmony { get; }

        /// <inheritdoc />
        public CarNames()
        {
            // Set the class instance to this one
            Instance = this;

            Harmony = new Harmony(Info.Metadata.GUID);
            Harmony.PatchAll(Assembly.GetAssembly(typeof(CarNames)));
            
            // Tell the localization manager to initialize
            LocalizationManager.Init();
        }

        public void Start()
        {
            // Delay name replacement
            Invoke("ReplaceNames", 5f);
        }

        public void ReplaceNames()
        {
            // Set replace car names count to 0 so we can log how many we're successfully replaced
            var replaced = 0;

            // Loop over all cars in the game. This code is taken directly from the game's code so should return all cars, even ones that aren't visible, IE "Owl" or "Veneom GT500CR"
            foreach (var car in DependencyInjector.Resolve<BaseModel>().QueryAll<PlayerCar>())
            {
                // Set the internal car name to the one loaded in the LocalizationManager
                car.name = LocalizationManager.Get(car.name);

                // Increase replaced
                replaced++;
            }

            // Log replaced info, bit of a joke too from Adthefum1#9823
            Logger.LogInfo($"Replaced {replaced} Off Brand Aldi Names with Real Names");
        }
    }
}
