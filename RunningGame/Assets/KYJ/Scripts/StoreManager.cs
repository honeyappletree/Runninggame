using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    [SerializeField] private Text goldLeaves;
    [SerializeField] private GameObject itemPanel1;
    [SerializeField] private GameObject itemPanel2;
    [SerializeField] private GameObject itemPanel3;
    [SerializeField] private Button item1Button;
    [SerializeField] private Button item2Button;
    [SerializeField] private Button item3Button;
    [SerializeField] private Text item1Text;
    [SerializeField] private Text item2Text;
    [SerializeField] private Text item3Text;

    // Start is called before the first frame update
    void Start()
    {
        returnButton.onClick.AddListener(Return);
        LibraryManager.totalGoldLeaves = PlayerRunning.clearLeaves + PlayerRunning2.clearLeaves2;
        goldLeaves.text = LibraryManager.totalGoldLeaves.ToString();
        item1Button.onClick.AddListener(Item1);
        item2Button.onClick.AddListener(Item2);
        item3Button.onClick.AddListener(Item3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Return()
    {
        SceneManager.LoadScene("LobbyScene");
        Debug.Log("go lobbyscene");
    }

    void Item1()
    {
        itemPanel1.SetActive(true);
        if (LibraryManager.totalGoldLeaves >= 3)
        {
            item1Text.text = "해당 아이템을 구매하시겠습니까?";
        }
        else
        {
            item1Text.text = "황금나뭇잎 갯수가 부족해서 \n 해당 아이템을 구매할 수 없습니다.";
        }
    }

    void Item2()
    {
        itemPanel2.SetActive(true);
        if (LibraryManager.totalGoldLeaves >= 3)
        {
            item2Text.text = "해당 아이템을 구매하시겠습니까?";
        }
        else
        {
            item2Text.text = "황금나뭇잎 갯수가 부족해서 \n 해당 아이템을 구매할 수 없습니다.";
        }
    }

    void Item3()
    {
        itemPanel3.SetActive(true);
        if (LibraryManager.totalGoldLeaves >= 3)
        {
            item3Text.text = "해당 아이템을 구매하시겠습니까?";
        }
        else
        {
            item3Text.text = "황금나뭇잎 갯수가 부족해서 \n 해당 아이템을 구매할 수 없습니다.";
        }
    }

    public void BuyItem1()
    {
        LibraryManager.totalGoldLeaves -= 3;
        goldLeaves.text = LibraryManager.totalGoldLeaves.ToString();
    }

    public void BuyItem2()
    {
        LibraryManager.totalGoldLeaves -= 3;
        goldLeaves.text = LibraryManager.totalGoldLeaves.ToString();
    }

    public void BuyItem3()
    {
        LibraryManager.totalGoldLeaves -= 3;
        goldLeaves.text = LibraryManager.totalGoldLeaves.ToString();
    }

    public void NotBuyItem1()
    {
        itemPanel1.SetActive(false);
    }

    public void NotBuyItem2()
    {
        itemPanel2.SetActive(false);
    }

    public void NotBuyItem3()
    {
        itemPanel3.SetActive(false);
    }
  
}
