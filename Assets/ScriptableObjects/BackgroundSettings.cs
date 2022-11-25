using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "BackgroundSettings", fileName = "BackgroundSettings", order = 51)]
    public class BackgroundSettings : ScriptableObject
    {
        [SerializeField] private Color lightThemeColor;
        [SerializeField] private Color blackThemeColor;

        public Color LightThemeColor => lightThemeColor;

        public Color BlackThemeColor => blackThemeColor;
    }
}
