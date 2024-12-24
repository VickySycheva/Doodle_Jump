using UnityEngine;

namespace Platforms
{
    public class NormalPlatform : Platform
    {
        protected override void OnCollisionEnter(Collision other)
        {
            base.OnCollisionEnter(other);
        }
    }
}