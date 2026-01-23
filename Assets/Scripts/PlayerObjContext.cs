using UnityEngine;
using Zenject;

public class PlayerObjContext : MonoInstaller
{
    [SerializeField] private Camera camTarget;
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(camTarget).AsSingle();
    }
}
