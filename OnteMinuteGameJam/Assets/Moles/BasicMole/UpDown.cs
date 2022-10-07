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

    public void MoleGoesUpAndDown(float molePopupWait, Tiles tile) {
      MoleGoesUpAndDown(molePopupWait, tile, () => { });
    }

    public void MoleGoesUpAndDown(float molePopupWait, Tiles tile, TweenCallback upDownEndCallback) {
      //jemtodo : maybe add particle effects of vines growing or dirt moving when the pumpkin goes up and down
      var originalY = transform.position.y;
      upDownSequence = DOTween.Sequence()
          //.AppendInterval(molePopupDelay) 
          .Append(transform.DOMoveY(molePopupTargetY, molePopupDuration))
          .AppendInterval(molePopupWait)
          .Append(transform.DOMoveY(originalY, molePopupDuration))
          .AppendCallback(upDownEndCallback)
          .Insert(0f, transform.DOShakeRotation(molePopupDuration))
          .SetLink(gameObject)
          .OnComplete(() => DeactivateMole(tile));
    }

    public void MoleGoesSuperSaiyan(float molePopupWait, Tiles tile, TweenCallback upDownEndCallback) {
      Debug.Log($"We going super sayain at: {transform.position}");

      upDownSequence =
          DOTween.Sequence()
              .Insert(0f, transform.DOMoveY(molePopupTargetY, molePopupDuration))
              .Insert(0f, transform.DOShakeRotation(molePopupDuration))
              .Insert(molePopupDuration, transform.DOPunchScale(Vector3.one * 1.20f, 1 + molePopupDuration, elasticity: 0f))
              .Insert(
                  molePopupDuration,
                  transform.DOJump(
                      transform.position + transform.forward * 1f + transform.up * 0.5f, 0.5f, 2, 1 + molePopupDuration))
              .InsertCallback(molePopupDuration + molePopupDuration + 1, upDownEndCallback)
              .SetLink(gameObject)
              .OnComplete(
                  () => {
                    if (TryGetComponent(out MoleDeath moleDeath)) {
                      moleDeath.KillMole(isExploding: true);
                    } else {
                      DeactivateMole(tile);
                    }
                  });
    }

    public void DeactivateMole(Tiles tile) {
        upDownSequence.Kill();
        gameObject.SetActive(false);
        tile.tileHasMole = false;
    }
}
