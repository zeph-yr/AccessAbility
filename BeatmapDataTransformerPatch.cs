using AccessAbility.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using AccessAbility.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AccessAbility
{
    [HarmonyPatch(typeof(BeatmapDataTransformHelper), "CreateTransformedBeatmapData")]        
    internal class BeatmapDataTransformerPatch
    {
        internal static PlayerSpecificSettings lhm;

        static IReadonlyBeatmapData Postfix(IReadonlyBeatmapData __result, bool leftHanded)
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

            //Plugin.Log.Debug("Song End Time: " + AccessAbilityController.audioTimeSyncController.songEndTime);
            //float end_time = AccessAbilityController.audioTimeSyncController.songEndTime + 10f;

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
                        }

                        if (noteData.colorType == ColorType.ColorA && PluginConfig.Instance.delete_red)
                        {
                            noteData.MoveTime(-1f);
                        }
                    }

                    //if ((noteData = (enumerator.Current as NoteData)) != null && noteData.colorType == to_delete)
                    //{
                    // POC Test Case
                    //noteData.MoveTime(-1f);

                    //Plugin.Log.Debug("Delete block");
                    //}
                }
            }

            return __result;
        }
    }
}