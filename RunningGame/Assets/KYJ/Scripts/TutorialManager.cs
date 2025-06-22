using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject runningImage;
    [SerializeField] GameObject shootingImage;
    [SerializeField] Button runningButton;
    [SerializeField] Button shootingButton;
    public static bool isTutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        runningButton.onClick.AddListener(NextRunning);
        shootingButton.onClick.AddListener(NextShooting);
    }

    void NextRunning()
    {
        runningImage.SetActive(false);
        shootingImage.SetActive(true);
    }

    void NextShooting()
    {
        isTutorial = true;
        DataManager.instance.player1.tutorialDone = isTutorial;
        DataManager.instance.SaveData();
        SceneManager.LoadScene("LobbyScene");
    }
}
