using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Quit;
    public Button Start_btn;
    public Button Store_btn;
    public Button Quit_btn;

    // Start is called before the first frame update
    void Start()
    {
        Quit.SetActive(false);
        Start_btn.onClick.AddListener(GoLobby);
        Store_btn.onClick.AddListener(GoStore);
        Quit_btn.onClick.AddListener(GameQuit);

        if(File.Exists(DataManager.instance.path))
        {
            DataManager.instance.LoadData();
            Debug.Log("데이터가 로드되었습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameQuit() //종료하기 버튼을 눌렀을 때
    {
        Application.Quit();
        Debug.Log("종료되었습니다");
    }


    void GoLobby() //시작 버튼 눌렀을 때 로딩 씬으로 가기
    {
        if(TutorialManager.isTutorial == false)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
            Debug.Log("go loadingscene");
        }
    }

    void GoStore() //상점 버튼 눌렀을 때 상점 씬으로 가기
    {
        SceneManager.LoadScene("StoreScene");
        Debug.Log("go storescene");
    }
}
