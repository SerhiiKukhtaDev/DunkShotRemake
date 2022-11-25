using Contexts.Level.Services.Audio;
using Lean.Gui;
using UniRx;
using UnityEngine;
using Zenject;
using AudioType = ScriptableObjects.Audios.AudioType;

namespace Buttons
{
    public class ButtonAudio : MonoBehaviour
    {
        [SerializeField] private LeanButton button;
        
        private IAudioService _audioService;

        [Inject]
        private void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        private void Start()
        {
            button.OnClick.AsObservable().Subscribe(_ => _audioService.Play(AudioType.ButtonClick)).AddTo(this);
        }
    }
}
