using System;
using System.Collections.Generic;
using Contexts.Level.Factories;
using Contexts.Project.Services;
using Contexts.Project.Services.Coroutine;
using ScriptableObjects.Settings.Base;
using ScriptableObjects.Settings.Base.Interfaces;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using AudioType = ScriptableObjects.Audios.AudioType;

namespace Contexts.Level.Services.Audio
{
	public interface IAudioService
	{
		void Play(AudioType type);
	}

	public class AudioService : IInitializable, IDisposable, IAudioService
	{
		private readonly ISettingsService _settingsService;
		private readonly ISimpleCoroutineWorker _coroutineWorker;
        private readonly AudioSourceFactory _sourceFactory;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private readonly Dictionary<AudioType, AudioSource> _activeAudioSources;
        private ISetting<bool> _canPlaySetting;

        public AudioService(ISettingsService settingsService, ISimpleCoroutineWorker coroutineWorker,
	        AudioSourceFactory sourceFactory)
        {
        	_activeAudioSources = new Dictionary<AudioType, AudioSource>();

            _settingsService = settingsService;
            _coroutineWorker = coroutineWorker;
        	_sourceFactory = sourceFactory;
        }

        void IInitializable.Initialize()
        {
	        _canPlaySetting = _settingsService.GetSetting<bool, PlaySoundsSetting>();
        }

        public void Play(AudioType type)
        {
	        if (!_canPlaySetting.Setting.Value || _activeAudioSources.ContainsKey(type))
		        return;

	        AudioSource source = _sourceFactory.CreateAndPlay(type);
	        
	        source.OnDestroyAsObservable().Subscribe(_ => _activeAudioSources.Remove(type));
	        
        	source.Play();
        	
        	_activeAudioSources.Add(type, source);
        }

        public void Dispose()
        {
        	_disposables.Dispose();
        }
    }
}
