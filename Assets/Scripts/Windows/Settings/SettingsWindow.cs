using Core;
using Cysharp.Threading.Tasks;
using Lean.Gui;
using UniRx;
using UnityEngine;
using Utils;

namespace Windows.Settings
{
    public class SettingsWindow : Window
    {
        [SerializeField] private LeanButton close;

        private void Start()
        {
            close.OnClickAsObservableLimit().Subscribe(_ => Hide().Forget()).AddTo(this);
        }
    }
}
