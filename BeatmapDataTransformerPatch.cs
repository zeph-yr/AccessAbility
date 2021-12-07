using AccessAbility.Configuration;
using HarmonyLib;
using System.Collections.Generic;

namespace AccessAbility
{
    [HarmonyPatch(typeof(BeatmapDataTransformHelper), "CreateTransformedBeatmapData")]        
    internal class BeatmapDataTransformerPatch
    {
        static IReadonlyBeatmapData Postfix(IReadonlyBeatmapData __result) //, bool leftHanded)
        {
            Plugin.Log.Debug("Delete Blocks");

            if (PluginConfig.Instance.delete_blue == false && PluginConfig.Instance.delete_red == false)
            {
                return __result;
            }

            //ColorType to_delete = ColorType.ColorA; // A: Red, B: Blue

            //if (leftHanded)
            //{
            //    to_delete = ColorType.ColorB;
            //}

            using (IEnumerator<BeatmapObjectData> enumerator = __result.beatmapObjectsData.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    NoteData noteData;

                    if((noteData = (enumerator.Current as NoteData)) != null)
                    {
                        if (noteData.colorType == ColorType.ColorB && PluginConfig.Instance.delete_blue)
                        {
                            noteData.MoveTime(-1f);
                            //Plugin.Log.Debug("Delete blue");
                        }

                        if (noteData.colorType == ColorType.ColorA && PluginConfig.Instance.delete_red)
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
}