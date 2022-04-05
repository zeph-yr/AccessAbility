using AccessAbility.Configuration;
using IPA.Loader;
using System;
using System.Linq;
using UnityEngine;

namespace AccessAbility
{
    internal class ScoreUtils
    {
        internal static bool ss_installed = true;
        internal static bool cc_installed = true;

        internal static MultiplayerModeSelectionFlowCoordinator multiplayer_1;
        internal static MultiplayerModeSelectionViewController multiplayer_2;
        internal static bool is_multiplayer_active = false;


        internal static void CheckForMods()
        {
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

            Plugin.Log.Debug("SS install: " + ss_installed);
            Plugin.Log.Debug("CC install: " + cc_installed);
        }


        internal static void BSEvents_gameSceneLoaded()
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


        internal static void BSEvents_menuSceneActive()
        {
            multiplayer_1 = Resources.FindObjectsOfTypeAll<MultiplayerModeSelectionFlowCoordinator>().FirstOrDefault();
            if (multiplayer_1 != null)
            {
                Plugin.Log.Debug("Found MultiplayerModeSelectionFlowCoordinator");
                multiplayer_1.didFinishEvent += Multiplayer_1_didFinishEvent;
            }

            multiplayer_2 = Resources.FindObjectsOfTypeAll<MultiplayerModeSelectionViewController>().FirstOrDefault();
            if (multiplayer_2 != null)
            {
                Plugin.Log.Debug("Found MultiplayerModeSelectionViewController");
                multiplayer_2.didFinishEvent += Multiplayer_2_didFinishEvent;
            }
        }

        private static void Multiplayer_1_didFinishEvent(MultiplayerModeSelectionFlowCoordinator obj)
        {
            if (obj != null)
            {
                Plugin.Log.Debug("MultiplayerModeSelectionFlowController didFinish");
                is_multiplayer_active = false; // Leaving MP
            }
        }

        private static void Multiplayer_2_didFinishEvent(MultiplayerModeSelectionViewController arg1, MultiplayerModeSelectionViewController.MenuButton arg2)
        {
            Plugin.Log.Debug("MultiplayerModeSelectionViewController didFinish");
            is_multiplayer_active = true;
        }


        /*private void Multiplayer_2_didActivateEvent(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            is_multiplayer_active = true;
        }

        private void MainMenuViewController_didFinishEvent(MainMenuViewController arg1, MainMenuViewController.MenuButton arg2)
        {
            if (arg2 == MainMenuViewController.MenuButton.Multiplayer)
            {
                is_multiplayer_active = true;
            }
            else
            {
                is_multiplayer_active = false;
            }
        }*/
    }
}
