using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float xMin, xMax;
    private float spriteHalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;

        Vector3 leftEdge = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 0, 0));

        xMin = leftEdge.x;
        xMax = rightEdge.x;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            spriteHalfWidth = sr.bounds.extents.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(moveInput * moveSpeed * Time.deltaTime, 0f, 0f);
        transform.position += move;

        float clampedX = Mathf.Clamp(transform.position.x, xMin + spriteHalfWidth, xMax - spriteHalfWidth);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
