using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class UpDown : MonoBehaviour
{
    //public float molePopupDelay = 2f; // how long does it wait before starting to go up
    float molePopupTargetY = -0.051f;
    float molePopupDuration = 1f;
    float molePopupWait = 1f; // how long does it wait before going back down

    Sequence upDownSequence;

    public void moleGoesUpAndDown(float molePopupWait, Tiles tile)
    {
        //jemtodo : maybe add particle effects of vines growing or dirt moving when the pumpkin goes up and down

        var originalY = transform.position.y;

        upDownSequence = DOTween.Sequence()
          //.AppendInterval(molePopupDelay) 
          .Append(transform.DOMoveY(molePopupTargetY, molePopupDuration))
          .AppendInterval(molePopupWait)
          .Append(transform.DOMoveY(originalY, molePopupDuration))
          .SetTarget(gameObject)
          .OnComplete(() => DeactivateMole(tile));
    }

    public void DeactivateMole(Tiles tile)
    {
        upDownSequence.Kill();
        gameObject.SetActive(false);
        tile.tileHasMole = false;
    }
}
