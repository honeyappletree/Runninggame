using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PBullet();
    }

    void PBullet()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
