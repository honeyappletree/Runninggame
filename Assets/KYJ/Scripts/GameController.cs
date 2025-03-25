using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Quit;
    // Start is called before the first frame update
    void Start()
    {
        Quit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenQuit() //종료하기 버튼을 눌렀을 때 종료 확인 창 뜨게 하기
    {
        Quit.SetActive(true);
    }

    public void GameQuit() //종료하기 버튼을 눌렀을 때
    {
        Application.Quit();
        Debug.Log("종료되었습니다");
    }

    public void Cancel() //취소하기 버튼을 눌렀을 때
    {
        Quit.SetActive(false);
    }
}
