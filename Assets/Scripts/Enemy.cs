using System;
using DG.Tweening;
using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int damage = 2;
    [SerializeField] int health = 3;
    [SerializeField] float speed = 0.5f;
    [SerializeField] float boundX = 3.6f;
    [SerializeField] Color damageColor;
    Action<int> UpdateCount;
    Action<Enemy> returnEnemy;
    Material material;
    Color startColor;

    public void Init(Action<int> updateCount, Action<Enemy> returnEnemy)
    {
        material = GetComponent<Renderer>().material;
        startColor = material.color;

        UpdateCount = updateCount;
        this.returnEnemy = returnEnemy;
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if(transform.position.x > boundX)
        {
            transform.position = new Vector3(boundX, transform.position.y, transform.position.z);
            speed *= -1;
        }
        if(transform.position.x < -boundX)
        {
            transform.position = new Vector3(-boundX, transform.position.y, transform.position.z);
            speed *= -1;
        }

        Vector2 posWorld = Camera.main.WorldToViewportPoint(transform.position);
        if(posWorld.y < 0)
        {
            returnEnemy(this);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.GetDamageToPlayer(speed);
            UpdateCount(-damage);
            Vector3 jumpPos = transform.position;
            jumpPos.x -= speed; 
            gameObject.transform.DOJump(jumpPos, 0.2f, 1, 0.3f);
        }
        else if (other.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
        {
            GetDamage(bullet.Damage);
            if(health < 0)
               returnEnemy(this);
        }
    }

    void GetDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        Sequence seq = DOTween.Sequence();
        seq.Append(material.DOColor(damageColor, 0.5f))
            .AppendInterval(0.1f)
            .Append(material.DOColor(startColor, 0.3f));
    }
}
