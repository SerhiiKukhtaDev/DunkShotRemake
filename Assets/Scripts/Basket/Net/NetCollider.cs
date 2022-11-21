using System;
using UniRx.Triggers;
using UnityEngine;

namespace Basket.Net
{
    public class NetCollider : MonoBehaviour
    {
        public IObservable<Collider2D> TriggerEnter => this.OnTriggerEnter2DAsObservable();
        
        public IObservable<Collider2D> TriggerExit => this.OnTriggerExit2DAsObservable();

        public IObservable<Collider2D> TriggerStay => this.OnTriggerStay2DAsObservable();
    }
}
