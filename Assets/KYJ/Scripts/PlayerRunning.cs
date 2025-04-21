using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;
using Unity.VisualScripting;
using UnityEngine.AI;

public class PlayerRunning : MonoBehaviour
{
    public GameObject Pause;
    public Button Resume_btn;
    public Button Quit_btn;
    public Button Retry_btn;
    public GameObject Hpbar_back;
    public GameObject Hpbar_front;
    public float maxHp = 100;
    public Button Pause_btn;
    float currentHp;
    float impactDamage = 5;
    float minusHp = 1;
    public Image hpValue;
    bool isJumping = false;
    public float jumpPower = 10;
    Rigidbody2D rigid;
    int jumpCount = 0;
    bool onGround = true;
    public GameObject Overpanel;
    public GameObject player;
    bool isSlide = false;
    bool isInvincible = false;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        Resume_btn.onClick.AddListener(Resume);
        Quit_btn.onClick.AddListener(Quit);
        Retry_btn.onClick.AddListener(Retry);
        Pause_btn.onClick.AddListener(Stop);
        Pause.SetActive(false);
        currentHp = maxHp;
        hpValue = Hpbar_front.GetComponent<Image>();
        InvokeRepeating("TimeDamage", 0, 1);
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        gameObject.layer = 7;
    }

    // Update is called once per frame
    void Update()
    {
        Slide();
        if (Input.GetKeyDown(KeyCode.W))
        {

            if (jumpCount < 2)
            {
                rigid.velocity = Vector2.up * jumpPower;
                isJumping = true;
                onGround = false;
                jumpCount++;
                Debug.Log("점프");
            }

            if(Input.GetKeyDown(KeyCode.S) && onGround == false)
            {
                isSlide = false;
            }
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

    void OnTriggerEnter2D(Collider2D collision) //여기에서의 collision은 충돌한 오브젝트
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isInvincible = false;
            currentHp -= impactDamage;
            hpValue.fillAmount = currentHp / maxHp;
            Debug.Log("currentHp: " + currentHp);
            StartCoroutine("OnDamaged");

            if (isInvincible = true && gameObject.layer == 8)
            {
                impactDamage = 0;
            }

            if (currentHp == 0)
            {
               GameOver();
            }
        }
        else if(collision.gameObject.CompareTag("Precipice"))
        {
            currentHp = 0;
            hpValue.fillAmount = currentHp / maxHp;
            Debug.Log("Game Over");
            GameOver();
        }
    }

    IEnumerator Invincible()
    {
        isInvincible = true;
        Debug.Log("무적 시간");

        gameObject.layer = 8;
        sprite.color = new Color(0, 0.9294f, 1, 0.7f);
        yield return new WaitForSeconds(5f);
        sprite.color = new Color(0, 0.9294f, 1, 1); 
        gameObject.layer = 7;
        impactDamage = 5;
    }

    void OnDamaged()
    {
        StartCoroutine("Invincible");
    }

    void TimeDamage() //1초씩 지나면 HP를 깎음
    {
        currentHp -= minusHp;
        hpValue.fillAmount = currentHp / maxHp;
        Debug.Log("currentHp: " + currentHp);

        if (currentHp == 0)
        {
            CancelInvoke("TimeDamage");
            GameOver();
        }
    }

    void Slide() //s키를 누르는 동안 슬라이드
    {
        if (onGround == true && Input.GetKey(KeyCode.S))
        {
            this.transform.localScale = new Vector3(0.9f, 1.3f, 1);
            this.transform.localPosition = new Vector3(-9.0774f, -1.61f, 0);
        }
        if (onGround == true && Input.GetKeyUp(KeyCode.S))
        {
            this.transform.localScale = new Vector3(0.9f, 2.3f, 1);
            this.transform.localPosition = new Vector3(-9.0774f, -1.7f, 0);
        }
        if (onGround == false && Input.GetKeyDown(KeyCode.S))
        {
            this.transform.localScale = new Vector3(0.9f, 2.3f, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            onGround = true;
            jumpCount = 0;
            isSlide = true;
        }

    }

    void GameOver()
    {
        if(currentHp == 0)
        {
            Overpanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
