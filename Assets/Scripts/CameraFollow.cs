using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Player player;
    Action endGame;
    Vector3 startPos;

    void LateUpdate() 
    {
        UpdateCameraPos();
    }

    public void Init(Player player, Action endGame)
    {
        this.player = player;
        this.endGame = endGame;
        startPos = transform.position;
    }

    public void ResetCameraPos()
    {
        transform.position = startPos;
    }

    void UpdateCameraPos()
    {
        if (player == null) return;
        if(player.transform.position.y > transform.position.y)
        {
            Vector3 newPos = new Vector3 (transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = newPos;
        } 
        if(!player.IsVisible)
        {
            enabled = false;
            endGame();
            ResetCameraPos();
        }    
    }
}