using System;
using Contexts.Level.Services.Audio;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using AudioType = ScriptableObjects.Audios.AudioType;

public class BorderHit : MonoBehaviour
{
    private IAudioService _audioService;

    [Inject]
    private void Construct(IAudioService audioService)
    {
        _audioService = audioService;
    }
    
    private void Start()
    {
        this
            .OnCollisionEnter2DAsObservable()
            .ThrottleFirst(TimeSpan.FromMilliseconds(100))
            .Subscribe(_ => _audioService.Play(AudioType.HitBorders)).AddTo(this);
    }
}
