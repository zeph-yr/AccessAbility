/*using AccessAbility.Configuration;
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
    public class AccessAbilityController : MonoBehaviour
    {
        internal static AccessAbilityController Instance { get; private set; }

        internal static AudioTimeSyncController audioTimeSyncController;


        private void Awake()
        {
            if (Instance != null)
            {
                Plugin.Log?.Warn($"Instance of {GetType().Name} already exists, destroying.");
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this); // Don't destroy this object on scene changes
            Instance = this;
            Plugin.Log?.Debug($"{name}: Awake()");

            audioTimeSyncController = Resources.FindObjectsOfTypeAll<AudioTimeSyncController>().LastOrDefault();
            Plugin.Log.Debug("Audio Time:" + audioTimeSyncController.songEndTime);
        }

        private void Start()
        {

        }


        private void OnEnable()
        {

        }


        private void OnDisable()
        {

        }


        private void OnDestroy()
        {
            Plugin.Log?.Debug($"{name}: OnDestroy()");
            if (Instance == this)
                Instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.

        }
    }
}*/
