using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private Camera _mainCamera;

    [Inject]
    private void Construct(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }

    private void Start()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = _mainCamera;
    }
}
