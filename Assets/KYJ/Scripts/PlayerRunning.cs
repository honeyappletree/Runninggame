using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerRunning : MonoBehaviour
{
    public float speed;
    public GameObject Pause;
    public Button Resume_btn;
    public Button Quit_btn;
    public Button Retry_btn;

    // Start is called before the first frame update
    void Start()
    {
        Resume_btn.onClick.AddListener(Resume);
        Quit_btn.onClick.AddListener(Quit);
        Retry_btn.onClick.AddListener(Retry);
        Pause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Stop();
    }

    void Move() //플레이어 이동
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void Stop()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause.SetActive(true);
            Time.timeScale = 0f;
        }
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
}
