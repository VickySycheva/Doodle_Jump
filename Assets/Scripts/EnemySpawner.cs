using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] int poolSize = 10;

    Queue<Enemy> enemyPool = new Queue<Enemy>();
    List<Enemy> spawnEnemy = new List<Enemy>();
    Action<int> UpdateCount;

    public void Init (Action<int> updateCount)
    {
        UpdateCount = updateCount;
        for (int i = 0; i < poolSize; i++)
        {
            CreateEnemy();
        }
    }

    public Enemy GetEnemy()
    {
        if(enemyPool.Count <= 0)
        {
            CreateEnemy();
        }
        Enemy enemy;
        enemy = enemyPool.Dequeue();
        enemy.gameObject.SetActive(true);
        spawnEnemy.Add(enemy);
        return enemy;
    }

    public void ReturnAllEnemy()
    {
        if(spawnEnemy.Count == 0) 
            return;

        while (spawnEnemy.Count > 0)
        {
            ReturnEnemy(spawnEnemy[0]);
        }
    }

    void CreateEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.Init(UpdateCount, ReturnEnemy);
        enemy.gameObject.SetActive(false);
        enemyPool.Enqueue(enemy);   
    }

    void ReturnEnemy (Enemy enemy)
    {
        spawnEnemy.Remove(enemy);
        enemyPool.Enqueue(enemy);
        enemy.gameObject.SetActive(false);
    }
}
