using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PBulletPool : MonoBehaviour
{
    public static PBulletPool instance;
    [SerializeField] private GameObject PBulletPrefab;
    Queue<PlayerBullet> objectPool = new Queue<PlayerBullet>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Initialize(10);
    }

    void Initialize(int initCount)
    {
        for(int i = 0; i < initCount; i++)
        {
            objectPool.Enqueue(CreateBullet());
        }
    }

    private PlayerBullet CreateBullet() //GameObject¸¦ ²¨³»¼­ ¹ÛÀ¸·Î µ¹·ÁÁÜ
    {
        var newBullet = Instantiate(PBulletPrefab).GetComponent<PlayerBullet>();
        newBullet.gameObject.SetActive(false);
        newBullet.transform.SetParent(transform);
        return newBullet;
    }

    public static PlayerBullet GetBullet()
    {
        if(instance.objectPool.Count > 0)
        {
            var bullet = instance.objectPool.Dequeue();
            bullet.gameObject.SetActive(true);
            bullet.transform.SetParent(instance.transform);
            return bullet;
        }
        else
        {
            var newBullet = instance.CreateBullet();
            newBullet.gameObject.SetActive(true);
            newBullet.transform.SetParent(instance.transform);
            return newBullet;
        }
    }

    public static void ReturnBullet(PlayerBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(instance.transform);
        instance.objectPool.Enqueue(bullet);
    }


}
