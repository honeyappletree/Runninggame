using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform Ebullet_position;
    public GameObject EnemyBullet;
    [SerializeField] private float coolTime = 1f;
    private float currentTime = 0;
    bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(coolTime <= currentTime)
        {
            isAttack = true;
            EnemyFire();
            Debug.Log("АјАн");
            currentTime = 0;
        }
    }

    void EnemyFire()
    {
        GameObject bullet = Instantiate(EnemyBullet);
        bullet.transform.position = Ebullet_position.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Debug.Log("destroy");
        }
    }
}
