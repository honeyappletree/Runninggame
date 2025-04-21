using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbySceneManager : MonoBehaviour
{
    public Button Stage1_btn;
    public Button Stage2_btn;
    public Button Stage3_btn;
    public Button Stage4_btn;

    // Start is called before the first frame update
    void Start()
    {
        Stage1_btn.onClick.AddListener(Stage1);
        Stage2_btn.onClick.AddListener(Stage2);
        Stage3_btn.onClick.AddListener(Stage3);
        Stage4_btn.onClick.AddListener(Stage4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Stage1() //Stage1로 씬 전환
    {
        SceneManager.LoadScene("RunningScene");
        Debug.Log("클릭되었습니다.");
    }

    void Stage2() //Stage2로 씬 전환
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("클릭되었습니다.");
    }

    void Stage3() //Stage3으로 씬 전환
    {
        Debug.Log("클릭되었습니다.");
    }

    void Stage4() //Stage4로 씬 전환
    {
        Debug.Log("클릭되었습니다.");
    }
}
