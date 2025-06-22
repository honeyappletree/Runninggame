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
    [SerializeField] private Button storeButton;
    [SerializeField] private Button libraryButton;
    [SerializeField] private GameObject lock1;
    [SerializeField] private GameObject lock2;
    [SerializeField] private GameObject lock3;

    // Start is called before the first frame update
    void Start()
    {
        Stage1_btn.onClick.AddListener(Stage1);
        Stage2_btn.onClick.AddListener(Stage2);
        Stage3_btn.onClick.AddListener(Stage3);
        Stage4_btn.onClick.AddListener(Stage4);
        storeButton.onClick.AddListener(GoStore);
        libraryButton.onClick.AddListener(GoLibrary);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerRunning.unlockStage2 == true)
        {
            lock1.SetActive(false);
        }
        if(PlayerRunning2.unlockStage4 == true)
        {
            lock3.SetActive(false);
        }
        if(EnemyShoot.unlockStage3 == true)
        {
            lock2.SetActive(false);
        }
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
        SceneManager.LoadScene("RunningScene 1");
        Debug.Log("클릭되었습니다.");
    }

    void Stage4() //Stage4로 씬 전환
    {
        SceneManager.LoadScene("SampleScene 1");
        Debug.Log("클릭되었습니다.");
    }

    void GoStore()
    {
        SceneManager.LoadScene("StoreScene");
    }

    void GoLibrary()
    {
        SceneManager.LoadScene("Library");
    }

}
