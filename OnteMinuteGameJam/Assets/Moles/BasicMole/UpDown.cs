using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class UpDown : MonoBehaviour
{
    public float moleTimeUp = 1f;
    public float moleSpeed = .00001f;

    //public float molePopupDelay = 2f;
    public float molePopupTargetY = -0.051f;
    public float molePopupDuration = 5f;
    public float molePopupWait = 1f;


    public void moleGoesUpAndDown(float molePopupWait)
    {
        //jemtodo : maybe add particle effects of vines growing or dirt moving when the pumpkin goes up and down

        var originalY = transform.position.y;

        DOTween.Sequence()
          //.AppendInterval(molePopupDelay)
          .Append(transform.DOMoveY(molePopupTargetY, molePopupDuration))
          .AppendInterval(molePopupWait)
          .Append(transform.DOMoveY(originalY, molePopupDuration))
          .SetTarget(gameObject);
    }
}
