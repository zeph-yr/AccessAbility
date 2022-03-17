using AccessAbility.Configuration;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Loader;
using System;
using System.Linq;
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
        
        internal static bool ss_installed = true;
        internal static bool cc_installed = true;


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
            CheckForMods();
            BS_Utils.Utilities.BSEvents.gameSceneLoaded += BSEvents_gameSceneLoaded;
            ApplyHarmonyPatches();

            BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("AccessAbility", "AccessAbility.ModifierUI.bsml", ModifierUI.instance);
        }

        private void CheckForMods()
        {
            try
            {
                var metadatas  = PluginManager.EnabledPlugins.Where(x => x.Id == "ScoreSaber");
                ss_installed = metadatas.Count() > 0;
            }
            catch (Exception)
            {
                ss_installed = false;
            }

            try
            {
                var metadatas = PluginManager.EnabledPlugins.Where(x => x.Id == "CustomCampaigns");
                cc_installed = metadatas.Count() > 0;
            }
            catch (Exception)
            {
                cc_installed = false;
            }

            Plugin.Log.Debug("SS install: " + ss_installed);
            Plugin.Log.Debug("CC install: " + cc_installed);
        }


        private void BSEvents_gameSceneLoaded()
        {
            Plugin.Log.Debug("Game Scene Loaded");

            if (PluginConfig.Instance.neversubmit_enabled && 
               (PluginConfig.Instance.blue_mode != 0 || PluginConfig.Instance.red_mode != 0 || 
                PluginConfig.Instance.yeet_bombs || PluginConfig.Instance.yeet_walls || PluginConfig.Instance.yeet_duck_walls))
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("AccessAbility");
            }

            else if ((ss_installed || cc_installed) && (PluginConfig.Instance.blue_mode == 2 || PluginConfig.Instance.red_mode == 2) && PluginConfig.Instance.dissolve_distance <= 3)
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("AccessAbility");
            }
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
