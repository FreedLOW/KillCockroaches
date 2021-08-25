using UnityEngine;
using UnityEngine.UI;

public class SkinShop : MonoBehaviour
{
    [SerializeField] BallItem[] balls;

    [SerializeField] Image ballImage;
    [SerializeField] Text priceBallText;
    int ballId;

    [SerializeField] Text moneyText;

    [SerializeField] Text attentionText;
    [SerializeField] Animator attention;

    private void Start()
    {
        ballImage.sprite = balls[0].ball;
        priceBallText.text = balls[0].price.ToString();
        ballId = balls[0].id;

        //foreach (var item in balls)
        //{
        //    GameManager.Instance.Ball.Add(item.id, item.ball);
        //}

        //print(GameManager.Instance.Ball.Count);
    }

    private void Update()
    {
        moneyText.text = GameManager.Instance.Money.ToString();
    }

    public void LeftScroll()
    {
        if (ballId > 0)
        {
            ballId--;
            ballImage.sprite = balls[ballId].ball;
            priceBallText.text = balls[ballId].price.ToString();
        }
    }

    public void RightScroll()
    {
        if (ballId < balls.Length - 1)
        {
            ballId++;
            ballImage.sprite = balls[ballId].ball;
            priceBallText.text = balls[ballId].price.ToString();
        }
    }

    public void BuyBall()
    {
        if (balls[ballId].price <= GameManager.Instance.Money && balls[ballId].ball != GameManager.Instance.BallSkin)
        {
            GameManager.Instance.Money -= balls[ballId].price;
            GameManager.Instance.BallSkin = balls[ballId].ball;
        }
        else if (balls[ballId].ball == GameManager.Instance.BallSkin)
        {
            //сделать анимацию появления надписи что есть такой скин
            print("buy enother skin");
            attentionText.text = LanguageSystem.Lang.enother_skin;
            attention.SetTrigger("Show");
        }
        else
        {
            //сделать анимацию появления надписи что не достаточно денег
            print("need more money");
            attentionText.text = LanguageSystem.Lang.not_enough;
            attention.SetTrigger("Show");
        }
    }
}