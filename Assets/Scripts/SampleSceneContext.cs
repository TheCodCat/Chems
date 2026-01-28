using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Zenject;

public class SampleSceneContext : MonoInstaller
{
    [SerializeField]private PlayerInput input;
    [SerializeField] private AimController aimController;
    [Header("Animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private ThirdPersonMovement playerMovement;
    [SerializeField] private Volume Volume;

    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().FromInstance(input).AsSingle();
        Container.Bind<AimController>().FromInstance(aimController).AsSingle();
        Container.Bind<ThirdPersonMovement>().FromInstance(playerMovement).AsSingle();
        Container.Bind<Animator>().FromInstance(animator).AsSingle();
        Container.Bind<Volume>().FromInstance(Volume).AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerAnimator>().FromNew().AsSingle()
            .WithArguments<Animator,ThirdPersonMovement, PlayerInput, AimController>(animator, playerMovement, input, aimController);
    }
}
