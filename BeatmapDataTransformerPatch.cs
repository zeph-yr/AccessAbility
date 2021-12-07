using HarmonyLib;
using System;
using System.Collections.Generic;

namespace AccessAbility
{
    /*[HarmonyPatch(typeof(AudioTimeSyncController))]
    [HarmonyPatch("StartSong")]
    class AudioTimeSyncControllerPatch
    {
        static void Postfix(AudioTimeSyncController __instance)
        {
            if (Plugin.access != null)
            {
                Plugin.Log.Debug("In Patch Postfix");
                AccessAbilityController.audioTimeSyncController = __instance;
                Plugin.Log.Debug("Song EndTime: " + __instance.songEndTime.ToString());
            }
            Plugin.Log.Debug("End Patch Postfix");
        }
    }*/


    /// <summary>
    /// This patches ClassToPatch.MethodToPatch(Parameter1Type arg1, Parameter2Type arg2)
    /// </summary>
    [HarmonyPatch(typeof(BeatmapDataTransformHelper), "CreateTransformedBeatmapData")]
        
    internal class BeatmapDataTransformerPatch
    {
        
        /// <summary>
        /// This code is run before the original code in MethodToPatch is run.
        /// </summary>
        /// <param name="__instance">The instance of ClassToPatch</param>
        /// <param name="arg1">The Parameter1Type arg1 that was passed to MethodToPatch</param>
        /// <param name="____privateFieldInClassToPatch">Reference to the private field in ClassToPatch named '_privateFieldInClassToPatch', 
        ///     added three _ to the beginning to reference it in the patch. Adding ref means we can change it.</param>
        /*[HarmonyAfter(new string[] { "Another.mods.HarmonyID" })] // If another mod patches this method, apply this patch after the other mod's.
        static bool Prefix(ClassToPatch __instance, ref Parameter1Type arg1, ref string ____privateFieldInClassToPatch)
        {
            arg1 = new Parameter1Type("ChangedValue"); // This will change arg1 for anything in MethodToPatch after this.
            ____privateFieldInClassToPatch = "private field changed";
            bool stopRunningMethodToPatch;
            if (stopRunningMethodToPatch)
                return false; // If you return false in prefix, the rest of the code in MethodToPatch is skipped.
            return true;
        }*/

        /// <summary>
        /// This code is run after the original code in MethodToPatch is run.
        /// </summary>
        /// <param name="__instance">The instance of ClassToPatch</param>
        /// <param name="arg1">The Parameter1Type arg1 that was passed to MethodToPatch</param>
        /// <param name="____privateFieldInClassToPatch">Reference to the private field in ClassToPatch named '_privateFieldInClassToPatch', 
        ///     added three _ to the beginning to reference it in the patch. Adding ref means we can change it.</param>
        static IReadonlyBeatmapData Postfix(IReadonlyBeatmapData __result)
        {
            Plugin.Log.Debug("In PostFix");

            //Plugin.Log.Debug("Song End Time: " + AccessAbilityController.audioTimeSyncController.songEndTime);

            //float end_time = AccessAbilityController.audioTimeSyncController.songEndTime + 10f;

            using (IEnumerator<BeatmapObjectData> enumerator = __result.beatmapObjectsData.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    NoteData noteData;
                    if ((noteData = (enumerator.Current as NoteData)) != null && noteData.colorType == ColorType.ColorB)
                    {
                        // POC Test Case
                        noteData.MoveTime(-1f);

                        Plugin.Log.Debug("Delete block");
                    }
                }
            }
            return __result;
        }
    }
}