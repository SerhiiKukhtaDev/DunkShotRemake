using UnityEngine;

namespace ScriptableObjects.Audios
{
    public interface IAudio
    {
        AudioType AudioType { get; }
        AudioClip Clip { get; }
        bool LoopAudio { get; }
        bool SceneDependent { get; }
        float Pitch { get; }
    }
    
    [CreateAssetMenu(fileName = "New audio", menuName = "Audios/Audio")]
    public class Audio : ScriptableObject, IAudio
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private AudioType audioType;
        
        [SerializeField] private bool loopAudio;
        [SerializeField] private bool randomizePitch;

        [Range(0.1f, 1)]
        [SerializeField] private float minPitch;

        [Range(1, 2)]
        [SerializeField] private float maxPitch;
        
        [SerializeField] private bool sceneDependent;

        public AudioClip Clip => clip;
        public bool LoopAudio => loopAudio;

        public AudioType AudioType => audioType;

        public bool SceneDependent => sceneDependent;

        public float Pitch
        {
            get
            {
                if (randomizePitch) return Random.Range(minPitch, maxPitch);
                else return 1;
            }
        }
    }
    
    public enum AudioType
    {
        ButtonClick,
        BallFlight,
        NotPerfectHit,
        PerfectHit,
        HitBorders,
        Lose
    }
}
