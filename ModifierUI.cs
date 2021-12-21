using AccessAbility.Configuration;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using System.Linq;
using UnityEngine;

namespace AccessAbility
{
    class ModifierUI : NotifiableSingleton<ModifierUI>
    {
        [UIComponent("dissolve_slider")]
        public SliderSetting Dissolve_Slider;
        [UIValue("dissolve_distance")]
        public float Dissolve_Distance
        {
            get => PluginConfig.Instance.dissolve_distance;
            set
            {
                PluginConfig.Instance.dissolve_distance = value;
            }
        }
        [UIAction("set_dissolve_distance")]
        public void Set_Dissolve_Distance(float value)
        {
            Dissolve_Distance = value;
        }


        [UIValue("yeet_walls")]
        public bool Yeet_Walls
        {
            get => PluginConfig.Instance.yeet_walls;
            set
            {
                PluginConfig.Instance.yeet_walls = value;
            }
        }
        [UIAction("set_yeet_walls")]
        void Set_Yeet_Walls(bool value)
        {
            Yeet_Walls = value;

            GameplayModifiersPanelController gameplayModifiersPanelController = Resources.FindObjectsOfTypeAll<GameplayModifiersPanelController>().FirstOrDefault();
            gameplayModifiersPanelController.RefreshTotalMultiplierAndRankUI();
        }


        [UIValue("yeet_duck_walls")]
        public bool Yeet_Duck_Walls
        {
            get => PluginConfig.Instance.yeet_duck_walls;
            set
            {
                PluginConfig.Instance.yeet_duck_walls = value;
            }
        }
        [UIAction("set_yeet_duck_walls")]
        void Set_Yeet_Duck_Walls(bool value)
        {
            Yeet_Duck_Walls = value;

            GameplayModifiersPanelController gameplayModifiersPanelController = Resources.FindObjectsOfTypeAll<GameplayModifiersPanelController>().FirstOrDefault();
            gameplayModifiersPanelController.RefreshTotalMultiplierAndRankUI();
        }


        [UIValue("neversubmit_enabled")]
        public bool Neversubmit_Enabled
        {
            get => PluginConfig.Instance.neversubmit_enabled;
            set
            {
                PluginConfig.Instance.neversubmit_enabled = value;
            }
        }
        [UIAction("set_neversubmit_enabled")]
        void Set_Never_Enabled(bool value)
        {
            Neversubmit_Enabled = value;
        }


        [UIValue("increment_value_blue")]
        private int Increment_Value_Blue
        {
            get => PluginConfig.Instance.blue_mode;
            set
            {
                PluginConfig.Instance.blue_mode = value;
                NotifyPropertyChanged(nameof(Increment_Value_Blue));
            }
        }

        [UIAction("increment_formatter_blue")]
        private string Increment_Formatter_Blue(int value) => ((Mode_Enum)value).ToString();


        [UIValue("increment_value_red")]
        private int Increment_Value_Red
        {
            get => PluginConfig.Instance.red_mode;
            set
            {
                PluginConfig.Instance.red_mode = value;
                NotifyPropertyChanged(nameof(Increment_Value_Red));
            }
        }

        [UIAction("increment_formatter_red")]
        private string Increment_Formatter_Red(int value) => ((Mode_Enum)value).ToString();
    }


    public enum Mode_Enum
    {
        On = 0,
        NoBlocks = 1,
        Disappear = 2
    }
}