using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DunkShotAnimation : MonoBehaviour
{

    void Start()
    {
        TweenParams tweenParams = new TweenParams().SetLoops(-1);
        Sequence rotateSequence = DOTween.Sequence();
        rotateSequence.Append(transform.DORotate(new Vector3(-20,0,transform.eulerAngles.z),1).SetAs(tweenParams));
        rotateSequence.Append(transform.DORotate(new Vector3(20,0,transform.eulerAngles.z),1).SetAs(tweenParams));
        rotateSequence.Append(transform.DORotate(new Vector3(0.2f,0,transform.eulerAngles.z),1f).SetAs(tweenParams));
        rotateSequence.SetAs(tweenParams);
    }
}
