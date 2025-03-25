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
        PBullet();
        Stop();
    }

    void Move() //플레이어 이동
    {
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(new Vector3(0, y, 0));
    }

    void PBullet() //플레이어 공격
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Instantiate(Player_bullet, bullet_position.transform.position, bullet_position.transform.rotation);
        }
    }

    void Stop()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
