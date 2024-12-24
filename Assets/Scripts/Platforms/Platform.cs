using System;
using UnityEngine;

namespace Platforms
{
    public abstract class Platform : MonoBehaviour
    {
        [SerializeField] Vector3 jumpForce = new Vector3(0, 40, 0);
        int points = 1;
        Action<int> addPoints;
        bool isActive;

        public void Init(Action<int> _addPoints, bool _isActive)
        {
            addPoints = _addPoints;
            isActive = _isActive;
        }

        protected virtual void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Player>(out Player player))
            {
                player.DoJump(jumpForce);
            } 

            if(isActive)
            {
                addPoints(points);
                isActive = false;
            }
        }
    }
}
