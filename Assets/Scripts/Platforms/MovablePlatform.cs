using UnityEngine;

namespace Platforms
{
    public class MovablePlatform : Platform
    {
        [SerializeField] float speed = 1;

        void Update()
        {
            Vector3 pos = transform.position;
            pos.x += speed * Time.deltaTime;
            transform.position = pos;

            if(transform.position.x > 3.5f || transform.position.x < -3.5f)
            {
                speed *= -1;
            }
        }
    }
}
