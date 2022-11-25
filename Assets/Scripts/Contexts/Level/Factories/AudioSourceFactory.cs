using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Contexts.Project.Services.Coroutine;
using CustomYieldInstructions;
using ScriptableObjects.Audios;
using UnityEngine;
using Zenject;
using AudioType = ScriptableObjects.Audios.AudioType;
using Object = UnityEngine.Object;

namespace Contexts.Level.Factories
{
    public class AudioSourceFactory : IInitializable
    {
        private readonly ISimpleCoroutineWorker _coroutineWorker;
        private readonly List<Audio> _audios;
        private Dictionary<AudioType, Audio> _cachedAudios;

        public AudioSourceFactory(ISimpleCoroutineWorker coroutineWorker, List<Audio> audios)
        {
            _coroutineWorker = coroutineWorker;
            _audios = audios;
        }

        public void Initialize()
        {
            _cachedAudios = _audios.ToDictionary(x => x.AudioType);
        }

        public AudioSource CreateAndPlay(AudioType type)
        {
            IAudio audio = Get(type);
            
            var gameObject = new GameObject($"{audio.GetType()}Audio");

            if (!audio.SceneDependent)
                Object.DontDestroyOnLoad(gameObject);

            AudioSource source = gameObject.AddComponent<AudioSource>();
            
            Initialize(source, audio);

            return source;
        }

        private void Initialize(AudioSource source, IAudio audio)
        {
            source.pitch = audio.Pitch;
            source.loop = audio.LoopAudio;
            source.clip = audio.Clip;

            if (!source.loop)
                _coroutineWorker.StartCoroutine(Destroy(source, audio.Clip, audio));
        }

        private IEnumerator Destroy(AudioSource audioSource, AudioClip clip, IAudio audio)
        {
            yield return new WaitForClipEnd(clip);

            if(audioSource != null) 
            {
                Object.Destroy(audioSource.gameObject);
            }
        }

        private IAudio Get(AudioType type)
        {
            IAudio audio = _cachedAudios[type];
        	
            if (audio == null)
            {
                throw new Exception($"Sound of type {type.ToString()} is not found in the audio service");
            }

            return audio;
        }
    }
}
