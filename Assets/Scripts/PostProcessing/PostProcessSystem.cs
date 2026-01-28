using UnityEngine;
using UnityEngine.Rendering;
using Assets.Scripts.PostProcessing;
using Zenject;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class PostProcessSystem : MonoBehaviour
{
    private Volume _volume;
    [Inject]
    public void Construct(Volume volume)
    {
        _volume = volume;
    }
    public void ActivePostProcess(PostProcessObject postProcessObject)
    {
        foreach (var procces in postProcessObject.Items)
        {
            switch (procces.PostProcessEnum)
            {
                case PostProcessEnum.Bloom:
                    if(_volume.profile.TryGet(out Bloom component))
                    {
                        DOTween.To(() => component.);
                    }
                    break;
            }
        }
    }
}
