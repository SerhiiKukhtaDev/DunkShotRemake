using System;
using UnityEngine;
using Zenject;

public class Ball : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    
    private Vector2 _startMousePosition;
    private Vector2 _endMousePosition;
    
    private IInputWrapper _inputWrapper;

    [Inject]
    private void Construct(IInputWrapper inputWrapper)
    {
        _inputWrapper = inputWrapper;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_inputWrapper.IsMouseDown)
            _startMousePosition = _inputWrapper.WorldMousePosition;

        if (_inputWrapper.IsMouseUp)
            _endMousePosition = _inputWrapper.WorldMousePosition;

        if (_startMousePosition != Vector2.zero && _endMousePosition != Vector2.zero)
        {
            rigidbody.AddForce((_startMousePosition - _endMousePosition) * 100, ForceMode2D.Force);
            _startMousePosition = Vector2.zero;
            _endMousePosition = Vector2.zero;
        }
    }
}
