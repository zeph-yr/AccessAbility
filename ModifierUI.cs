using AccessAbility.Configuration;
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


        /*[UIValue("delete_blue")]
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


        [UIValue("dissolve_blue")]
        public bool Dissolve_Blue
        {
            get => PluginConfig.Instance.dissolve_blue;
            set
            {
                PluginConfig.Instance.dissolve_blue = value;
            }
        }
        [UIAction("set_dissolve_blue")]
        void Set_Dissolve_Blue(bool value)
        {
            Dissolve_Blue = value;
        }


        [UIValue("dissolve_red")]
        public bool Dissolve_Red
        {
            get => PluginConfig.Instance.dissolve_red;
            set
            {
                PluginConfig.Instance.dissolve_red = value;
            }
        }
        [UIAction("set_dissolve_red")]
        void Set_Dissolve_Red(bool value)
        {
            Dissolve_Red = value;
        }*/


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
            /*if (value)
            {
                disable_col = "<#ff0000>Disable Score Submission";
                Disable_Color = "changed";
            }

            else
            {
                disable_col = "<#ffffff>Disable Score Submission";
                Disable_Color = "changed";
            }*/
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
        Off = 1,
        Disappear = 2
    }
}


// Previous UI: Parking Lot
/*
< checkbox - setting value = 'delete_blue' on - change = 'set_delete_blue' text = 'No Blue' font - color = '#4d4dffff' hover - hint = 'Play original red patterns only' ></ checkbox - setting >
< checkbox - setting value = 'dissolve_blue' on - change = 'set_dissolve_blue' text = 'Disappearing Blue' font - color = '#4d4dff' hover - hint = 'Play original red patterns with disappearing blue blocks' ></ checkbox - setting >

< horizontal >
    < text text = '------------------' font - color = '#00000000' font - size = '1' ></ text >                      
</ horizontal >

< checkbox - setting value = 'delete_red' on - change = 'set_delete_red' text = 'No Red' font - color = '#ff4d4dff' hover - hint = 'Play original blue patterns only' ></ checkbox - setting >                          
< checkbox - setting value = 'dissolve_red' on - change = 'set_dissolve_red' text = 'Disappearing Red' font - color = '#ff4d4dff' hover - hint = 'Play original blue patterns with disappearing red blocks' ></ checkbox - setting >                                                  

< horizontal >
    < text text = '------------------' font - color = '#00000000' font - size = '1' ></ text >                                                                      
</ horizontal >
*/