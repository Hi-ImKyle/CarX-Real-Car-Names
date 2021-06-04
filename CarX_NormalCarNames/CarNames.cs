using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using DB;
using DB.Meta;
using DI;
using UnityEngine.SocialPlatforms;

namespace CarX_NormalCarNames
{
    [BepInPlugin("com.kingfisher.carnames", "Car Names", "1.0")]
    public class CarNames : BaseUnityPlugin
    {
        public static CarNames Instance;

        public ManualLogSource Log => Logger;

        /// <inheritdoc />
        public CarNames()
        {
            Instance = this;
            
            LocalizationManager.Init();
        }

        public void Awake()
        {
            var replaced = 0;
            foreach (var car in DependencyInjector.Resolve<BaseModel>().QueryAll<PlayerCar>())
            {
                car.name = LocalizationManager.Get(car.name);
                replaced++;
            }

            Logger.LogInfo($"Replaced {replaced} Off Brand Aldi Names with Real Names");
        }
    }
}
