using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class LanguageSystem : MonoBehaviour
{
    private static LanguageSystem m_instance;
    public static LanguageSystem m_Instance { get => m_instance; }

    private string json;

    public static Lang Lang = new Lang();

    public int indexLang = 1;
    private string[] languageArray = { "en_US", "ru_RU", "ch_CH" };

    [SerializeField] Image langImage;
    [SerializeField] Sprite[] langFlags;

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
            }
            else if (Application.systemLanguage == SystemLanguage.Chinese)
            {
                PlayerPrefs.SetString("Language", "ch_CH");
            }
            else PlayerPrefs.SetString("Language", "en_US");
        }

        LanguageLoad();
    }

    private void Start()
    {
        for (int i = 0; i < languageArray.Length; i++)
        {
            if (PlayerPrefs.GetString("Language") == languageArray[i])
            {
                indexLang = i + 1;
                langImage.sprite = langFlags[i];
                break;
            }
        }
    }

    public void LanguageLoad()
    {
#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_REMOTE
                string path = Path.Combine(Application.streamingAssetsPath, "Languages/" + PlayerPrefs.GetString("Language") + ".json");
                WWW reader = new WWW(path);
                while (!reader.isDone) { }
                json = reader.text;
                Lang = JsonUtility.FromJson<Lang>(json);
#endif

#if UNITY_EDITOR
        json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Language") + ".json");

        Lang = JsonUtility.FromJson<Lang>(json);
#endif
    }

    public void SwitchLanguage()
    {
        if (indexLang != languageArray.Length)
            indexLang++;
        else indexLang = 1;

        PlayerPrefs.SetString("Language", languageArray[indexLang - 1]);

        langImage.sprite = langFlags[indexLang - 1];

        LanguageLoad();
    }
}

public class Lang
{
    //поля которые будут переводиться (ключи):
    public string sound;
    public string max_score;
    public string music;
    public string vibration;
    public string languages;
    public string notification;
    public string last_score;
    public string buy;
    public string accept;
    public string restart;
    public string mainMenu;
    public string money;
    public string lose;
    public string not_enough;
    public string enother_skin;
    public string title_n;
    public string text_n;
}