using AccessAbility.Configuration;
using HarmonyLib;
using System.Collections.Generic;

namespace AccessAbility
{
    [HarmonyPatch(typeof(BeatmapDataTransformHelper), "CreateTransformedBeatmapData")]
    internal class BeatmapDataTransformPatch
    {
        static IReadonlyBeatmapData Postfix(IReadonlyBeatmapData __result)
        {
            if (PluginConfig.Instance.blue_mode == 0 && PluginConfig.Instance.red_mode == 0)
            {
                return __result;
            }

            //Plugin.Log.Debug("Delete Blocks or Duck Walls");

            BeatmapData result_2 = new BeatmapData(__result.numberOfLines);

            foreach (BeatmapDataItem beatmapDataItem in __result.allBeatmapDataItems)
            {
                NoteData noteData;
                if ((noteData = (beatmapDataItem as NoteData)) != null)
                {
                    if (noteData.colorType == ColorType.ColorB && PluginConfig.Instance.blue_mode != 1)
                    {
                        result_2.AddBeatmapObjectData(noteData);
                        //Plugin.Log.Debug("Delete blue");
                    }

                    if (noteData.colorType == ColorType.ColorA && PluginConfig.Instance.red_mode != 1)
                    {
                        result_2.AddBeatmapObjectData(noteData);
                        //Plugin.Log.Debug("Delete red");
                    }

                    if (noteData.colorType == ColorType.None)
                    {
                        result_2.AddBeatmapObjectData(noteData);
                    }
                }

                ObstacleData obstacleData;
                if ((obstacleData = (beatmapDataItem as ObstacleData)) != null)
                {
                    result_2.AddBeatmapObjectData(obstacleData);
                }

                BeatmapEventData beatmapEventData;
                if ((beatmapEventData = (beatmapDataItem as BeatmapEventData)) != null)
                {
                    result_2.InsertBeatmapEventData(beatmapEventData);
                }                
            }

            foreach (string keyword in __result.specialBasicBeatmapEventKeywords)
            {
                result_2.AddSpecialBasicBeatmapEventKeyword(keyword);
            }

            return result_2;
        }
    }


    [HarmonyPatch(typeof(NoteController), "Update")]
    internal class NoteControllerPatch
    {
        static void Postfix(NoteController __instance)
        {
            //Plugin.Log.Debug("NoteController PostFix");

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


    [HarmonyPatch(typeof(BeatmapDataObstaclesAndBombsTransform), "ShouldUseBeatmapDataItem")]
    internal class BeatmapDataObstaclesAndBombsTransformPatch
    {
        static bool Postfix(bool __result, BeatmapDataItem beatmapDataItem, GameplayModifiers.EnabledObstacleType enabledObstaclesType, bool noBombs)
        {
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
    internal class ObstacleInteractionPatch
    {
        static bool Prefix()
        {
            if (PluginConfig.Instance.yeet_walls)
            {
                //Plugin.Log.Debug("Yeeting walls");
                return false;
            }

            return true;
        }
    }


    [HarmonyPatch(typeof(BombNoteController), "Init")]
    internal class BombNoteControllerPatch
    {
        static void Postfix(ref BombNoteController __instance)
        {
            if (PluginConfig.Instance.yeet_bombs)
            {
                __instance.GetComponentInChildren<CuttableBySaber>().canBeCut = false;
            }
        }
    }


    [HarmonyPatch(typeof(GameplayModifiersModelSO), "CreateModifierParamsList")]
    internal class GameplayModifiersPatch
    {
        static List<GameplayModifierParamsSO> Postfix(List<GameplayModifierParamsSO> __result, ref GameplayModifiers gameplayModifiers, ref GameplayModifiersModelSO __instance)
        {
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
    internal class StandardLevelScenesTransitionPatch
    {
        static void Prefix(ref GameplayModifiers gameplayModifiers)
        {
            gameplayModifiers = AccessAbility_Modifiers.Set_AccessAbility_Modifiers(gameplayModifiers);
        }
    }


    [HarmonyPatch(typeof(MultiplayerLevelScenesTransitionSetupDataSO), "Init")]
    internal class MultiplayerLevelScenesTransitionPatch
    {
        static void Prefix(ref GameplayModifiers gameplayModifiers)
        {
            gameplayModifiers = AccessAbility_Modifiers.Set_AccessAbility_Modifiers(gameplayModifiers);
        }
    }


    [HarmonyPatch(typeof(MissionLevelScenesTransitionSetupDataSO), "Init")]
    internal class MissionLevelScenesTransitionPatch
    {
        static void Prefix(ref GameplayModifiers gameplayModifiers)
        {
            gameplayModifiers = AccessAbility_Modifiers.Set_AccessAbility_Modifiers(gameplayModifiers);
        }
    }


    internal class AccessAbility_Modifiers
    {
        internal static GameplayModifiers Set_AccessAbility_Modifiers(GameplayModifiers gameplayModifiers)
        {
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
}
