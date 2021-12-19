using AccessAbility.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace AccessAbility
{
    [HarmonyPatch(typeof(BeatmapDataTransformHelper), "CreateTransformedBeatmapData")]
    internal class BeatmapDataTransformerPatch
    {
        static IReadonlyBeatmapData Postfix(IReadonlyBeatmapData __result) //, bool leftHanded)
        {
            //if (PluginConfig.Instance.delete_blue == false && PluginConfig.Instance.delete_red == false)
            if (PluginConfig.Instance.blue_mode == 0 && PluginConfig.Instance.red_mode == 0)
            {
                return __result;
            }

            Plugin.Log.Debug("Delete Blocks");

            /*ColorType to_delete = ColorType.ColorA; // A: Red, B: Blue
            if (leftHanded)
            {
                to_delete = ColorType.ColorB;
            }*/

            using (IEnumerator<BeatmapObjectData> enumerator = __result.beatmapObjectsData.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    NoteData noteData;

                    if ((noteData = (enumerator.Current as NoteData)) != null)
                    {
                        if (noteData.colorType == ColorType.ColorB && PluginConfig.Instance.blue_mode == 1) //PluginConfig.Instance.delete_blue)
                        {
                            noteData.MoveTime(-1f);
                            //Plugin.Log.Debug("Delete blue");
                        }

                        if (noteData.colorType == ColorType.ColorA && PluginConfig.Instance.red_mode == 1) //PluginConfig.Instance.delete_red)
                        {
                            noteData.MoveTime(-1f);
                            //Plugin.Log.Debug("Delete red");
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
            //Plugin.Log.Debug("Note z position:" + __instance.noteTransform.position.z);

            /*if (PluginConfig.Instance.delete_blue || PluginConfig.Instance.delete_red)
            {
                return;
            }*/

            if (PluginConfig.Instance.red_mode == 2 /*PluginConfig.Instance.dissolve_red*/ && __instance.noteData.colorType == ColorType.ColorA && __instance.noteTransform.position.z <= PluginConfig.Instance.dissolve_distance)
            {
                    __instance.Dissolve(0.001f);
            }

            if (PluginConfig.Instance.blue_mode == 2 /*PluginConfig.Instance.dissolve_blue*/ && __instance.noteData.colorType == ColorType.ColorB && __instance.noteTransform.position.z <= PluginConfig.Instance.dissolve_distance)
            {
                    __instance.Dissolve(0.001f);
            }
        }
    }


    /*[HarmonyPatch(typeof(PlayerHeadAndObstacleInteraction), "Init")]
    internal class ObstacleDataPatch
    {
        static void Prefix(ref ObstacleData obstacleData)
        {
            if (PluginConfig.Instance.yeet_walls)
            {
                Plugin.Log.Debug("yeeting walls");
                Plugin.Log.Debug("width: " + obstacleData.width);


                float time = obstacleData.time;
                int line = obstacleData.lineIndex;
                ObstacleType type = obstacleData.obstacleType;
                float duration = obstacleData.duration;
                int width = obstacleData.width;

                ObstacleData fake_wall = new ObstacleData(time, line, type, duration, width * -1);
                obstacleData = fake_wall;

                Plugin.Log.Debug("width: " + fake_wall.width);
            }
        }
    }*/

    [HarmonyPatch(typeof(PlayerHeadAndObstacleInteraction), "GetObstaclesContainingPoint")]
    internal class ObstacleDataPatch
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
}