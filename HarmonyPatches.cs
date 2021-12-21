using AccessAbility.Configuration;
using HarmonyLib;
using System.Collections.Generic;


namespace AccessAbility
{
    [HarmonyPatch(typeof(BeatmapDataTransformHelper), "CreateTransformedBeatmapData")]
    internal class HarmonyPatches
    {
        static IReadonlyBeatmapData Postfix(IReadonlyBeatmapData __result)
        {
            if (PluginConfig.Instance.blue_mode == 0 && PluginConfig.Instance.red_mode == 0 && PluginConfig.Instance.yeet_duck_walls == false)
            {
                return __result;
            }

            Plugin.Log.Debug("Delete Blocks");


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

                    ObstacleData obstacleData;

                    if ((obstacleData = (enumerator.Current as ObstacleData)) != null && PluginConfig.Instance.yeet_duck_walls)
                    {
                        if (obstacleData.obstacleType == ObstacleType.Top)
                        {
                            obstacleData.MoveTime(-1f);
                            //Plugin.Log.Debug("Delete duck wall");
                        }
                    }
                         
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


    [HarmonyPatch(typeof(GameplayModifiersModelSO), "CreateModifierParamsList")]
    internal class GameplayModifiersPatch
    {
        static List<GameplayModifierParamsSO> Postfix(List<GameplayModifierParamsSO> __result, ref GameplayModifiers gameplayModifiers, ref GameplayModifiersModelSO __instance)
        {
            if (PluginConfig.Instance.yeet_walls || PluginConfig.Instance.yeet_duck_walls)
            {
                if ((int)gameplayModifiers.enabledObstacleType == 2)
                {
                    return __result;
                }

                Plugin.Log.Debug("Add NO modifier");

                __result.Add(__instance.GetGameplayModifierParams((GameplayModifierMask)8));
            }

            return __result;
        }
    }
}