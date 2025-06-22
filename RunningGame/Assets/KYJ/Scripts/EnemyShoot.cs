using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
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
    [SerializeField] private GameObject lockStage;
    [SerializeField] GameObject bulletPrefab;
    float bulletSpeed = 10f;
    [SerializeField] Transform topPoint;
    [SerializeField] Transform bottomPoint;
    [SerializeField] Transform forwardPoint;
    public static bool unlockStage3 = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        currentHp = startHp;
        clearPanel.SetActive(false);
        StartCoroutine(ShootLoop());
        lockStage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(coolTime <= currentTime)
        {
            isAttack = true;
            Debug.Log("공격");
            currentTime = 0;
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
                unlockStage3 = true;
            }
        }
    }

    IEnumerator ShootLoop()
    {
        while (currentHp > 0 && PlayerController.currentHp > 0)
        {
            // 위쪽 포인트 위치에서 위로 2발
            for (int i = 0; i < 2; i++)
            {
                Shoot(topPoint.position, Vector3.left);
                yield return new WaitForSeconds(0.3f);
            }

            // 아래쪽 포인트 위치에서 아래로 3발
            for (int i = 0; i < 3; i++)
            {
                Shoot(bottomPoint.position, Vector3.left);
                yield return new WaitForSeconds(0.3f);
            }

            // 가운데 포인트에서 정면으로 1발 (ex: Vector3.right)
            Shoot(forwardPoint.position, Vector3.left);
            yield return new WaitForSeconds(1f);
        }
    }

    void Shoot(Vector3 spawnPos, Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().SetDirection(direction, bulletSpeed);
    }
}
