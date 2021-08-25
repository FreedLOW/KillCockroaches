using UnityEngine;
using System.IO;
using System;

public class SaveScore : MonoBehaviour
{
    private Save save = new Save();

    private string path;

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "SaveData.json");
#else
        path = Path.Combine(Application.dataPath, "SaveData.json");
#endif

        if (File.Exists(path))
        {
            save = JsonUtility.FromJson<Save>(File.ReadAllText(path));
        }
    }

    private void Update()
    {
        CheckScore(GameManager.Instance.Score, GameManager.Instance.Last_Score, GameManager.Instance.Money);
        //GameManager.Instance.Score = save.maxScore;
        //GameManager.Instance.Last_Score = save.lastScore;
        GameManager.Instance.Money = save.money;

        if (MainMenuHUD.Instance != null)
        {
            MainMenuHUD.Instance.max_Score.text = save.maxScore.ToString();
            MainMenuHUD.Instance.last_Score.text = save.lastScore.ToString();
        }
    }

    public void CheckScore(int score, int last, int money)
    {
        if (score > 0 && score > save.maxScore)
        {
            save.maxScore = score;
        }

        if (last > 0 && money > 0)
        {
            save.lastScore = last;  //сохран€ю последний результат
            save.money = money;  //сохран€ю колличество денег
        }
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            File.WriteAllText(path, JsonUtility.ToJson(save));
    }
#endif

    private void OnApplicationQuit()
    {
        File.WriteAllText(path, JsonUtility.ToJson(save));
    }
}

[Serializable]
public class Save
{
    public int maxScore;
    public int lastScore;
    public int money;
}