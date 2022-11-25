using System.Collections.Generic;
using Contexts.Level.Factories;
using Contexts.Level.Services.Audio;
using ScriptableObjects.Audios;
using UnityEngine;
using Zenject;

namespace Contexts.Level.Installers
{
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private List<Audio> audios;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AudioService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AudioSourceFactory>().AsSingle();
            Container.BindInstance(audios).AsSingle().WhenInjectedInto<AudioSourceFactory>();
        }
    }
}
