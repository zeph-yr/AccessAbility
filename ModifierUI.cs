using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessAbility.Configuration;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;

namespace AccessAbility
{
    class ModifierUI : NotifiableSingleton<ModifierUI>
    {
        /*private string disable_col;

        [UIValue("disable_color")]
        public string Disable_Color
        {
            get => disable_col;
            set
            {
                NotifyPropertyChanged();
            }
        }*/

        private string blue_text = "Turn Off<#0000FF>Blue";
        private string red_text = "Turn Off<#FF00000>Red";

        [UIValue("delete_blue")]
        public bool Delete_Blue
        {
            get => PluginConfig.Instance.delete_blue;
            set
            {
                PluginConfig.Instance.delete_blue = value;
            }
        }
        [UIAction("set_delete_blue")]
        void Set_Delete_Blue(bool value)
        {
            Delete_Blue = value;
        }


        [UIValue("delete_red")]
        public bool Delete_Red
        {
            get => PluginConfig.Instance.delete_red;
            set
            {
                PluginConfig.Instance.delete_red = value;
            }
        }
        [UIAction("set_delete_red")]
        void Set_Delete_Red(bool value)
        {
            Delete_Red = value;
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
            if (value)
            {
                disable_col = "<#ff0000>Disable Score Submission";
                Disable_Color = "changed";
            }

            else
            {
                disable_col = "<#ffffff>Disable Score Submission";
                Disable_Color = "changed";
            }

        }
    }
}
