using AccessAbility.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;


namespace AccessAbility
{
    [HarmonyPatch(typeof(BeatmapDataTransformHelper), "CreateTransformedBeatmapData")]
    internal class BeatmapDataTransformPatch
    {
        static IReadonlyBeatmapData Postfix(IReadonlyBeatmapData __result)
        {
            if (PluginConfig.Instance.blue_mode == 0 && PluginConfig.Instance.red_mode == 0 && PluginConfig.Instance.yeet_duck_walls == false)
            {
                return __result;
            }

            Plugin.Log.Debug("Delete Blocks or Duck Walls");


            using (IEnumerator<BeatmapObjectData> enumerator = __result.beatmapObjectsData.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    NoteData noteData;

                    if ((noteData = (enumerator.Current as NoteData)) != null)
                    {
                        if (noteData.colorType == ColorType.ColorB && PluginConfig.Instance.blue_mode == 1)
                        {
                            noteData.MoveTime(-1f);
                            //Plugin.Log.Debug("Delete blue");
                        }

                        if (noteData.colorType == ColorType.ColorA && PluginConfig.Instance.red_mode == 1)
                        {
                            noteData.MoveTime(-1f);
                            //Plugin.Log.Debug("Delete red");
                        }
                    }

                    /*ObstacleData obstacleData;

                    if ((obstacleData = (enumerator.Current as ObstacleData)) != null && PluginConfig.Instance.yeet_duck_walls)
                    {
                        if (obstacleData.obstacleType == ObstacleType.Top)
                        {
                            obstacleData.MoveTime(-1f);
                            //Plugin.Log.Debug("Delete duck wall");
                        }
                    }*/
                }
            }

            return __result;
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
            }

            if (PluginConfig.Instance.blue_mode == 2 && __instance.noteData.colorType == ColorType.ColorB && __instance.noteTransform.position.z <= PluginConfig.Instance.dissolve_distance)
            {
                __instance.Dissolve(0.001f);
            }
        }
    }


    [HarmonyPatch(typeof(BeatmapDataObstaclesAndBombsTransform), "ShouldUseBeatmapObject")]
    internal class BeatmapDataObstaclesAndBombsTransformPatch
    {
        static bool Postfix(bool __result, BeatmapObjectData beatmapObjectData, GameplayModifiers.EnabledObstacleType enabledObstaclesType, bool noBombs)
        {
            if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Obstacle)
            {
                if (enabledObstaclesType == GameplayModifiers.EnabledObstacleType.FullHeightOnly || PluginConfig.Instance.yeet_duck_walls)
                {
                    ObstacleData obstacleData;
                    if ((obstacleData = (beatmapObjectData as ObstacleData)) != null && obstacleData.obstacleType == ObstacleType.Top)
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

            else if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note && ((NoteData)beatmapObjectData).colorType == ColorType.None)
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



    [HarmonyPatch(typeof(PlayerHeadAndObstacleInteraction), "GetObstaclesContainingPoint")]
    internal class ObstacleInteractionPatch
    {
        static void Postfix(List<ObstacleController> obstacleControllers)
        {
            if (PluginConfig.Instance.yeet_walls)
            {
                //Plugin.Log.Debug("Yeeting walls");

                obstacleControllers.Clear();
            }
        }
    }


    [HarmonyPatch(typeof(BombNoteController), "Init")]
    internal class BombNoteControllerPatch
    {
        static void Postfix(ref BombNoteController __instance)
        {
            __instance.GetComponentInChildren<CuttableBySaber>().canBeCut = false;
        }
    }


    [HarmonyPatch(typeof(GameplayModifiersModelSO), "CreateModifierParamsList")]
    internal class GameplayModifiersPatch
    {
        static List<GameplayModifierParamsSO> Postfix(List<GameplayModifierParamsSO> __result, ref GameplayModifiers gameplayModifiers, ref GameplayModifiersModelSO __instance)
        {
            if ((PluginConfig.Instance.yeet_walls || PluginConfig.Instance.yeet_duck_walls) && gameplayModifiers.enabledObstacleType == GameplayModifiers.EnabledObstacleType.All)
            {
                /*if ((int)gameplayModifiers.enabledObstacleType == 2)
                {
                    return __result;
                }*/

                //Plugin.Log.Debug("Add no obstacles modifier");
                __result.Add(__instance.GetGameplayModifierParams((GameplayModifierMask)8));
            }

            if (PluginConfig.Instance.yeet_bombs && gameplayModifiers.noBombs == false)
            {
                /*if (gameplayModifiers.noBombs)
                {
                    return __result;
                }*/

                //Plugin.Log.Debug("Add no bombs modifier");
                __result.Add(__instance.GetGameplayModifierParams((GameplayModifierMask)16));
            }

            return __result;
        }
    }


    // Keep this note: Don't try to edit modifiers by patching this function
    // Causes problems when user clicks those modifiers in panel. States get all messed up

    /*[HarmonyPatch(typeof(GameplayModifiersModelSO), "CreateGameplayModifiers")] 
    internal class GameplayModifiersPatch_2
    {
        static GameplayModifiers Postfix(GameplayModifiers __result, ref Func<GameplayModifierParamsSO, bool> valueGetter, ref GameplayModifiersModelSO __instance)
        {
            GameplayModifiers.EnabledObstacleType no_obstacles = GameplayModifiers.EnabledObstacleType.All;
            if (PluginConfig.Instance.yeet_walls || PluginConfig.Instance.yeet_duck_walls)
            {
                no_obstacles = GameplayModifiers.EnabledObstacleType.NoObstacles;

                Plugin.Log.Debug("Create NO modifier");
            }

            bool no_bombs = PluginConfig.Instance.yeet_bombs;
            Plugin.Log.Debug("Create no bombs modifier");


            return new GameplayModifiers(__result.demoNoFail, __result.demoNoObstacles, __result.energyType, __result.noFailOn0Energy, __result.instaFail, false, no_obstacles, no_bombs, __result.fastNotes, __result.strictAngles, __result.disappearingArrows, __instance.GetSongSpeedFromValueGetter(valueGetter), __result.noArrows, __result.ghostNotes, __result.proMode, __result.zenMode, __result.smallCubes);
        }
    }*/


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

            return gameplayModifiers.CopyWith(null, null, null, null, null, null, walls_modifier, bombs_modifier, null, null, null, null, null, null, null, null, null);
        }
    }
}



    /*[HarmonyPatch(typeof(GameplayModifiers))] // Doesnt work to patch constructor
    [HarmonyPatch(new Type[] { })]
    class Patch
    {
        static void Prefix(ref GameplayModifiers.EnabledObstacleType enabledObstacleType, bool noBomb)
        {
            enabledObstacleType = GameplayModifiers.EnabledObstacleType.NoObstacles;
            noBomb = true;

            Plugin.Log.Debug("GameplayModifiers Patch");
        }
    }*/
