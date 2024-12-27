using UnityEngine;

public class LoadBattle : MonoBehaviour
{
    public Indexer possible;
    public int lV;
    public BattleManager battle;
    // Start is called before the first frame update
    void Start()
    {
        lV = UnityEngine.Random.Range(1, 101);
        Unit player = new Unit();
        player.name = PlayerPrefs.GetString("Unit0Name");
        player.Base = possible.particles[PlayerPrefs.GetInt("Unit0Base")];
        player.level = PlayerPrefs.GetInt("Unit0Level");
        player.hP = PlayerPrefs.GetInt("Unit0MaxHP");
        player.maxHP = PlayerPrefs.GetInt("Unit0HP");
        player.attack = PlayerPrefs.GetInt("Unit0Attack");
        player.sPAttack = PlayerPrefs.GetInt("Unit0SPAttack");
        player.defence = PlayerPrefs.GetInt("Unit0Defence");
        player.sPDefence = PlayerPrefs.GetInt("Unit0SPDefence");
        player.speed = PlayerPrefs.GetInt("Unit0Speed");
        player.atk1 = possible.attacks[PlayerPrefs.GetInt("Unit0Attack1")];
        player.atk2 = possible.attacks[PlayerPrefs.GetInt("Unit0Attack2")];
        player.atk3 = possible.attacks[PlayerPrefs.GetInt("Unit0Attack3")];
        player.atk4 = possible.attacks[PlayerPrefs.GetInt("Unit0Attack4")];
        battle.p1 = player;

        Unit foe = new Unit();
        foe = FindObjectOfType<ScenePersist>().BattleTarget;
        /*
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
        foe.atk1 = foe.Base.attacks[UnityEngine.Random.Range(0, foe.Base.attacks.Length)];
        foe.atk2 = foe.Base.attacks[UnityEngine.Random.Range(0, foe.Base.attacks.Length)];
        foe.atk3 = foe.Base.attacks[UnityEngine.Random.Range(0, foe.Base.attacks.Length)];
        foe.atk4 = foe.Base.attacks[UnityEngine.Random.Range(0, foe.Base.attacks.Length)];
        */
        battle.p2 = foe;
    }
}
