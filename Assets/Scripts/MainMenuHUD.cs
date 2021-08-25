using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuHUD : MonoBehaviour
{
    private bool vibration = true;

    [SerializeField] Sprite onImage;
    [SerializeField] Sprite offImage;

    [SerializeField] private AudioClip clickBtn;
    [SerializeField] private AudioMixer mixer;

    public Text max_Score;
    public Text last_Score;

    public GameObject bonusPanel;

    #region Text to translate
    [SerializeField] Text soundText;
    [SerializeField] Text musicText;
    [SerializeField] Text vibrationText;
    [SerializeField] Text languagesText;
    [SerializeField] Text notificationText;
    [SerializeField] Text maxScoreText;
    [SerializeField] Text lastScoreText;
    [SerializeField] Text buyText;
    [SerializeField] Text acceptText;
    [SerializeField] Text howMoneyText;
    #endregion

    public static MainMenuHUD Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        CheckLanguage();

        PlayerPrefs.SetString("Vibration", vibration.ToString());

        //max_Score.text = GameManager.Instance.Score.ToString();
        //last_Score.text = GameManager.Instance.Last_Score.ToString();
    }

    public void ClickButton(AudioSource audio)
    {
        audio.PlayOneShot(clickBtn);
    }

    public void SetLanguage()
    {
        musicText.text = LanguageSystem.Lang.music;
        //bestScore.fontSize = 190;
        vibrationText.text = LanguageSystem.Lang.vibration;
        languagesText.text = LanguageSystem.Lang.languages;
        notificationText.text = LanguageSystem.Lang.notification;
        soundText.text = LanguageSystem.Lang.sound;
        maxScoreText.text = LanguageSystem.Lang.max_score;
        lastScoreText.text = LanguageSystem.Lang.last_score;
        buyText.text = LanguageSystem.Lang.buy;
        acceptText.text = LanguageSystem.Lang.accept;
        howMoneyText.text = LanguageSystem.Lang.money;
    }

    void CheckLanguage()
    {
        if (PlayerPrefs.GetString("Language") == "ru_RU")
        {
            musicText.text = LanguageSystem.Lang.music;
            vibrationText.text = LanguageSystem.Lang.vibration;
            languagesText.text = LanguageSystem.Lang.languages;
            notificationText.text = LanguageSystem.Lang.notification;
            soundText.text = LanguageSystem.Lang.sound;
            maxScoreText.text = LanguageSystem.Lang.max_score;
            lastScoreText.text = LanguageSystem.Lang.last_score;
            buyText.text = LanguageSystem.Lang.buy;
            acceptText.text = LanguageSystem.Lang.accept;
            howMoneyText.text = LanguageSystem.Lang.money;
        }
        else if (PlayerPrefs.GetString("Language") == "en_US")
        {
            musicText.text = LanguageSystem.Lang.music;
            vibrationText.text = LanguageSystem.Lang.vibration;
            languagesText.text = LanguageSystem.Lang.languages;
            notificationText.text = LanguageSystem.Lang.notification;
            soundText.text = LanguageSystem.Lang.sound;
            maxScoreText.text = LanguageSystem.Lang.max_score;
            lastScoreText.text = LanguageSystem.Lang.last_score;
            buyText.text = LanguageSystem.Lang.buy;
            acceptText.text = LanguageSystem.Lang.accept;
            howMoneyText.text = LanguageSystem.Lang.money;
        }
        else if (PlayerPrefs.GetString("Language") == "ch_CH")
        {
            musicText.text = LanguageSystem.Lang.music;
            vibrationText.text = LanguageSystem.Lang.vibration;
            languagesText.text = LanguageSystem.Lang.languages;
            notificationText.text = LanguageSystem.Lang.notification;
            soundText.text = LanguageSystem.Lang.sound;
            maxScoreText.text = LanguageSystem.Lang.max_score;
            lastScoreText.text = LanguageSystem.Lang.last_score;
            buyText.text = LanguageSystem.Lang.buy;
            acceptText.text = LanguageSystem.Lang.accept;
            howMoneyText.text = LanguageSystem.Lang.money;
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ValueChange(Scrollbar scrollbar)
    {
        float value = scrollbar.value;
        if (value == 1)
            scrollbar.GetComponent<Image>().sprite = onImage;
        else scrollbar.GetComponent<Image>().sprite = offImage;
    }

    public void MusicControll(Scrollbar scrollbar)
    {
        var value = scrollbar.value;
        if (value >= 0.5f)
        {
            mixer.SetFloat("m_volume", 0f);
        }
        else if (value < 0.5f)
        {
            mixer.SetFloat("m_volume", -80f);
        }
    }

    public void SoundControll(Scrollbar scrollbar)
    {
        var value = scrollbar.value;
        if (value == 1)
        {
            mixer.SetFloat("s_volume", 0f);
        }
        else
        {
            mixer.SetFloat("s_volume", -80f);
        }
    }

    public void VibrationControll(Scrollbar scrollbar)
    {
        var value = scrollbar.value;
        if (value == 1)
        {
            GameManager.Instance.CanVibrate = true;
        }
        else
        {
            GameManager.Instance.CanVibrate = false;
        }
    }

    public void NotificationControll(Scrollbar scrollbar)
    {
        var value = scrollbar.value;
        if (value == 1)
        {
            GameManager.Instance.Importance = Unity.Notifications.Android.Importance.High;
        }
        else
        {
            GameManager.Instance.Importance = Unity.Notifications.Android.Importance.Low;
        }
    }
}