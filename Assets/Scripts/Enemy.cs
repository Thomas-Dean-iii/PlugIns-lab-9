using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    private float speed;
    private Vector3 leftPoint;
    private Vector3 rightPoint;
    private bool movingRight = true;

    
    public static event Action<Enemy> OnEnemyKilled;

    
    public void Initialize(float speed, Vector3 left, Vector3 right)
    {
        this.speed = speed;
        leftPoint = left;
        rightPoint = right;
        gameObject.SetActive(true);
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightPoint.x) movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftPoint.x) movingRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false); 
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        
        OnEnemyKilled?.Invoke(this);
    }
}