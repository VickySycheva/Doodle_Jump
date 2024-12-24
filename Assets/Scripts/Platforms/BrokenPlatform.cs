using DG.Tweening;
using UnityEngine;

namespace Platforms
{
    public class BrokenPlatform : Platform
    {
        Tween tween;

        protected override void OnCollisionEnter(Collision other)
        {
            tween = transform.DOShakePosition(.2f, .2f, 20, 20, false, true)
                            .OnComplete(() => gameObject.SetActive(false));;

        }

        void OnDestroy()
        {
            if (tween != null) tween.Kill();
        }
    }
}