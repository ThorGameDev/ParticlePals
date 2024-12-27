using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public Indexer index;
    public OwnedParticle[] team;
    public void OnDestroy()
    {
        Save();
    }
    public void Start()
    {
        IndexerManagement.SetIndex(index);
        if(PlayerPrefs.GetInt("FirstPlay") != 1)
        {
            
            int particleindex = UnityEngine.Random.Range(0, index.particles.Length);
            Particle p = index.particles[particleindex];
            int atk1 = IndexerManagement.AttackToIndex(p.attacks[UnityEngine.Random.Range(0, p.attacks.Length)]);
            int atk2 = IndexerManagement.AttackToIndex(p.attacks[UnityEngine.Random.Range(0, p.attacks.Length)]);
            int atk3 = IndexerManagement.AttackToIndex(p.attacks[UnityEngine.Random.Range(0, p.attacks.Length)]);
            int atk4 = IndexerManagement.AttackToIndex(p.attacks[UnityEngine.Random.Range(0, p.attacks.Length)]);
            PlayerPrefs.SetString("Unit0Name", p.name);
            PlayerPrefs.SetInt("Unit0UniqueIdentifier", UnityEngine.Random.Range(-100000,100000));
            PlayerPrefs.SetInt("Unit0Base", particleindex);
            PlayerPrefs.SetInt("Unit0Level", 5);
            PlayerPrefs.SetInt("Unit0EXP", 500);
            PlayerPrefs.SetInt("Unit0MaxHP", (int)((p.maxHP - p.minHP) / 100f * 5) + p.minHP);
            PlayerPrefs.SetInt("Unit0HP", (int)((p.maxHP - p.minHP) / 100f * 5) + p.minHP);
            PlayerPrefs.SetInt("Unit0Attack", (int)((p.maxAttack - p.minAttack) / 100f * 5) + p.minAttack);
            PlayerPrefs.SetInt("Unit0SPAttack", (int)((p.maxSPAttack - p.minSPAttack) / 100f * 5) + p.minSPAttack);
            PlayerPrefs.SetInt("Unit0Defence", (int)((p.maxDefence - p.minDefence) / 100f * 5) + p.minDefence);
            PlayerPrefs.SetInt("Unit0SPDefence", (int)((p.maxSPDefence - p.minSPDefence) / 100f * 5) + p.minSPDefence);
            PlayerPrefs.SetInt("Unit0Speed", (int)((p.maxSpeed - p.minSpeed) / 100f * 5) + p.minSpeed);
            PlayerPrefs.SetInt("Unit0Attack1", atk1);
            PlayerPrefs.SetInt("Unit0Attack2", atk2);
            PlayerPrefs.SetInt("Unit0Attack3", atk3);
            PlayerPrefs.SetInt("Unit0Attack4", atk4);
            PlayerPrefs.SetInt("Unit0IsFaint", 0);
            PlayerPrefs.SetString("Unit0FoundAt", "A gift from the universe");
            PlayerPrefs.SetString("Unit0Owner", PlayerPrefs.GetString("PlayerName"));
            PlayerPrefs.SetInt("FirstPlay", 1);
        }
        Load();
    }
    public void Save()
    {
        PlayerPrefs.SetInt("TeamScize",team.Length - 1);
        for(int i = 0; i < team.Length; i++)
        {
            if (PlayerPrefs.GetInt($"Unit{i}UniqueIdentifier",2147483647) != team[i].uniqueIdentitier)
            {
                PlayerPrefs.SetString($"Unit{i}Name", team[i].name);
                PlayerPrefs.SetInt($"Unit{i}UniqueIdentifier", team[i].uniqueIdentitier);
                PlayerPrefs.SetInt($"Unit{i}Base", team[i].Base);
                PlayerPrefs.SetInt($"Unit{i}Level", team[i].level);
                PlayerPrefs.SetInt($"Unit{i}EXP", team[i].exp);
                PlayerPrefs.SetInt($"Unit{i}MaxHP", team[i].maxHP);
                PlayerPrefs.SetInt($"Unit{i}HP", team[i].hP);
                PlayerPrefs.SetInt($"Unit{i}Attack", team[i].attack);
                PlayerPrefs.SetInt($"Unit{i}SPAttack", team[i].sPAttack);
                PlayerPrefs.SetInt($"Unit{i}Defence", team[i].defence);
                PlayerPrefs.SetInt($"Unit{i}SPDefence", team[i].sPDefence);
                PlayerPrefs.SetInt($"Unit{i}Speed", team[i].speed);
                PlayerPrefs.SetInt($"Unit{i}Attack1", team[i].atk1);
                PlayerPrefs.SetInt($"Unit{i}Attack2", team[i].atk2);
                PlayerPrefs.SetInt($"Unit{i}Attack3", team[i].atk3);
                PlayerPrefs.SetInt($"Unit{i}Attack4", team[i].atk4);
                PlayerPrefs.SetInt($"Unit{i}IsFaint", team[i].isFaint);
                PlayerPrefs.SetString($"Unit{i}FoundAt", team[i].foundAt);
                PlayerPrefs.SetString($"Unit{i}Owner", team[i].Owner);
            }
        }
    }
    public void Load()
    {
        int teamScize = PlayerPrefs.GetInt("TeamScize");
        List<OwnedParticle> temp = new List<OwnedParticle>() { };
        for (int i = 0; i <= teamScize; i++)
        {
            OwnedParticle unit = new OwnedParticle() { };
            unit.name = PlayerPrefs.GetString($"Unit{i}Name");
            unit.uniqueIdentitier = PlayerPrefs.GetInt($"Unit{i}UniqueIdentifier");
            unit.Base = PlayerPrefs.GetInt($"Unit{i}Base");
            unit.level = PlayerPrefs.GetInt($"Unit{i}Level");
            unit.exp = PlayerPrefs.GetInt($"Unit{i}EXP");
            unit.maxHP = PlayerPrefs.GetInt($"Unit{i}MaxHP");
            unit.hP = PlayerPrefs.GetInt($"Unit{i}HP");
            unit.attack = PlayerPrefs.GetInt($"Unit{i}Attack");
            unit.sPAttack = PlayerPrefs.GetInt($"Unit{i}SPAttack");
            unit.defence = PlayerPrefs.GetInt($"Unit{i}Defence");
            unit.sPDefence = PlayerPrefs.GetInt($"Unit{i}SPDefence");
            unit.speed = PlayerPrefs.GetInt($"Unit{i}Speed");
            unit.atk1 = PlayerPrefs.GetInt($"Unit{i}Attack1");
            unit.atk2 = PlayerPrefs.GetInt($"Unit{i}Attack2");
            unit.atk3 = PlayerPrefs.GetInt($"Unit{i}Attack3");
            unit.atk4 = PlayerPrefs.GetInt($"Unit{i}Attack4");
            unit.isFaint = PlayerPrefs.GetInt($"Unit{i}IsFaint");
            unit.foundAt = PlayerPrefs.GetString($"Unit{i}FoundAt");
            unit.Owner = PlayerPrefs.GetString($"Unit{i}Owner");
            temp.Add(unit);
        }
        team = temp.ToArray();
    }
}
[System.Serializable]
public struct OwnedParticle
{
    public string name;
    public int uniqueIdentitier;
    public int Base;
    public int level;
    public int exp;
    public int maxHP;
    public int hP;
    public int attack;
    public int sPAttack;
    public int defence;
    public int sPDefence;
    public int speed;
    public int atk1;
    public int atk2;
    public int atk3;
    public int atk4;
    public int isFaint;
    public string foundAt;
    public string Owner;
}