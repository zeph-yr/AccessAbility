using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace AccessAbility.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public virtual bool enabled { get; set; } = false;
        public virtual int blue_mode { get; set; } = 0;
        public virtual int red_mode { get; set; } = 0;
        public virtual float dissolve_distance { get; set; } = 6;
        public virtual bool yeet_bombs { get; set; } = false;
        public virtual bool yeet_walls { get; set; } = false;
        public virtual bool yeet_duck_walls { get; set; } = false;
        public virtual bool neversubmit_enabled { get; set; } = false;
        public virtual bool play_without_modifiers { get; set; } = false;
        public virtual bool yeet_fail { get; set; } = false;
        public virtual bool yeet_arcs { get; set; } = false;
        public virtual bool yeet_chains { get; set; } = false;


        public virtual void OnReload()
        {
            // Do stuff after config is read from disk.
        }

        public virtual void Changed()
        {
            // Do stuff when the config is changed.
        }

        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }
    }
}