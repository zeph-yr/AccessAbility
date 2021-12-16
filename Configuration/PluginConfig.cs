using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace AccessAbility.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public virtual int blue_mode { get; set; } = 0;
        public virtual int red_mode { get; set; } = 0;

        //public virtual bool delete_blue { get; set; } = false;
        //public virtual bool dissolve_blue { get; set; } = false;

        //public virtual bool delete_red { get; set; } = false;
        //public virtual bool dissolve_red { get; set; } = false;

        public virtual float dissolve_distance { get; set; } = 6;
        public virtual bool neversubmit_enabled { get; set; } = true;


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