﻿using Contexts.Level;
using Core.Factories;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BallInstaller : MonoInstaller
    {
        [SerializeField] private Ball ball;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ICustomFactory<Vector2, Ball>>()
                .To<BallFactory>()
                .FromSubContainerResolve()
                .ByMethod(BindFactory)
                .AsSingle();
        }

        private void BindFactory(DiContainer container)
        {
            container.Bind<BallFactory>().AsSingle();
            container.BindInstance(ball);
        }
    }
}