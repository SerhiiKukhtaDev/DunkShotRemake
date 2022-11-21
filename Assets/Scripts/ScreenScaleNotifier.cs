using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScreenScaleNotifier : IInitializable
{
    private readonly CanvasScaler _scaler;
    
    public float Factor { get; private set; }

    public ScreenScaleNotifier(CanvasScaler scaler)
    {
        _scaler = scaler;
    }

    void IInitializable.Initialize()
    {
        Factor = Screen.width / _scaler.referenceResolution.x;
    }
}
