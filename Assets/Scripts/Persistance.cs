using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Persistance : MonoBehaviour
{
    public static Persistance Instance = null;

    private MainManager MainManagerInstance = null;

    private int m_BestScore;
    private string m_BestName;
    private string m_Name;

    // where we store/read data
    private string filePath = "";

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        filePath = Application.persistentDataPath + "/savefile.json";

        LoadData();
    }

    [System.Serializable]
    class PlayerInfo
    {
        public string Name;
        public int Score;

        public PlayerInfo(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }

    public void SetCurrentName(string name)
    {
        m_Name = name;
    }

    public void SetCurrentScore(int score)
    {
        // if best -> update best info
        if (score > m_BestScore)
        {
            m_BestScore = score;
            m_BestName = m_Name;
        }
    }

    public void SaveData()
    {
        PlayerInfo data = new(m_BestName, m_BestScore);

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(filePath, json);
    }

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerInfo data = JsonUtility.FromJson<PlayerInfo>(json);

            m_BestScore = data.Score;
            m_BestName = data.Name;
        }
    }

    public void SyncGameData()
    {
        MainManagerInstance = GameObject.Find("MainManager").GetComponent<MainManager>();

        if (m_Name != "")
        {
            MainManagerInstance.UpdateBestScoreText(m_BestScore, m_BestName);
        }
    }
}
