using AccessAbility.Configuration;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace AccessAbility
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        public const string HarmonyId = "com.zephyr.BeatSaber.AccessAbility";
        internal static readonly HarmonyLib.Harmony harmony = new HarmonyLib.Harmony(HarmonyId);
        
        
        [Init]
        public Plugin(IPALogger logger, Config config)
        {
            Instance = this;
            Plugin.Log = logger;
            Plugin.Log?.Debug("Logger initialized.");

            PluginConfig.Instance = config.Generated<PluginConfig>();
        }

        [OnEnable]
        public void OnEnable()
        {
            ScoreUtils.CheckForMods();

            BS_Utils.Utilities.BSEvents.gameSceneLoaded += ScoreUtils.BSEvents_gameSceneLoaded;
            BS_Utils.Utilities.BSEvents.menuSceneActive += ScoreUtils.BSEvents_menuSceneActive;

            ApplyHarmonyPatches();

            BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("AccessAbility", "AccessAbility.ModifierUI.bsml", ModifierUI.instance);
        }


        [OnDisable]
        public void OnDisable()
        {
            RemoveHarmonyPatches();
        }

        internal static void ApplyHarmonyPatches()
        {
            try
            {
                Plugin.Log?.Debug("Applying Harmony patches.");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Plugin.Log?.Error("Error applying Harmony patches: " + ex.Message);
                Plugin.Log?.Debug(ex);
            }
        }

        internal static void RemoveHarmonyPatches()
        {
            try
            {
                harmony.UnpatchSelf();
            }
            catch (Exception ex)
            {
                Plugin.Log?.Error("Error removing Harmony patches: " + ex.Message);
                Plugin.Log?.Debug(ex);
            }
        }
    }
}
