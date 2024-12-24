using DG.Tweening;
using UnityEngine;

public class Player: MonoBehaviour
{
    public bool IsVisible { get; set; }
    
    [SerializeField] float speed;
    [SerializeField] float boundX = 3.6f;
    
    float moveX;
    float lastPosY;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] Collider playerColl;

    public void Init ()
    {
        lastPosY = transform.position.y;
        IsVisible = true;
    }

    void FixedUpdate() 
    {
        DoMove();
        
        playerColl.enabled = transform.position.y < lastPosY;
        lastPosY = transform.position.y;

        Vector2 pos = Camera.main.WorldToViewportPoint (transform.position);
        if(pos.y < 0)
        {
            IsVisible = false;
        }
    }

    public void DoJump(Vector3 jumpForce)
    {
        if (playerRb == null) return;

        playerRb.AddForce(jumpForce, ForceMode.Impulse);
    } 

    public void GetDamageToPlayer(float speed)
    {
        gameObject.transform.DOShakePosition(0.2f, new Vector3 (0.3f, 0.1f, 0), 6, 90);

        Vector3 pos = transform.position;

        if(transform.position.x > boundX - 0.9f && transform.position.x < -boundX + 0.9f)
            speed *= -1;

        pos.x += speed + 0.4f;

        gameObject.transform.DOJump(pos, 0.2f, 1, 0.3f);
    }

    void DoMove()
    {
        if (playerRb == null) return;

        moveX = Input.GetAxis ("Horizontal") * speed;
        
        Vector3 vel = playerRb.linearVelocity;
        vel.x = moveX;
        playerRb.linearVelocity = vel;

        if(transform.position.x > boundX)
        {
            transform.position = new Vector3(boundX, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < -boundX)
        {
            transform.position = new Vector3(-boundX, transform.position.y, transform.position.z);
        }
    }
}
