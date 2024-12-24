using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage {get => damage;}
    [SerializeField] float speed = 5;
    [SerializeField] int damage = 1;

    Action<Bullet> ReturnBullet;

    public void Init(Action<Bullet> returnBullet)
    {
        ReturnBullet = returnBullet;
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.y += speed * Time.fixedDeltaTime;
        transform.position = pos;

        Vector2 posWorld = Camera.main.WorldToViewportPoint(transform.position);
        if(posWorld.y > 1)
        {
            ReturnBullet(this);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        ReturnBullet(this);
    }
}
