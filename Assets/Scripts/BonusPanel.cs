using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusPanel : MonoBehaviour
{
    [SerializeField] GameObject[] shadows;
    [SerializeField] Image[] balls;
    int randomItem;

    private void Start()
    {
        randomItem = Random.Range(0, shadows.Length);
        shadows[randomItem].SetActive(false);
    }

    public void AcceptSkin()
    {
        GameManager.Instance.BallSkin = balls[randomItem].sprite;
    }
}