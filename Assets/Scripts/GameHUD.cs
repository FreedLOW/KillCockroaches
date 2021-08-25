using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance;

    #region text to translate
    [SerializeField] Text loseText;
    [SerializeField] Text restartText;
    [SerializeField] Text mainMenuText;
    #endregion

    [SerializeField] GameObject loseScren;

    [SerializeField] Text lastScoreText;

    [SerializeField] private AudioClip clickBtn;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        CheckLanguage();

        GameManager.Instance.Last_Score = 0;
        GameManager.Instance.Score = 0;
    }

    public void ClickButton(AudioSource audio)
    {
        audio.PlayOneShot(clickBtn);
    }

    private void CheckLanguage()
    {
        if (PlayerPrefs.GetString("Language") == "ru_RU")
        {
            loseText.text = LanguageSystem.Lang.lose;
            restartText.text = LanguageSystem.Lang.restart;
            mainMenuText.text = LanguageSystem.Lang.mainMenu;
        }
        else if (PlayerPrefs.GetString("Language") == "en_US")
        {
            loseText.text = LanguageSystem.Lang.lose;
            restartText.text = LanguageSystem.Lang.restart;
            mainMenuText.text = LanguageSystem.Lang.mainMenu;
        }
        else if (PlayerPrefs.GetString("Language") == "ch_CH")
        {
            loseText.text = LanguageSystem.Lang.lose;
            restartText.text = LanguageSystem.Lang.restart;
            mainMenuText.text = LanguageSystem.Lang.mainMenu;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowLoseScreen()
    {
        lastScoreText.text = GameManager.Instance.Last_Score.ToString();

        var vibration = GameManager.Instance.CanVibrate;
        if (vibration)
        {
            loseScren.SetActive(true);
            Handheld.Vibrate();
        }
        else loseScren.SetActive(true);
    }
}