using System;
using UnityEngine;
using Zenject;

public class AdaptiveSprite : MonoBehaviour
{
    [SerializeField] private Transform sprite;
    
    private ScreenScaleNotifier _scaleNotifier;

    [Inject]
    private void Construct(ScreenScaleNotifier scaleNotifier)
    {
        _scaleNotifier = scaleNotifier;
    }

    private void Start()
    {
        //sprite.localScale *= _scaleNotifier.Factor;
    }
}
