using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "ScriptableObjects/Attack", order = 1)]
public class Attack : ScriptableObject
{
    [Tooltip("0 = Status, 1 = Normal, 2 = Special")]
    public int type = 0;
    public Type catagory = Type.Neutral;
    public int damage = 60;
    [Tooltip("0 = Annihilation \n" +
        "1 = Attack Debuff \n" +
        "2 = Defence Debuff \n" +
        "3 = Special Attack Debuff \n" +
        "4 = Special Defence Debuff \n" +
        "5 = Speed Debuff \n" +
        "6 = Heal Self Half\n" +
        "7 = Attack Buff\n" +
        "8 = Defence Buff\n" +
        "9 = Special Attack Buff\n" +
        "10 = Special Defence Buff\n" +
        "11 = Speed Buff")]
    public int[] specialEffects;
    public float accuracy = 95;
}
