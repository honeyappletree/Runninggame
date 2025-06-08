using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 40;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyzone"))
        {
            PBulletPool.ReturnBullet(this);
            Debug.Log("ÅºÈ¯ ¹ÝÈ¯");
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PBulletPool.ReturnBullet(this);
            Debug.Log("ÅºÈ¯ ¹ÝÈ¯");
        }
    }

}
