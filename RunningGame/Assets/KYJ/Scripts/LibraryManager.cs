using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LibraryManager : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Button book1Button;
    [SerializeField] private GameObject book1Panel;
    [SerializeField] private Button book2Button;
    [SerializeField] private GameObject book2Panel;
    [SerializeField] private Button book3Button;
    [SerializeField] private GameObject book3Panel;
    [SerializeField] private Text book1Text;
    [SerializeField] private Text goldLeaves;
    public static int totalGoldLeaves;

    // Start is called before the first frame update
    void Start()
    {
        returnButton.onClick.AddListener(Return);
        book1Button.onClick.AddListener(BuyBook1);
        book2Button.onClick.AddListener(BuyBook2);
        book3Button.onClick.AddListener(BuyBook3);
        totalGoldLeaves = PlayerRunning.clearLeaves + PlayerRunning2.clearLeaves2;
        goldLeaves.text = totalGoldLeaves.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Return()
    {
        SceneManager.LoadScene("LobbyScene");
        Debug.Log("go lobbyscene");
    }

    void BuyBook1()
    {
        book1Panel.SetActive(true);
        if (totalGoldLeaves >= 5)
        {
            book1Text.text = "해당 책을 구매하시겠습니까?";
        }
        else
        {
            book1Text.text = "황금나뭇잎 갯수가 부족해서 \n 해당 책을 구매할 수 없습니다.";
        }
    }

    void BuyBook2()
    {
        book2Panel.SetActive(true);
        if (totalGoldLeaves >= 5)
        {
            book1Text.text = "해당 책을 구매하시겠습니까?";
        }
        else
        {
            book1Text.text = "황금나뭇잎 갯수가 부족해서 \n 해당 책을 구매할 수 없습니다.";
        }
    }

    void BuyBook3()
    {
        book3Panel.SetActive(true);
        if (totalGoldLeaves >= 5)
        {
            book1Text.text = "해당 책을 구매하시겠습니까?";
        }
        else
        {
            book1Text.text = "황금나뭇잎 갯수가 부족해서 \n 해당 책을 구매할 수 없습니다.";
        }
    }

    public void BuyItem1()
    {
        totalGoldLeaves -= 5;
        goldLeaves.text = totalGoldLeaves.ToString();
    }

    public void BuyItem2()
    {
        totalGoldLeaves -= 5;
        goldLeaves.text = totalGoldLeaves.ToString();
    }

    public void BuyItem3()
    {
        totalGoldLeaves -= 5;
        goldLeaves.text = totalGoldLeaves.ToString();
    }

    public void NotBuyItem1()
    {
        book1Panel.SetActive(false);
    }

    public void NotBuyItem2()
    {
        book2Panel.SetActive(false);
    }

    public void NotBuyItem3()
    {
        book3Panel.SetActive(false);
    }
}
