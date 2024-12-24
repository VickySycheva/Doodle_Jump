using System.Collections.Generic;
using Platforms;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] Player playerPrefab;
    [SerializeField] CameraFollow gameCamera;
    [SerializeField] List<GameObject> platformsPrefabs;
    [SerializeField] BulletSpawner bulletSpawner;
    [SerializeField] EnemySpawner enemySpawner;

    List<GameObject> allPlatforms;

    Player player;
    GameObject startPlatform;
    Vector3 playerPos;

    int count;
    int bestScore;

    void Start()
    {
        playerPos = new Vector3(0, -2.9f, 0);

        uiManager.Init(StartGame, Restart, Back);
        if(PlayerPrefs.HasKey("Best score"))
        {
            bestScore = PlayerPrefs.GetInt("Best score");
        }

        enemySpawner.Init(UpdateCount);
    }

    void StartGame()
    {
        if(uiManager.currentToggle == null) return;
        if(player != null) 
        {
            Restart();
            player.GetComponent<Renderer>().material = uiManager.currentToggle.GetMaterial();
        }
        else
        {
            player = Instantiate(playerPrefab, playerPos, Quaternion.identity);
            player.Init();
            player.GetComponent<Renderer>().material = uiManager.currentToggle.GetMaterial();

            bulletSpawner.Init(player);

            gameCamera.Init(player, EndGame);

            uiManager.ActivateStartScreen(false);
            uiManager.ActivateGameScreen(true);
            uiManager.UpdateCountText(0);

            startPlatform = Instantiate(platformsPrefabs[0], new Vector3(0, -4.9f, 0), Quaternion.identity);
            startPlatform.transform.localScale = new Vector3(10, 0.4f, 1);
            if(startPlatform.TryGetComponent(out Platform component))
                component.Init((int points) => uiManager.UpdateCountText(points), false);

            InstPlatforms(100);
        }   
    }

    void Back()
    {
        foreach(GameObject platform in allPlatforms)
        {
            Destroy(platform);
        }

        player.gameObject.SetActive(false);
        startPlatform.SetActive(false);

        uiManager.ActivateGameScreen(false);
        uiManager.ActivateEndScreen(false);
        uiManager.ActivateStartScreen(true);

        bulletSpawner.gameObject.SetActive(false);
        enemySpawner.ReturnAllEnemy();
    }

    void Restart()
    {
        foreach(GameObject platform in allPlatforms)
        {
            Destroy(platform);
        }
        
        enemySpawner.ReturnAllEnemy();
        Debug.Log("Restart, return all enemy");

        player.gameObject.SetActive(true);
        player.transform.position = playerPos;
        player.IsVisible = true;

        startPlatform.gameObject.SetActive(true);

        gameCamera.ResetCameraPos();
        gameCamera.enabled = true;

        InstPlatforms(100);

        uiManager.ActivateStartScreen(false);
        uiManager.ActivateEndScreen(false);
        uiManager.ActivateGameScreen(true);
        count = 0;
        UpdateCount(0);

        bulletSpawner.gameObject.SetActive(true);
    }

    void EndGame()
    {
        // enemySpawner.ReturnAllEnemy();
        player.gameObject.SetActive(false);
        startPlatform.gameObject.SetActive(false);
        
        foreach(GameObject platform in allPlatforms)
        {
            Destroy(platform);
        }
        allPlatforms.Clear();

        if(count > bestScore)
        {
            bestScore = count;
            PlayerPrefs.SetInt("Best score", bestScore);
        }

        uiManager.ActivateEndScreen(true);
        if(count < 0)
            count = 0;
        uiManager.UpdateEndCountText(count, bestScore);
        uiManager.ActivateGameScreen(false);

        bulletSpawner.gameObject.SetActive(false);
    }

    void InstPlatforms(int count)
    {
        float x = Random.Range(-3.7f, 3.7f);
        float y = Random.Range(-2.5f, -1.5f);
        Vector3 position = new Vector3(x, y, 0);

        allPlatforms = new List<GameObject>(count);

        for (int i = 0; i < count; i++)
        {
            if (i > 5 && i % 6 == 0)
            {
                Enemy enemy = enemySpawner.GetEnemy();
                enemy.transform.position = position;
            }
            else
            {
                GameObject platform = Instantiate(platformsPrefabs[Random.Range(0, platformsPrefabs.Count)], position, Quaternion.identity);
                if(platform.TryGetComponent<Platform>(out Platform component))
                    component.Init(UpdateCount, true);

                allPlatforms.Add(platform);
            }
            
            float newX = Random.Range(-3.5f, 3.5f);
            y += Random.Range(1f, 2f);

            while (Mathf.Abs(newX - x) < 1.7f)
            {
                newX = Random.Range(-3.5f, 3.5f);
            }

            position = new Vector3(newX, y, 0);
            x = newX;   
        }
    }

    public void UpdateCount (int points)
    {
        count += points;
        uiManager.UpdateCountText(count);
        if(count < 0)
            EndGame();
    }
}
