using AccessAbility.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AccessAbility
{
    [HarmonyPatch(typeof(BeatmapDataTransformHelper), "CreateTransformedBeatmapData")]
    internal sealed class BeatmapDataTransformPatch
    {
        private static IReadonlyBeatmapData Postfix(IReadonlyBeatmapData __result)
        {
            // No longer relevant in v4.1.0 - need to yeet arcs and chains
            /*if (PluginConfig.Instance.blue_mode == 0 && PluginConfig.Instance.red_mode == 0)
            {
                return __result;
            }*/

            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return __result;
            }

            Plugin.Log.Debug("Modifying Map");

            // To accomodate CJD in 1.21.0
            __result = __result.GetFilteredCopy(beatmapDataItem =>
            {
                NoteData noteData;
                if ((noteData = (beatmapDataItem as NoteData)) != null)
                {
                    if (noteData.colorType == ColorType.None)
                    {
                        return noteData;
                    }

                    if (PluginConfig.Instance.blue_mode != 1 && noteData.colorType == ColorType.ColorB)
                    {
                        // v5.2.0 Option to de-rotate dot blocks
                        if (PluginConfig.Instance.yeet_dots && noteData.cutDirection == NoteCutDirection.Any)
                        {
                            noteData.SetCutDirectionAngleOffset(0);
                            // Ok to use elseif: slider and burstslider have angleoffset of 0 as per base game
                        }

                        if (PluginConfig.Instance.yeet_chains && noteData.gameplayType == NoteData.GameplayType.BurstSliderHead)
                        {
                            noteData.ChangeToGameNote();
                        }

                        return noteData;
                    }

                    if (PluginConfig.Instance.red_mode != 1 && noteData.colorType == ColorType.ColorA)
                    {
                        if (PluginConfig.Instance.yeet_dots && noteData.cutDirection == NoteCutDirection.Any)
                        {
                            noteData.SetCutDirectionAngleOffset(0);
                        }

                        if (PluginConfig.Instance.yeet_chains && noteData.gameplayType == NoteData.GameplayType.BurstSliderHead)
                        {
                            noteData.ChangeToGameNote();
                        }

                        return noteData;
                    }
                }

                SliderData sliderData;
                if ((sliderData = (beatmapDataItem as SliderData)) != null)
                {
                    // v4.1.0 Option to yeet arcs and chains (v4.0.0 added them by default)
                    if (sliderData.sliderType == SliderData.Type.Normal && PluginConfig.Instance.yeet_arcs == false)
                    {
                        if (PluginConfig.Instance.blue_mode != 1 && sliderData.colorType == ColorType.ColorB)
                        {
                            return sliderData;
                        }
                        if (PluginConfig.Instance.red_mode != 1 && sliderData.colorType == ColorType.ColorA)
                        {
                            return sliderData;
                        }
                    }

                    if (sliderData.sliderType == SliderData.Type.Burst && PluginConfig.Instance.yeet_chains == false)
                    {
                        if (PluginConfig.Instance.blue_mode != 1 && sliderData.colorType == ColorType.ColorB)
                        {
                            return sliderData;
                        }
                        if (PluginConfig.Instance.red_mode != 1 && sliderData.colorType == ColorType.ColorA)
                        {
                            return sliderData;
                        }
                    }
                }

                ObstacleData obstacleData;
                if ((obstacleData = (beatmapDataItem as ObstacleData)) != null)
                {
                    return obstacleData;
                }

                BeatmapEventData beatmapEventData;
                if ((beatmapEventData = (beatmapDataItem as BeatmapEventData)) != null)
                {
                    return beatmapEventData;
                }

                return null;
            });

            if (__result is BeatmapData)
            {
                foreach (string keyword in __result.specialBasicBeatmapEventKeywords)
                {
                    ((BeatmapData)__result).AddSpecialBasicBeatmapEventKeyword(keyword);
                }
            }

            return __result;
        }
    }


        /*BeatmapData result_2 = new BeatmapData(__result.numberOfLines);

        foreach (BeatmapDataItem beatmapDataItem in __result.allBeatmapDataItems)
        {
            //Plugin.Log.Debug("Blocks");
            NoteData noteData;
            if ((noteData = (beatmapDataItem as NoteData)) != null)
            {
                if (PluginConfig.Instance.blue_mode != 1 && noteData.colorType == ColorType.ColorB)
                {
                    if (PluginConfig.Instance.yeet_chains && noteData.scoringType == NoteData.ScoringType.BurstSliderHead)
                    {
                        noteData.ChangeToGameNote();
                    }
                    result_2.AddBeatmapObjectData(noteData);
                }

                if (PluginConfig.Instance.red_mode != 1 && noteData.colorType == ColorType.ColorA)
                {
                    if (PluginConfig.Instance.yeet_chains && noteData.scoringType == NoteData.ScoringType.BurstSliderHead)
                    {
                        noteData.ChangeToGameNote();
                    }    
                    result_2.AddBeatmapObjectData(noteData);
                }

                if (noteData.colorType == ColorType.None)
                {
                    result_2.AddBeatmapObjectData(noteData);
                }
            }

            //Plugin.Log.Debug("Sliders");
            SliderData sliderData;
            if ((sliderData = (beatmapDataItem as SliderData)) != null)
            {
                // v4.1.0 Option to yeet arcs and chains (v4.0.0 added them by default)
                if (sliderData.sliderType == SliderData.Type.Normal && PluginConfig.Instance.yeet_arcs == false)
                {
                    if (PluginConfig.Instance.blue_mode != 1 && sliderData.colorType == ColorType.ColorB)
                    {
                        result_2.AddBeatmapObjectData(sliderData);
                    }
                    if (PluginConfig.Instance.red_mode != 1 && sliderData.colorType == ColorType.ColorA)
                    {
                        result_2.AddBeatmapObjectData(sliderData);
                    }
                }

                if (sliderData.sliderType == SliderData.Type.Burst && PluginConfig.Instance.yeet_chains == false)
                {
                    if (PluginConfig.Instance.blue_mode != 1 && sliderData.colorType == ColorType.ColorB)
                    {
                        result_2.AddBeatmapObjectData(sliderData);
                    }
                    if (PluginConfig.Instance.red_mode != 1 && sliderData.colorType == ColorType.ColorA)
                    {
                        result_2.AddBeatmapObjectData(sliderData);
                    }
                }*/


                /*if (PluginConfig.Instance.blue_mode != 1 && sliderData.colorType == ColorType.ColorB)
                {
                    if (PluginConfig.Instance.yeet_arcs == false && sliderData.sliderType == SliderData.Type.Normal)
                    {
                        result_2.AddBeatmapObjectData(sliderData);
                    }
                    if (PluginConfig.Instance.yeet_chains == false && sliderData.sliderType == SliderData.Type.Burst)
                    {
                        result_2.AddBeatmapObjectData(sliderData);
                    }
                }

                if (PluginConfig.Instance.red_mode != 1 && sliderData.colorType == ColorType.ColorA)
                {
                    if (PluginConfig.Instance.yeet_arcs == false && sliderData.sliderType == SliderData.Type.Normal)
                    {
                        result_2.AddBeatmapObjectData(sliderData);
                    }
                    if (PluginConfig.Instance.yeet_chains == false && sliderData.sliderType == SliderData.Type.Burst)
                    {
                        result_2.AddBeatmapObjectData(sliderData);
                    }
                }*/
        

                /*//Plugin.Log.Debug("Obstacles");
                ObstacleData obstacleData;
                if ((obstacleData = (beatmapDataItem as ObstacleData)) != null)
                {
                    result_2.AddBeatmapObjectData(obstacleData);
                }

                //Plugin.Log.Debug("Events");
                BeatmapEventData beatmapEventData;
                if ((beatmapEventData = (beatmapDataItem as BeatmapEventData)) != null)
                {
                    result_2.InsertBeatmapEventData(beatmapEventData);
                }                
            }

            //Plugin.Log.Debug("EventKeywords");
            foreach (string keyword in __result.specialBasicBeatmapEventKeywords)
            {
                result_2.AddSpecialBasicBeatmapEventKeyword(keyword);
            }

            return result_2;
        }*/



    [HarmonyPatch(typeof(NoteController), "Update")]
    internal sealed class NoteControllerPatch
    {
        private static void Postfix(NoteController __instance)
        {
            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return;
            }


            if (PluginConfig.Instance.red_mode == 2 && __instance.noteData.colorType == ColorType.ColorA && __instance.noteTransform.position.z <= PluginConfig.Instance.dissolve_distance)
            {
                __instance.Dissolve(0.001f);
                return;
            }
            if (PluginConfig.Instance.blue_mode == 2 && __instance.noteData.colorType == ColorType.ColorB && __instance.noteTransform.position.z <= PluginConfig.Instance.dissolve_distance)
            {
                __instance.Dissolve(0.001f);
                return;
            }
        }
    }

    [HarmonyPatch(typeof(SliderController), "Update")]
    internal sealed class SliderControllerPatch
    {
        private static void Postfix(SliderController __instance)
        {
            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return;
            }


            if (PluginConfig.Instance.red_mode == 2 && __instance.sliderData.colorType == ColorType.ColorA && __instance.transform.position.z <= PluginConfig.Instance.dissolve_distance)
            {
                __instance.Dissolve(0.001f);
                return;
            }
            if (PluginConfig.Instance.blue_mode == 2 && __instance.sliderData.colorType == ColorType.ColorB && __instance.transform.position.z <= PluginConfig.Instance.dissolve_distance)
            {
                __instance.Dissolve(0.001f);
                return;
            }
        }
    }


    //[HarmonyPatch(typeof(BeatmapDataObstaclesAndBombsTransform), "ShouldUseBeatmapDataItem")]  // 1.31.0 made this private
    [HarmonyPatch]
    internal sealed class BeatmapDataObstaclesAndBombsTransformPatch
    { 
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method("BeatmapDataObstaclesAndBombsTransform:ShouldUseBeatmapDataItem");
        }

        private static bool Postfix(bool __result, BeatmapDataItem beatmapDataItem, GameplayModifiers.EnabledObstacleType enabledObstaclesType, bool noBombs)
        {
            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return true;
            }


            if (beatmapDataItem is ObstacleData)
            {
                if (enabledObstaclesType == GameplayModifiers.EnabledObstacleType.FullHeightOnly || PluginConfig.Instance.yeet_duck_walls)
                {
                    ObstacleData obstacleData = beatmapDataItem as ObstacleData;
                    if (obstacleData.height == 3)
                    {
                        return false;
                    }

                    return true;
                }

                if (enabledObstaclesType == GameplayModifiers.EnabledObstacleType.NoObstacles)
                {
                    if (PluginConfig.Instance.yeet_walls)
                    {
                        return true;
                    }

                    return false;
                }
            }

            else if (beatmapDataItem is NoteData && ((NoteData)beatmapDataItem).colorType == ColorType.None)
            {
                if (PluginConfig.Instance.yeet_bombs)
                {
                    return true;
                }

                return !noBombs;
            }

            return true;
        }
    }


    [HarmonyPatch(typeof(PlayerHeadAndObstacleInteraction), "Update")]
    internal sealed class ObstacleInteractionPatch
    {
        private static bool Prefix()
        {
            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return true;
            }

            if (PluginConfig.Instance.yeet_walls)
            {
                return false;
            }

            return true;
        }
    }


    [HarmonyPatch(typeof(BombNoteController), "Init")]
    internal sealed class BombNoteControllerPatch
    {
        private static void Postfix(ref BombNoteController __instance)
        {
            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return;
            }

            if (PluginConfig.Instance.yeet_bombs)
            {
                __instance.GetComponentInChildren<CuttableBySaber>().canBeCut = false;
            }
        }
    }


    [HarmonyPatch(typeof(GameEnergyCounter), "ProcessEnergyChange")]
    internal sealed class GameEnergyCounterPatch
    {
        private static bool Prefix(float energyChange, GameEnergyCounter __instance)
        {
            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return true;
            }

            //Plugin.Log.Debug("Energy: " + __instance.energy);
            //Plugin.Log.Debug("Change: " + energyChange);

            if ((PluginConfig.Instance.play_without_fail && __instance.energy + energyChange <= 0.01) &&
               (ScoreUtils.leaderboards_installed == false || BS_Utils.Gameplay.Gamemode.IsPartyActive))
            {
                //Plugin.Log.Debug("Saved from failing");
                return false;
            }

            return true;
        }
    }


    [HarmonyPatch(typeof(MultiplayerVerticalPlayerMovementManager), "Update")]
    internal sealed class MultiplayerVerticalPlayerMovementPatch
    {
        private static bool Prefix()
        {
            if (PluginConfig.Instance.enabled == false)
            {
                return true;
            }

            if (PluginConfig.Instance.play_without_mp_movement)
            {
                return false;
            }

            return true;
        }
    }


    [HarmonyPatch(typeof(MultiplayerOtherPlayersScoreDiffTextManager), "Update")]
    internal sealed class MultiplayerOtherPlayersScorePatch
    {
        private static MethodInfo hideall = AccessTools.Method("MultiplayerOtherPlayersScoreDiffTextManager:HideAll");
        private static bool Prefix(MultiplayerOtherPlayersScoreDiffTextManager __instance)
        {
            if (PluginConfig.Instance.enabled == false)
            {
                return true;
            }

            if (PluginConfig.Instance.play_without_mp_movement)
            {
                hideall.Invoke(__instance, null);
                return false;
            }

            return true;
        }
    }


    [HarmonyPatch(typeof(GameplayModifiersModelSO), "CreateModifierParamsList")]
    internal sealed class GameplayModifiersPatch
    {
        private static List<GameplayModifierParamsSO> Postfix(List<GameplayModifierParamsSO> __result, ref GameplayModifiers gameplayModifiers, ref GameplayModifiersModelSO __instance)
        {
            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return __result;
            }

            if (PluginConfig.Instance.play_without_modifiers && 
               (ScoreUtils.leaderboards_installed == false || BS_Utils.Gameplay.Gamemode.IsPartyActive))
            {
                return __result;
            }

            if ((PluginConfig.Instance.yeet_walls || PluginConfig.Instance.yeet_duck_walls) && gameplayModifiers.enabledObstacleType == GameplayModifiers.EnabledObstacleType.All)
            {
                //Plugin.Log.Debug("Add no obstacles modifier");
                __result.Add(__instance.GetGameplayModifierParams((GameplayModifierMask)8));
            }

            if (PluginConfig.Instance.yeet_bombs && gameplayModifiers.noBombs == false)
            {
                //Plugin.Log.Debug("Add no bombs modifier");
                __result.Add(__instance.GetGameplayModifierParams((GameplayModifierMask)16));
            }

            return __result;
        }
    }


    [HarmonyPatch(typeof(StandardLevelScenesTransitionSetupDataSO), "Init")]
    internal sealed class StandardLevelScenesTransitionPatch
    {
        private static void Prefix(ref GameplayModifiers gameplayModifiers)
        {
            gameplayModifiers = AccessAbility_Modifiers.Set_AccessAbility_Modifiers(gameplayModifiers);
        }
    }


    [HarmonyPatch(typeof(MissionLevelScenesTransitionSetupDataSO), "Init")]
    internal sealed class MissionLevelScenesTransitionPatch
    {
        private static void Prefix(ref GameplayModifiers gameplayModifiers)
        {
            gameplayModifiers = AccessAbility_Modifiers.Set_AccessAbility_Modifiers(gameplayModifiers);
        }
    }


    [HarmonyPatch(typeof(MultiplayerLevelScenesTransitionSetupDataSO), "Init")]
    internal sealed class MultiplayerLevelScenesTransitionPatch
    {
        private static void Prefix(ref GameplayModifiers gameplayModifiers)
        {
            gameplayModifiers = AccessAbility_Modifiers.Set_AccessAbility_Modifiers(gameplayModifiers);
        }
    }


    internal sealed class AccessAbility_Modifiers
    {
        internal static GameplayModifiers Set_AccessAbility_Modifiers(GameplayModifiers gameplayModifiers)
        {
            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return gameplayModifiers;
            }


            if (PluginConfig.Instance.play_without_modifiers &&
                (ScoreUtils.leaderboards_installed == false || BS_Utils.Gameplay.Gamemode.IsPartyActive))
            {
                return gameplayModifiers;
            }

            GameplayModifiers.EnabledObstacleType? walls_modifier = null;
            bool? bombs_modifier = null;

            if (PluginConfig.Instance.yeet_walls || PluginConfig.Instance.yeet_duck_walls)
            {
                walls_modifier = GameplayModifiers.EnabledObstacleType.NoObstacles;
                Plugin.Log.Debug("Set no obstacles modifier");
            }

            if (PluginConfig.Instance.yeet_bombs)
            {
                bombs_modifier = true;
                Plugin.Log.Debug("Set no bombs modifier");
            }

            return gameplayModifiers.CopyWith(null, null, null, null, walls_modifier, bombs_modifier, null, null, null, null, null, null, null);
        }
    }


    [HarmonyPatch(typeof(PlatformLeaderboardsModel), "UploadScore")]
    [HarmonyPatch(new Type[] { typeof(LeaderboardScoreUploader.ScoreData), typeof(PlatformLeaderboardsModel.UploadScoreCompletionHandler) })]
    internal sealed class PlatformLeaderboardsModelPatch_1
    {
        private static bool Prefix()
        {
            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return true;
            }

            if (PluginConfig.Instance.play_without_modifiers || PluginConfig.Instance.play_without_fail)
            {
                return false;
            }

            return true;
        }
    }


    [HarmonyPatch(typeof(PlatformLeaderboardsModel), "UploadScore")]
    [HarmonyPatch(new Type[] { typeof(IDifficultyBeatmap), typeof(int), typeof(int), typeof(int), typeof(bool), typeof(int), typeof(int), typeof(int), typeof(int), typeof(float), typeof(GameplayModifiers) })]
    internal sealed class PlatformLeaderboardsModelPatch_2
    {
        private static bool Prefix()
        {
            // BS 1.21.0
            if (PluginConfig.Instance.enabled == false)
            {
                return true;
            }

            if (PluginConfig.Instance.play_without_modifiers || PluginConfig.Instance.play_without_fail)
            {
                return false;
            }

            return true;
        }
    }
}
