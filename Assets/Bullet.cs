using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public float lifeTimer;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        lifeTimer = lifeTime;
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.up * speed * Time.deltaTime);
        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0)
        {
            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            BulletPool.Instance.ReturnBullet(gameObject);
            Debug.Log("Hit Enemy");
        }
    }
}
