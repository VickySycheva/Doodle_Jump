using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] int poolSize = 5;
    
    Queue<Bullet> bulletPool = new Queue<Bullet>();
    Player player;

    public void Init (Player player)
    {
        for (int i = 0; i < poolSize; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.Init(ReturnBullet);
            bullet.gameObject.SetActive(false);
            bulletPool.Enqueue(bullet);
        }

        this.player = player;
    }
    
    void Update()
    {
        if(player == null) return;
        if(Input.GetKeyDown (KeyCode.Space))
        {
            Bullet bullet = GetBullet();
            bullet.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.2f, 0);
        }
    }

    Bullet GetBullet()
    {
        Bullet bullet;
        if(bulletPool.Count > 0)
        {
            bullet = bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
        }
        else
        {
            bullet = Instantiate(bulletPrefab);
            bullet.Init(ReturnBullet);
        }
        return bullet;
    }

    void ReturnBullet (Bullet bullet)
    {
        bulletPool.Enqueue(bullet);
        bullet.gameObject.SetActive(false);
    }
}