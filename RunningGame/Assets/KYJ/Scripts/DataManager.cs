using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public int countLeaves1 = 0; //재화의 갯수(황금나뭇잎)
    public int countLeaves2 = 0;
    public bool tutorialDone = false; //튜토리얼을 수행했는지 확인하기 위함
    public int bestScore1 = 0; //최고 기록(stage1)을 재기 위함
    public int bestScore2 = 0; //최고 기록(stage3)을 재기 위함
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerData player1 = new PlayerData();
    public string path;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/BookBug";
        Debug.Log("path: " + path);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        string playerData = JsonUtility.ToJson(player1);
        Debug.Log(playerData);
        File.WriteAllText(path, playerData);
    }

    public void LoadData()
    {
        string playerData = File.ReadAllText(path);
        player1 = JsonUtility.FromJson<PlayerData>(playerData);
    } 
}
