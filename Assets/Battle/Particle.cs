using UnityEngine;
[CreateAssetMenu(fileName = "Particle", menuName = "ScriptableObjects/Particle", order = 1)]
public class Particle : ScriptableObject
{
    public Sprite Image;
    //public Sprite BackImage; Add later
    public Type catagory = Type.Neutral;
    public Type secondCatagory = Type.Neutral;

    public int ID;
    public int TableID;
    public bool Antimatter;

    [TextArea(15, 20)]
    public string description;

    [Header("Min")]

    public int minHP;
    public int minAttack;
    public int minSPAttack;
    public int minDefence;
    public int minSPDefence;
    public int minSpeed;

    [Header("Max")]

    public int maxHP;
    public int maxAttack;
    public int maxSPAttack;
    public int maxDefence;
    public int maxSPDefence;
    public int maxSpeed;

    public Attack[] attacks;
}
