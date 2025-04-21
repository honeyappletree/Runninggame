using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform Ebullet_position;
    public GameObject EnemyBullet;
    public float coolTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //EnemyFire();
    }

    void EnemyFire()
    {
        
        
            GameObject bullet = Instantiate(EnemyBullet);
            bullet.transform.position = Ebullet_position.transform.position;
        
        
    }
}
