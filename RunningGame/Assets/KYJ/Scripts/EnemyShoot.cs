using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Types;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] Transform[] Ebullet_position;
    public GameObject EnemyBullet;
    [SerializeField] private float coolTime = 1f;
    private float currentTime = 0;
    bool isAttack;
    int startHp = 50;
    int currentHp;
    int enemyDamage = 10;
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI clearScore;
    int currentScore;
    private int currentUp = 0;
    private int currentMiddle = 0;
    private int currentDown = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        currentHp = startHp;
        clearPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(coolTime <= currentTime)
        {
            isAttack = true;
            EnemyFire1();
            EnemyFire2();
            EnemyFire3();
            Debug.Log("АјАн");
            currentTime = 0;
        }
    }

    void EnemyFire1()
    {
        for (int f = 0; f < 2; f++)
        {
            GameObject bullet = Instantiate(EnemyBullet);
            bullet.transform.position = Ebullet_position[0].transform.position;
        }
    }
    
    void EnemyFire2()
    {
        for (int f = 2; f < 5; f++)
        {
            GameObject bullet = Instantiate(EnemyBullet);
            bullet.transform.position = Ebullet_position[1].transform.position;
        }
    }

    void EnemyFire3()
    {
        for(int f = 5; f < 6; f++)
        {
            GameObject bullet = Instantiate(EnemyBullet);
            bullet.transform.position = Ebullet_position[2].transform.position;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            currentHp -= enemyDamage;

            if(currentHp == 0)
            {
                clearPanel.SetActive(true);
                Time.timeScale = 0;
                currentScore += 250;
                clearScore.text = currentScore.ToString();
                scoreText.text = "250";

            }
        }
    }
}
