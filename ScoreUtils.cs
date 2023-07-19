using AccessAbility.Configuration;
using IPA.Loader;
using System;
using System.Linq;
using UnityEngine;

namespace AccessAbility
{
    internal class ScoreUtils
    {
        internal static bool bl_installed = true;
        internal static bool ss_installed = true;
        internal static bool cc_installed = true;
        internal static bool leaderboards_installed = true;

        internal static MultiplayerModeSelectionFlowCoordinator multiplayer_1;
        internal static MultiplayerModeSelectionViewController multiplayer_2;
        internal static bool is_multiplayer_active = false;


        internal static void CheckForMods()
        {
            try
            {
                var metadatas = PluginManager.EnabledPlugins.Where(x => x.Id == "BeatLeader");
                bl_installed = metadatas.Count() > 0;
            }
            catch (Exception)
            {
                bl_installed = false;
            }

            try
            {
                var metadatas = PluginManager.EnabledPlugins.Where(x => x.Id == "ScoreSaber");
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

            Plugin.Log.Debug("BL install: " + bl_installed);
            Plugin.Log.Debug("SS install: " + ss_installed);
            Plugin.Log.Debug("CC install: " + cc_installed);

            leaderboards_installed = bl_installed || ss_installed || cc_installed;
            //Plugin.Log.Debug("Leaderboards installed: " + leaderboards_installed);
        }


        internal static void BSEvents_gameSceneLoaded()
        {
            //Plugin.Log.Debug("Game Scene Loaded");

            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return;
            }


            if (PluginConfig.Instance.play_without_score &&
               (PluginConfig.Instance.blue_mode != 0 || PluginConfig.Instance.red_mode != 0 ||
                PluginConfig.Instance.yeet_arcs || PluginConfig.Instance.yeet_chains || PluginConfig.Instance.yeet_fail ||
                PluginConfig.Instance.yeet_bombs || PluginConfig.Instance.yeet_walls || PluginConfig.Instance.yeet_duck_walls))
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("AccessAbility");
                return; // prevent multiple messages
            }

            if (BS_Utils.Gameplay.Gamemode.IsPartyActive)
            {
                return;
            }

            if (leaderboards_installed)
            {
                if ((PluginConfig.Instance.blue_mode == 2 || PluginConfig.Instance.red_mode == 2) && PluginConfig.Instance.dissolve_distance <= 3)
                {
                    BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("AccessAbility");
                    return; // prevent multiple messages
                }

                if (PluginConfig.Instance.yeet_arcs || PluginConfig.Instance.yeet_chains)
                {
                    BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("AccsesAbility");
                }
            }
        }


        internal static void BSEvents_menuSceneActive()
        {
            multiplayer_1 = Resources.FindObjectsOfTypeAll<MultiplayerModeSelectionFlowCoordinator>().FirstOrDefault();
            if (multiplayer_1 != null)
            {
                //Plugin.Log.Debug("Found MultiplayerModeSelectionFlowCoordinator");
                multiplayer_1.didFinishEvent += Multiplayer_1_didFinishEvent;
            }

            multiplayer_2 = Resources.FindObjectsOfTypeAll<MultiplayerModeSelectionViewController>().FirstOrDefault();
            if (multiplayer_2 != null)
            {
                //Plugin.Log.Debug("Found MultiplayerModeSelectionViewController");
                multiplayer_2.didFinishEvent += Multiplayer_2_didFinishEvent;
            }
        }

        private static void Multiplayer_1_didFinishEvent(MultiplayerModeSelectionFlowCoordinator obj)
        {
            if (obj != null)
            {
                //Plugin.Log.Debug("MultiplayerModeSelectionFlowController didFinish");
                Plugin.Log.Debug("Exit MP");
                is_multiplayer_active = false;
            }
        }

        private static void Multiplayer_2_didFinishEvent(MultiplayerModeSelectionViewController arg1, MultiplayerModeSelectionViewController.MenuButton arg2)
        {
            //Plugin.Log.Debug("MultiplayerModeSelectionViewController didFinish");
            Plugin.Log.Debug("In MP");
            is_multiplayer_active = true;
        }
    }
}
