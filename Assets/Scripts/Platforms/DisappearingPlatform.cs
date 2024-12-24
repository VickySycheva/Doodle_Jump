using DG.Tweening;
using UnityEngine;

namespace Platforms
{
    public class DisappearingPlatform : Platform
    {
        Material material;
        Tween tween;
    
        void Start()
        {
            material = GetComponent<Renderer>().material;
        }

        protected override void OnCollisionEnter(Collision other)
        {
            base.OnCollisionEnter(other);
            tween = material.DOFade(0.3f, 0.5f)
                                    .OnComplete(() => gameObject.SetActive(false));
        }

        void OnDestroy()
        {
            if (tween != null) tween.Kill();
        }
    }
}