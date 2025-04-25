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
    public Image[] Life;
    public GameObject GameOverobject;
    Rigidbody2D rigid;
    bool isJumping = false;
    public float jumpPower = 10;
    int jumpCount = 0;
    bool onGround = true;
    bool isInvincible = false;
    SpriteRenderer sprite;


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
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        gameObject.layer = 7;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PBullet();
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
        }
    }

    void Move() //플레이어 이동
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(x, y, 0);

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            onGround = true;
            jumpCount = 0;
        }

    }

    void Stop() //일시정지
    {
        Pause.SetActive(true);
        Time.timeScale = 0f;
    }

    void Resume() //계속하기
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
    }

    void Retry() //다시하기
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    void Quit() //그만하기
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
            Life[currentHp].color = new Color(0.3018868f, 0.3018868f, 0.3018868f, 1);

            Debug.Log("currentHp: " + currentHp);

            StartCoroutine("OnDamaged");
            if (isInvincible = true && gameObject.layer == 8)
            {
                bulletDamage = 0;
            }

            if (currentHp == 0)
            {
                GameOver();
            }
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
        bulletDamage = 1;
    }

    void OnDamaged()
    {
        StartCoroutine("Invincible");
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
