using Zenject;

namespace AccessAbility.Installers
{
    internal sealed class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ModifierUI>().AsSingle();
        }
    }
}
