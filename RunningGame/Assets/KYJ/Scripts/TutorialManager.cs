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
        SceneManager.LoadScene("LobbyScene");
    }
}
