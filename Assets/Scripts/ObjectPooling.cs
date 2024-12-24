using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    // [SerializeField] Bullet bulletPrefab;
    // [SerializeField] int poolSize = 5;
    
    // Queue<Bullet> bulletPool = new Queue<Bullet>();

    // public void Init ()
    // {
    //     for (int i = 0; i < poolSize; i++)
    //     {
    //         Bullet bullet = Instantiate(bulletPrefab);
    //         bullet.Init(ReturnBullet);
    //         bullet.gameObject.SetActive(false);
    //         bulletPool.Enqueue(bullet);
    //     }
    // }

    // public Bullet GetBullet()
    // {
    //     Bullet bullet;
    //     if(bulletPool.Count > 0)
    //     {
    //         bullet = bulletPool.Dequeue();
    //         bullet.gameObject.SetActive(true);
    //     }
    //     else
    //     {
    //         bullet = Instantiate(bulletPrefab);
    //         bullet.Init(ReturnBullet);
    //     }
    //     return bullet;
    // }

    // void ReturnBullet (Bullet bullet)
    // {
    //     bulletPool.Enqueue(bullet);
    //     bullet.gameObject.SetActive(false);
    // }
}