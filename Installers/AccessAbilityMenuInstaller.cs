using Zenject;

namespace AccessAbility.Installers
{
    internal sealed class AccessAbilityMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ModifierUI>().AsSingle();
        }
    }
}
