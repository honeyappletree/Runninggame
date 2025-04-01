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
    float currentTime = 0;
    float minusHp = 1;
    public Image hpValue;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            currentHp -= impactDamage;
            hpValue.fillAmount = currentHp / maxHp;
            Debug.Log("currentHp: " + currentHp);
        }
    }
}
