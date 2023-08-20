using AccessAbility.Configuration;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.Parser;
using HarmonyLib;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Zenject;


namespace AccessAbility
{
    internal sealed class ModifierUI : IInitializable, IDisposable, INotifyPropertyChanged
    {
        private static GameplayModifiersPanelController gameplayModifiersPanelController;
        public event PropertyChangedEventHandler PropertyChanged;


        public void Initialize()
        {
            GameplaySetup.instance.AddTab("AccessAbility", "AccessAbility.ModifierUI.bsml", this, MenuType.All);
            Donate.Refresh_Text();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Donate_Update_Dynamic)));
        }

        public void Dispose()
        {
            if (GameplaySetup.instance != null)
            {
                GameplaySetup.instance.RemoveTab("AccessAbility");
            }
        }


        // BS 1.21.0 Addition
        [UIValue("enabled")]
        private bool Enabled
        {
            get => PluginConfig.Instance.enabled;
            set
            {
                PluginConfig.Instance.enabled = value;
            }
        }
        [UIAction("set_enabled")]
        private void Set_Enabled(bool value)
        {
            Enabled = value;
            Refresh_Modifier_UI();
        }


        [UIValue("increment_value_blue")]
        private int Increment_Value_Blue
        {
            get => PluginConfig.Instance.blue_mode;
            set
            {
                PluginConfig.Instance.blue_mode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Increment_Value_Blue)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Increment_Value_Red)));
            }
        }
        [UIAction("increment_formatter_red")]
        private string Increment_Formatter_Red(int value) => ((Mode_Enum)value).ToString();


        [UIComponent("dissolve_slider")]
        private SliderSetting Dissolve_Slider;
        [UIValue("dissolve_distance")]
        private float Dissolve_Distance
        {
            get => PluginConfig.Instance.dissolve_distance;
            set
            {
                PluginConfig.Instance.dissolve_distance = value;
            }
        }
        [UIAction("set_dissolve_distance")]
        private void Set_Dissolve_Distance(float value)
        {
            Dissolve_Distance = value;
        }


        // BS 1.21.0 Addition
        [UIValue("yeet_arcs")]
        private bool Yeet_Arcs
        {
            get => PluginConfig.Instance.yeet_arcs;
            set
            {
                PluginConfig.Instance.yeet_arcs = value;
            }
        }
        [UIAction("set_yeet_arcs")]
        private void Set_Yeet_Arcs(bool value)
        {
            Yeet_Arcs = value;
            Refresh_Modifier_UI();
        }


        [UIValue("yeet_chains")]
        private bool Yeet_Chains
        {
            get => PluginConfig.Instance.yeet_chains;
            set
            {
                PluginConfig.Instance.yeet_chains = value;
            }
        }
        [UIAction("set_yeet_chains")]
        private void Set_Yeet_Chains(bool value)
        {
            Yeet_Chains = value;
            Refresh_Modifier_UI();
        }


        [UIValue("yeet_dots")]
        private bool Yeet_Dots
        {
            get => PluginConfig.Instance.yeet_dots;
            set
            {
                PluginConfig.Instance.yeet_dots = value;
            }
        }
        [UIAction("set_yeet_dots")]
        private void Set_Yeet_Dots(bool value)
        {
            Yeet_Dots = value;
        }


        [UIValue("yeet_bombs")]
        private bool Yeet_Bombs
        {
            get => PluginConfig.Instance.yeet_bombs;
            set
            {
                PluginConfig.Instance.yeet_bombs = value;
            }
        }
        [UIAction("set_yeet_bombs")]
        private void Set_Yeet_Bombs(bool value)
        {
            Yeet_Bombs = value;
            Refresh_Modifier_UI();
        }


        [UIValue("yeet_walls")]
        private bool Yeet_Walls
        {
            get => PluginConfig.Instance.yeet_walls;
            set
            {
                PluginConfig.Instance.yeet_walls = value;
            }
        }
        [UIAction("set_yeet_walls")]
        private void Set_Yeet_Walls(bool value)
        {
            Yeet_Walls = value;
            Refresh_Modifier_UI();
        }


        [UIValue("yeet_duck_walls")]
        private bool Yeet_Duck_Walls
        {
            get => PluginConfig.Instance.yeet_duck_walls;
            set
            {
                PluginConfig.Instance.yeet_duck_walls = value;
            }
        }
        [UIAction("set_yeet_duck_walls")]
        private void Set_Yeet_Duck_Walls(bool value)
        {
            Yeet_Duck_Walls = value;
            Refresh_Modifier_UI();
        }


        [UIValue("play_without_mp_movement")]
        private bool Play_Without_MP_Movement
        {
            get => PluginConfig.Instance.play_without_mp_movement;
            set
            {
                PluginConfig.Instance.play_without_mp_movement = value;
            }
        }
        [UIAction("set_play_without_mp_movement")]
        private void Set_Play_Without_MP_Movement(bool value)
        {
            Play_Without_MP_Movement = value;
        }


        [UIValue("play_without_modifiers")]
        private bool Play_Without_Modifiers
        {
            get => PluginConfig.Instance.play_without_modifiers;
            set
            {
                PluginConfig.Instance.play_without_modifiers = value;
            }
        }
        [UIAction("set_play_without_modifiers")]
        private void Set_Play_Without_Modifiers(bool value)
        {
            Play_Without_Modifiers = value;
            Refresh_Modifier_UI();
        }


        [UIValue("play_without_fail")]
        private bool Play_Without_Fail
        {
            get => PluginConfig.Instance.play_without_fail;
            set
            {
                PluginConfig.Instance.play_without_fail = value;
            }
        }
        [UIAction("set_play_without_fail")]
        private void Set_Play_Without_Fail(bool value)
        {
            Play_Without_Fail = value;
        }


        [UIValue("play_without_score")]
        private bool Play_Without_Score
        {
            get => PluginConfig.Instance.play_without_score;
            set
            {
                PluginConfig.Instance.play_without_score = value;
            }
        }
        [UIAction("set_play_without_score")]
        private void Set_Play_Without_Score(bool value)
        {
            Play_Without_Score = value;
        }


        private void Refresh_Modifier_UI()
        {
            gameplayModifiersPanelController = Resources.FindObjectsOfTypeAll<GameplayModifiersPanelController>().FirstOrDefault();

            if (gameplayModifiersPanelController != null)
            {
                MethodInfo refresh = AccessTools.Method("GameplayModifiersPanelController:RefreshTotalMultiplierAndRankUI");
                refresh.Invoke(gameplayModifiersPanelController, null);
            }
        }


        // v5.2.0 Addition
        [UIValue("open_donate_text")]
        private string Open_Donate_Text => Donate.donate_clickable_text;

        [UIValue("open_donate_hint")]
        private string Open_Donate_Hint => Donate.donate_clickable_hint;

        [UIParams]
        private BSMLParserParams parserParams;

        [UIAction("open_donate_modal")]
        private void Open_Donate_Modal()
        {
            parserParams.EmitEvent("hide_donate_modal");
            Donate.Refresh_Text();
            parserParams.EmitEvent("show_donate_modal");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Donate_Modal_Text_Dynamic)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Donate_Modal_Hint_Dynamic)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Donate_Update_Dynamic)));
        }

        private void Open_Donate_Patreon()
        {
            Donate.Patreon();
        }
        private void Open_Donate_Kofi()
        {
            Donate.Kofi();
        }

        [UIValue("donate_modal_text_static_1")]
        private string Donate_Modal_Text_Static_1 => Donate.donate_modal_text_static_1;

        [UIValue("donate_modal_text_static_2")]
        private string Donate_Modal_Text_Static_2 => Donate.donate_modal_text_static_2;

        [UIValue("donate_modal_text_dynamic")]
        private string Donate_Modal_Text_Dynamic => Donate.donate_modal_text_dynamic;

        [UIValue("donate_modal_hint_dynamic")]
        private string Donate_Modal_Hint_Dynamic => Donate.donate_modal_hint_dynamic;

        [UIValue("donate_update_dynamic")]
        private string Donate_Update_Dynamic => Donate.donate_update_dynamic;


        /*[UIAction("#post-parse")]
        private void PostParse()
        {
            Plugin.Log.Debug("PostParse");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Donate_Update_Dynamic)));
        }*/
    }

    
    internal enum Mode_Enum
    {
        On = 0,
        NoBlocks = 1,
        Disappear = 2
    }
}