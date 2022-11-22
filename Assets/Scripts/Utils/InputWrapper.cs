using UnityEngine;

public interface IInputWrapper
{
    bool IsMouseDown { get; }
    bool IsMouseUp { get; }
    Vector2 WorldMousePosition { get; }
    bool LeftMousePressed { get; }
}

public class InputWrapper : IInputWrapper
{
    private readonly Camera _mainCamera;

    public bool IsMouseDown => Input.GetMouseButtonDown(0);
    
    public bool IsMouseUp => Input.GetMouseButtonUp(0);

    public bool LeftMousePressed => Input.GetMouseButton(0);

    public Vector2 WorldMousePosition => _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    
    public InputWrapper(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }
}
