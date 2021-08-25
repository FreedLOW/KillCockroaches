using System;
using UnityEngine;
using Unity.Notifications.Android;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int last_Score;
    [SerializeField] private int money;

    public int Score { get => score; set => score = value; }
    public int Last_Score { get => last_Score; set => last_Score = value; }
    public int Money { get => money; set => money = value; }

    Sprite ballSkin;
    public Sprite BallSkin { get => ballSkin; set => ballSkin = value; }

    Dictionary<int, Sprite> ball = new Dictionary<int, Sprite>();
    public Dictionary<int, Sprite> Ball { get => ball; set => ball = value; }

    Importance importance;
    public Importance Importance { get => importance; set => importance = value; }

    bool canVibrate = true;
    public bool CanVibrate { get => canVibrate; set => canVibrate = value; }

    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameManager = Instantiate(Resources.Load("Prefabs/GameManager")) as GameObject;

                _instance = gameManager.GetComponent<GameManager>();
            }
            return _instance;
        }
    }
    #endregion

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            if (_instance != this)
                Destroy(gameObject);
        }

        importance = Importance.High;

        AndroidNotificationChannel channel = new AndroidNotificationChannel()
        {
            Name = "KillCockroaches | Game",
            Description = "Notification about skin!",
            Id = "skin",
            Importance = importance
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //проверка на первый вход
        if (!PlayerPrefs.HasKey("FirstSession"))
        {
            SendNotification();
            CheckVisit();
        }

        //проверка что уже прошли сутки для отправки уведомления и игрок заходил в игру
        if (PlayerPrefs.HasKey("FirstSession"))
        {
            if (DateTime.Now > DateTime.Parse(PlayerPrefs.GetString("FirstSession"))
                && DateTime.Now < DateTime.Parse(PlayerPrefs.GetString("FirstSession")).AddHours(25)
                && DateTime.Now > DateTime.Parse(PlayerPrefs.GetString("FirstSession")).AddHours(23))
            {
                SendNotification();
                CheckVisit();
                MainMenuHUD.Instance.bonusPanel.SetActive(true);
            }
        }
    }

    public void SendNotification()
    {
        AndroidNotification notification = new AndroidNotification()
        {
            Title = LanguageSystem.Lang.title_n,
            Text = LanguageSystem.Lang.text_n,
            FireTime = DateTime.Now.AddHours(24),
            SmallIcon = "icon_small",
            LargeIcon = "icon_large"
        };

        AndroidNotificationCenter.SendNotification(notification, "skin");
    }

    void CheckVisit()
    {
        PlayerPrefs.SetString("FirstSession", DateTime.Now.ToString());
    }
}