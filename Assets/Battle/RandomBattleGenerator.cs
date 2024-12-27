using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBattleGenerator : MonoBehaviour
{
    public Indexer possible;
    public int lV;
    public BattleManager battle;
    // Start is called before the first frame update
    void Start()
    {
        lV = UnityEngine.Random.Range(1, 101);
        Unit player = new Unit();
        player.level = lV;
        player.Base = possible.particles[UnityEngine.Random.Range(0, possible.particles.Length)];
        player.name = player.Base.name;
        player.hP = (int)((player.Base.maxHP - player.Base.minHP) / 100f * player.level) + player.Base.minHP;
        player.maxHP = player.hP;
        player.attack = (int)((player.Base.maxAttack - player.Base.minAttack) / 100f * player.level) + player.Base.minAttack;
        player.sPAttack = (int)((player.Base.maxSPAttack - player.Base.minSPAttack) / 100f * player.level) + player.Base.minSPAttack;
        player.defence = (int)((player.Base.maxDefence - player.Base.minDefence) / 100f * player.level) + player.Base.minDefence;
        player.sPDefence = (int)((player.Base.maxSPDefence - player.Base.minSPDefence) / 100f * player.level) + player.Base.minSPDefence;
        player.speed = (int)((player.Base.maxSpeed - player.Base.minSpeed) / 100f * player.level) + player.Base.minSpeed;
        player.atk1 = player.Base.attacks[UnityEngine.Random.Range(0, player.Base.attacks.Length)];
        player.atk2 = player.Base.attacks[UnityEngine.Random.Range(0, player.Base.attacks.Length)];
        player.atk3 = player.Base.attacks[UnityEngine.Random.Range(0, player.Base.attacks.Length)];
        player.atk4 = player.Base.attacks[UnityEngine.Random.Range(0, player.Base.attacks.Length)];
        battle.p1 = player;
        Unit foe = new Unit();
        foe.level = lV;
        foe.Base = possible.particles[UnityEngine.Random.Range(0, possible.particles.Length)];
        foe.name = foe.Base.name;
        foe.hP = (int)((foe.Base.maxHP - foe.Base.minHP) / 100f * foe.level) + foe.Base.minHP;
        foe.maxHP = foe.hP;
        foe.attack = (int)((foe.Base.maxAttack - foe.Base.minAttack) / 100f * foe.level) + foe.Base.minAttack;
        foe.sPAttack = (int)((foe.Base.maxSPAttack - foe.Base.minSPAttack) / 100f * foe.level) + foe.Base.minSPAttack;
        foe.defence = (int)((foe.Base.maxDefence - foe.Base.minDefence) / 100f * foe.level) + foe.Base.minDefence;
        foe.sPDefence = (int)((foe.Base.maxSPDefence - foe.Base.minSPDefence) / 100f * foe.level) + foe.Base.minSPDefence;
        foe.speed = (int)((foe.Base.maxSpeed - foe.Base.minSpeed) / 100f * foe.level) + foe.Base.minSpeed;
        foe.atk1 = foe.Base.attacks[UnityEngine.Random.Range(0,foe.Base.attacks.Length )];
        foe.atk2 = foe.Base.attacks[UnityEngine.Random.Range(0, foe.Base.attacks.Length)];
        foe.atk3 = foe.Base.attacks[UnityEngine.Random.Range(0, foe.Base.attacks.Length)];
        foe.atk4 = foe.Base.attacks[UnityEngine.Random.Range(0, foe.Base.attacks.Length)];
        battle.p2 = foe;
    }
}
