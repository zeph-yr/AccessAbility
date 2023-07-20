using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace AccessAbility.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; internal set; }

        public virtual bool enabled { get; internal set; } = false;
        public virtual int blue_mode { get; internal set; } = 0;
        public virtual int red_mode { get; internal set; } = 0;
        internal virtual float dissolve_distance { get; set; } = 6;
        internal virtual bool yeet_bombs { get; set; } = false;
        internal virtual bool yeet_walls { get; set; } = false;
        internal virtual bool yeet_duck_walls { get; set; } = false;
        internal virtual bool play_without_score { get; set; } = false;
        internal virtual bool play_without_modifiers { get; set; } = false;
        internal virtual bool play_without_fail { get; set; } = false;
        internal virtual bool yeet_arcs { get; set; } = false;
        internal virtual bool yeet_chains { get; set; } = false;
        internal virtual bool yeet_dots { get; set; } = false;
        internal virtual bool play_without_mp_movement { get; set; } = false;


        protected virtual void OnReload()
        {
            // Do stuff after config is read from disk.
        }

        protected virtual void Changed()
        {
            // Do stuff when the config is changed.
        }

        protected virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }
    }
}