using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Types;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform Ebullet_position;
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
