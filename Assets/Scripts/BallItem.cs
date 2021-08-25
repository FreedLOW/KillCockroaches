using UnityEngine;

[CreateAssetMenu(fileName = "Ball", menuName = "Create Ball", order = 51)]
public class BallItem : ScriptableObject
{
    public Sprite ball;
    public int price;
    public int id;
}