using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int speed = 5;
    public GameObject Player_bullet;
    public GameObject Pause;
    public Button Resume_btn;
    public Button Quit_btn;
    public Button Retry_btn;
    public Transform bullet_position;
    public Button Pause_btn;
    int currentHp;
    int maxHp = 5;
    int bulletDamage = 1;
    public GameObject[] Life;
    public GameObject GameOverobject;

    // Start is called before the first frame update
    void Start()
    {
        Resume_btn.onClick.AddListener(Resume);
        Quit_btn.onClick.AddListener(Quit);
        Retry_btn.onClick.AddListener(Retry);
        Pause.SetActive(false);
        Pause_btn.onClick.AddListener(Stop);
        bullet_position = transform.Find("bullet_position");
        currentHp = maxHp;
        GameOverobject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PBullet();
    }

    void Move() //플레이어 이동
    {
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(new Vector3(0, y, 0));

        float moveX = Mathf.Clamp(transform.position.x, -21.47f, 57.5f); //이동 범위 제한(x축)
        float moveY = Mathf.Clamp(transform.position.y, -20.3f, 16.2f); //이동 범위 제한(y축)

        transform.position = new Vector3(moveX, moveY, transform.position.z);
    }

    void PBullet() //플레이어 공격
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("총알 발사!");
            GameObject bullet = Instantiate(Player_bullet);
            bullet.transform.position = bullet_position.transform.position;
        }
    }

    void Stop()
    {
        Pause.SetActive(true);
        Time.timeScale = 0f;
    }

    void Resume()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
    }

    void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    void Quit()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            currentHp -= bulletDamage;
            Life[currentHp].SetActive(false);
            Debug.Log("currentHp: " + currentHp);

            if(currentHp == 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        if(currentHp == 0)
        {
            Time.timeScale = 0f;
            GameOverobject.SetActive(true);
        }
    }


}
