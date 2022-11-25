using UnityEngine;

namespace CustomYieldInstructions
{
    public class WaitForClipEnd : CustomYieldInstruction
    {
        private readonly AudioClip _clip;
        private readonly float _startTime;
        
        public override bool keepWaiting => Time.time - _startTime < _clip.length;

        public WaitForClipEnd(AudioClip clip)
        {
            _clip = clip;
            _startTime = Time.time;
        }
    }
}