using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject player;
    bool isSlide = false;
    bool isInvincible = true;
    bool onSlope;
    SpriteRenderer sprite;
    float inputX;
    int clearLeaves = 0;
    [SerializeField] private GameObject Clearpanel;
    [SerializeField] private GameObject[] GoldLeaves;
    //[SerializeField] private GameObject slide_Position;
    [SerializeField] private TextMeshProUGUI GoldLeavesCount; //플레이 시 획득한 나뭇잎 갯수
    [SerializeField] private TextMeshProUGUI LastLeaves; //게임 클리어 시 뜨는 황금 나뭇잎 갯수
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText; //최종 점수 텍스트
    
    public static int addScore;
    bool isGiant = false;
    bool isSpeed = false;

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
        Clearpanel.SetActive(false);
        isGiant = false;
        isSpeed = false;
        ObstacleMove.speed = 5;
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

            if (Input.GetKey(KeyCode.S) && onGround == false)
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
        SceneManager.LoadScene("RunningScene");
    }

    void Quit()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1f;
    }

    void OnTriggerEnter2D(Collider2D other) //여기에서의 collision은 충돌한 오브젝트
    {
        if (other.CompareTag("Obstacle"))
        {
            currentHp -= impactDamage;
            hpValue.fillAmount = currentHp / maxHp;
            Debug.Log("currentHp: " + currentHp);
            OnDamaged();

            if (isGiant == true || isSpeed == true)
            {
                StopCoroutine(Invincible());
                Debug.Log("무적 코루틴 정지" + isInvincible);
            }

            if (currentHp == 0)
            {
                GameOver();
            }

            if (isSpeed == true && gameObject.layer == 11 && other.CompareTag("Obstacle"))
            {
                Obstacle _obstacle = other.GetComponent<Obstacle>();
                if (_obstacle != null)
                {
                    _obstacle.PlayAnim();
                    Debug.Log("장애물이 날아갔습니다.");
                    addScore += 50;
                    ScoreText.text = addScore.ToString();
                    Debug.Log("점수: " + addScore);
                }
            }

            if (isGiant == true && gameObject.layer == 10 && other.CompareTag("Obstacle"))
            {
                Obstacle _obstacle = other.GetComponent<Obstacle>();
                if (_obstacle != null)
                {
                    _obstacle.PlayAnim();
                    Debug.Log("장애물이 날아갔습니다.");
                    addScore += 50;
                    ScoreText.text = addScore.ToString();
                    Debug.Log("점수: " + addScore);
                }

            }
        }

        else if (other.gameObject.CompareTag("Precipice"))
        {
            currentHp = 0;
            hpValue.fillAmount = currentHp / maxHp;
            Debug.Log("Game Over");
            GameOver();
        }

        else if (other.gameObject.CompareTag("GoldLeaves"))
        {
            GoldLeaves[clearLeaves].SetActive(false);
            clearLeaves++;
            GoldLeavesCount.text = clearLeaves.ToString();
            addScore += 10;
            ScoreText.text = addScore.ToString();

            if (clearLeaves == 5)
            {
                Clearpanel.SetActive(true);
                LastLeaves.text = clearLeaves.ToString();
                lastScoreText.text = addScore.ToString();
                Time.timeScale = 0f;
            }
        }

        else if (other.gameObject.CompareTag("RecoveryPotion"))
        {
            currentHp += 10;
            hpValue.fillAmount = currentHp / maxHp;
            Debug.Log(currentHp);
        }

        else if(other.gameObject.CompareTag("SpeedPotion"))
        {
            StartCoroutine(PlayerSpeedUP());
            StopCoroutine(Invincible());
        }

        else if(other.gameObject.CompareTag("GiantItem"))
        {
            StartCoroutine(PlayerGiant());
            StopCoroutine(Invincible());
        }
    }

    IEnumerator Invincible() //플레이어가 데미지를 입었을 때
    {
        if (isGiant == true || isSpeed == true)
        {
            isInvincible = false;
            Debug.Log("거대화 상태 혹은 속도 증가 상태입니다.");
        }
        else if(isGiant == false && isSpeed == false)
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
    }

    IEnumerator PlayerGiant()
    {
        isGiant = true;
        Debug.Log("거대화 코루틴 시작");
        gameObject.layer = 10;
        impactDamage = 0;
        currentHp -= 0;
        Debug.Log("hp: " + currentHp);
        this.transform.localScale = new Vector3(1.1f, 2.5f, 1);
        this.transform.localPosition = new Vector3(-9.0774f, -1.124f, 0);
        yield return new WaitForSeconds(5.0f);
        Debug.Log("무적 상태로 이어졌음.");
        gameObject.layer = 8;
        sprite.color = new Color(0, 0.9294f, 1, 0.7f);
        this.transform.localScale = new Vector3(0.9f, 1.1f, 1);
        yield return new WaitForSeconds(3f); //3초 동안 유지
        sprite.color = new Color(0, 0.9294f, 1, 1);
        gameObject.layer = 7;
        impactDamage = 5;
        isGiant = false;
        isInvincible = true;
    }

    IEnumerator PlayerSpeedUP() //속도 증가 아이템을 얻었을 때
    {
        isInvincible = false;
        isSpeed = true;
        Debug.Log("속도 증가 코루틴 시작");
        gameObject.layer = 11;
        impactDamage = 0;
        currentHp -= 0;
        Debug.Log("hp: " + currentHp);
        ObstacleMove.speed = 9.5f;
        Debug.Log("speed: " + ObstacleMove.speed);
        yield return new WaitForSeconds(5.0f);
        ObstacleMove.speed = 5;
        Debug.Log("speed: " + ObstacleMove.speed);
        Debug.Log("무적 상태로 이어짐.");
        gameObject.layer = 8;
        sprite.color = new Color(0, 0.9294f, 1, 0.7f);
        yield return new WaitForSeconds(3f); //3초 동안 유지
        sprite.color = new Color(0, 0.9294f, 1, 1);
        gameObject.layer = 7;
        impactDamage = 5;
        isSpeed = false;
        isInvincible = true;
    }

    void OnDamaged()
    {
        if(isInvincible == true)
        {
            StartCoroutine(Invincible());
        }
        if (isSpeed == true || isGiant == true)
        {
            Debug.Log("속도증가나 거대화 아이템 획득");
            StopCoroutine("Invincible");
        }
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
            this.transform.localPosition = new Vector3(-9.0774f, -1.62f, 0);
        }
        if (onGround == true && Input.GetKeyUp(KeyCode.S))
        {
            this.transform.localScale = new Vector3(0.9f, 2.3f, 1);
            this.transform.localPosition = new Vector3(-9.0774f, -1.124f, 0);
        }
        else if (onGround == false && Input.GetKey(KeyCode.S))
        {
            this.transform.localScale = new Vector3(0.9f, 2.3f, 1);
        }
        if (onSlope == true && Input.GetKey(KeyCode.S))
        {
            this.transform.localScale = new Vector3(0.5f, 2.3f, 1);
            this.transform.localRotation = Quaternion.Euler(0, 0, 135);
        }
        if (onSlope == true && Input.GetKeyUp(KeyCode.S))
        {
            this.transform.localPosition = new Vector3(-9.0774f, -0.43f, 0);
            this.transform.localScale = new Vector3(0.9f, 2.3f, 1);
        }
        if (onSlope == false && Input.GetKey(KeyCode.S))
        {
            this.transform.localPosition = new Vector3(-9.22f, 0.87f, 0);
            this.transform.localScale = new Vector3(0.9f, 1.3f, 1);
        }
        if (onSlope == false && Input.GetKeyUp(KeyCode.S))
        {

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
            this.transform.localRotation = Quaternion.identity;
        }

        //if (collision.gameObject.CompareTag("Slope"))
        //{
        //    isJumping = false;
        //    onSlope = true;
        //    jumpCount = 0;
        //    isSlide = true;
        //    this.transform.localRotation = Quaternion.Euler(0, 0, 45);
        //}

        //if (collision.gameObject.CompareTag("UnSlope"))
        //{
        //    isJumping = false;
        //    jumpCount = 0;
        //    isSlide = true;
        //    onSlope = false;
        //    this.transform.localRotation = Quaternion.identity;
        //    this.transform.localPosition = new Vector3(-9.22f, 0.91f, 0);
        //}
    }

    void GameOver()
    {
        if(currentHp == 0)
        {
            Clearpanel.SetActive(true);
            LastLeaves.text = clearLeaves.ToString();
            lastScoreText.text = addScore.ToString();
            Time.timeScale = 0f;
        }
    }
}
