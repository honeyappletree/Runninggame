using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 moveDir;
    private float speed;

    public void SetDirection(Vector3 dir, float spd)
    {
        moveDir = dir.normalized;
        speed = spd;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * speed * Time.deltaTime;
    }

    
}


