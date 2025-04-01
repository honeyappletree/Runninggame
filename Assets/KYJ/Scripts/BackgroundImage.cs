using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImage : MonoBehaviour
{
    float width;
    public float speed;
    BoxCollider2D collider2D;


    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        width = collider2D.size.x;
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ImageMove();
        if (transform.position.x <= -140.0f)
        {
            Reposition();
        }
    }

    void ImageMove()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void Reposition()
    {
        Vector3 offset = new Vector3(width * 420f, 0, 0);
        transform.position = transform.position + offset;
    }
}
