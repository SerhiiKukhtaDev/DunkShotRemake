using ScriptableObjects.Settings.Base.Types;
using UniRx;
using UnityEngine;

namespace ScriptableObjects.Settings.Base
{
    [CreateAssetMenu(menuName = "Settings/PlaySounds", fileName = "PlaySounds", order = 51)]
    public class PlaySoundsSetting : BoolSettingBase
    {
        [SerializeField] private bool defaultValue;
        [SerializeField] private BoolReactiveProperty currentValue;

        protected override bool DefaultValue => defaultValue;
        
        public override ReactiveProperty<bool> Setting => currentValue;
    }
}
