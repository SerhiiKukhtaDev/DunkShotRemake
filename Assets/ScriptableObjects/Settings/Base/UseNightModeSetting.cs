using ScriptableObjects.Settings.Base.Types;
using UniRx;
using UnityEngine;

namespace ScriptableObjects.Settings.Base
{
    [CreateAssetMenu(menuName = "Settings/UseNightMode", fileName = "UseNightMode", order = 51)]
    public class UseNightModeSetting : BoolSettingBase
    {
        [SerializeField] private bool defaultValue;
        [SerializeField] private BoolReactiveProperty currentValue;

        protected override bool DefaultValue => defaultValue;
        
        public override ReactiveProperty<bool> Setting => currentValue;
    }
}