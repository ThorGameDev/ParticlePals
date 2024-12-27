using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Unit p1;
    private Unit p1Internal;
    public Slider p1HP;
    public TMP_Text p1Name;
    public Image p1Pic;

    public TMP_Text attackButton1;
    public TMP_Text attackButton2;
    public TMP_Text attackButton3;
    public TMP_Text attackButton4;

    public GameObject choicePannel;
    public GameObject attackPannel;
    public GameObject unitsPannel;

    public Unit p2;
    private Unit p2Internal;
    public Slider p2HP;
    public TMP_Text p2Name;
    public Image p2Pic;

    public int playerChoice;

    public GameObject battleTextPannel;
    public TMP_Text battleText;

    public Indexer index;

    public GameObject holderButton;
    public GameObject unitsHolder;
    public ParticleStorage particleStorage;

    public void Start()
    {
        if (!HasUsableUnit())
        {
            StartCoroutine(FoeWon());
        }
        LoadPlayer();
        LoadFoe();
        int teamScize = Mathf.Clamp(PlayerPrefs.GetInt("TeamScize"),0,5);
        for (int i = 0; i <= teamScize; i++)
        {
            GameObject newHolderButton = Instantiate(holderButton);
            Slot s = newHolderButton.GetComponent<Slot>();
            s.slotId = i;
            s.particleStorage = particleStorage;
            newHolderButton.transform.SetParent(unitsHolder.transform);
        }
    }

    public void ChooseAttack()
    {
        attackPannel.SetActive(true);
        choicePannel.SetActive(false);
    }

    public void Update()
    {
        if (isInSwitch == false)
        {
            PlayerPrefs.SetInt("Unit0HP", p1.hP);
            PlayerPrefs.SetInt("Unit0IsFaint", p1.isFaint);
        }
        UpdateUI();
    }

    public IEnumerator Turn()
    {
        yield return new WaitForSeconds(0.5f);
        if (p1.speed > p2.speed)
        {
            StartCoroutine(PlayerAttack());

            yield return new WaitForSeconds(3);
            if (p2.hP <= 0)
            {
                StartCoroutine(PlayerWon());
                yield break;
            }
            StartCoroutine(FoeAttack());
            yield return new WaitForSeconds(3);
            if (p1.hP <= 0)
            {
                StartCoroutine(FoeWon());
                yield break;
            }
        }
        else
        {
            StartCoroutine(FoeAttack());
            yield return new WaitForSeconds(3);
            if (p1.hP <= 0)
            {
                StartCoroutine(FoeWon());
                yield break;
            }
            StartCoroutine(PlayerAttack());
            yield return new WaitForSeconds(3);
            if (p2.hP <= 0)
            {
                StartCoroutine(PlayerWon());
                yield break;
            }
        }
        choicePannel.SetActive(true);
    }

    public void Flee()
    {
        StartCoroutine(Fleeing());
    }

    public IEnumerator Fleeing()
    {
        attackPannel.SetActive(false);
        choicePannel.SetActive(false);
        battleText.text = $"{p1.name} Excaped!";
        battleTextPannel.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("World Map");
    }

    public IEnumerator PlayerWon()
    {
        p2Pic.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        battleText.text = $"{p2.name} was defeated! {p1.name} Wins!";
        battleTextPannel.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        int teamScize = Mathf.Clamp(PlayerPrefs.GetInt("TeamScize"), 0, 5);
        for (int i = 0; i <= teamScize; i++)
        {
            int EXP = (int)(((p2.level / PlayerPrefs.GetInt($"Unit{i}Level")) + 0.3f)* 47.297f * UnityEngine.Random.Range(0.9f,1.2f) * (i == 0? 1.1f : 1));
            int totalExp = PlayerPrefs.GetInt($"Unit{i}EXP") + EXP;
            int lVIncrease = (int)(totalExp / 100) - PlayerPrefs.GetInt($"Unit{i}Level");
            while(lVIncrease > 0)
            {
                lVIncrease--;
                battleText.text = $"{PlayerPrefs.GetString($"Unit{i}Name")} leveled up to LV {PlayerPrefs.GetInt($"Unit{i}Level")}!";
                battleTextPannel.SetActive(true);
                LevelUpUnit(i);
                yield return new WaitForSeconds(1.1f);
            }
            PlayerPrefs.SetInt($"Unit{i}EXP",totalExp);
            
        }

        battleText.text = "";
        battleTextPannel.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("World Map");
    }

    private void LevelUpUnit(int i)
    {
        particleStorage.save.Save();
        int level = PlayerPrefs.GetInt($"Unit{i}Level") + 1;
        PlayerPrefs.SetInt($"Unit{i}Level", level);
        Particle Base = index.particles[PlayerPrefs.GetInt($"Unit{i}Base")];
        PlayerPrefs.SetInt($"Unit{i}MaxHP", (int)((Base.maxHP - Base.minHP) / 100f * level) + Base.minHP);
        PlayerPrefs.SetInt($"Unit{i}HP", (int)((Base.maxHP - Base.minHP) / 100f * level) + Base.minHP);
        PlayerPrefs.SetInt($"Unit{i}Attack", (int)((Base.maxAttack - Base.minAttack) / 100f * level) + Base.minAttack);
        PlayerPrefs.SetInt($"Unit{i}SPAttack", (int)((Base.maxSPAttack - Base.minSPAttack) / 100f * level) + Base.minSPAttack);
        PlayerPrefs.SetInt($"Unit{i}Defence", (int)((Base.maxDefence - Base.minDefence) / 100f * level) + Base.minDefence);
        PlayerPrefs.SetInt($"Unit{i}SPDefence", (int)((Base.maxSPDefence - Base.minSPDefence) / 100f * level) + Base.minSPDefence);
        PlayerPrefs.SetInt($"Unit{i}Speed", (int)((Base.maxSpeed - Base.minSpeed) / 100f * level) + Base.minSpeed);
        particleStorage.save.Load();
    }

    public IEnumerator FoeWon()
    {
        p1.isFaint = 1;
        PlayerPrefs.SetInt("Unit0IsFaint", p1.isFaint);
        PlayerPrefs.SetInt("Unit0HP", p1.hP);
        p1Pic.color = Color.black;
        if (HasUsableUnit())
        {
            battleText.text = $"{p1.name} was defeated!";
            yield return new WaitForSeconds(1.1f);
            Switch();
            yield break;
        }
        yield return new WaitForSeconds(0.1f);
        battleText.text = $"{p1.name} was defeated! {p2.name} Wins!";
        battleTextPannel.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        battleText.text = "";
        battleTextPannel.SetActive(false);
        p1.isFaint = 0;
        PlayerPrefs.SetInt("Unit0IsFaint", p1.isFaint);
        PlayerPrefs.SetInt("Unit0HP", 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("World Map");
    }

    private IEnumerator PlayerAttack()
    {
        Attack choice = null;
        switch (playerChoice)
        {
            case 1:
                choice = p1.atk1;
                break;
            case 2:
                choice = p1.atk2;
                break;
            case 3:
                choice = p1.atk3;
                break;
            case 4:
                choice = p1.atk4;
                break;
            default:
                break;
        }
        battleText.text = $"{p1.name} used {choice.name}!";
        battleTextPannel.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        LaunchAttack(ref p1, ref p2, choice, out string extra);
        battleText.text = extra;
        yield return new WaitForSeconds(1);
        battleText.text = "";
        battleTextPannel.SetActive(false);
    }

    private IEnumerator FoeAttack()
    {
        Attack choice = null;
        switch (UnityEngine.Random.Range(1,5))
        {
            case 1:
                choice = p2.atk1;
                break;
            case 2:
                choice = p2.atk2;
                break;
            case 3:
                choice = p2.atk3;
                break;
            case 4:
                choice = p2.atk4;
                break;
            default:
                break;
        }
        battleText.text = $"{p2.name} used {choice.name}!";
        battleTextPannel.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        LaunchAttack(ref p2, ref p1, choice, out string extra);
        battleText.text = extra;
        yield return new WaitForSeconds(1);
        battleText.text = "";
        battleTextPannel.SetActive(false);
        
    }

    public bool HasUsableUnit()
    {
        for(int i = 0; i <= 5; i++)
        {
            if (PlayerPrefs.GetInt($"Unit{i}IsFaint", 1) == 0)
            {
                return true;
            }
        }
        return false;
    }

    public void SubmitInput(int Choice)
    {
        attackPannel.SetActive(false);
        StartCoroutine(Turn());
        playerChoice = Choice;
    }

    private void LaunchAttack(ref Unit attacker, ref Unit target, Attack choice, out string extra)
    {
        string attackInfo = "";
        // Status
        if (UnityEngine.Random.Range(0f, 100f) >= choice.accuracy)
        {
            extra = "Missed! ";
            return;
        }
        foreach (int effect in choice.specialEffects)
        {
            if (effect == 0) // Annihilation
            {
                if (p1.Base.TableID == p2.Base.TableID && p1.Base.Antimatter != p2.Base.Antimatter)
                {
                    attackInfo += "Annihilation Sucsesfull!";
                    target.hP = 0;
                    attacker.hP = 1;
                    attacker.attack = 1;
                    attacker.sPAttack = 1;
                    attacker.defence = 1;
                    attacker.sPDefence = 1;
                    attacker.speed = 1;
                    attacker.atk1 = index.flail;
                    attacker.atk2 = index.flail;
                    attacker.atk3 = index.flail;
                    attacker.atk4 = index.flail;
                }
                else
                {
                    attackInfo += "Annihilation Failed!";
                }
            }
            if (effect == 1) // Attack Debuff
            {
                target.attack = (int)(target.attack * 0.8f);
                attackInfo += $"{target.name}'s Atk. Reduced!";
            }
            if (effect == 2) // Defence Debuff
            {
                target.defence = (int)(target.defence * 0.8f);
                attackInfo += $"{target.name}'s Def. Reduced!";
            }
            if (effect == 3) // SP Attack Debuff
            {
                target.sPAttack = (int)(target.sPAttack * 0.8f);
                attackInfo += $"{target.name}'s Sp. Atk. Reduced!";
            }
            if (effect == 4) // SP Defecne Debuff
            {
                target.sPDefence = (int)(target.sPDefence * 0.8f);
                attackInfo += $"{target.name}'s Sp. Def. Reduced!";
            }
            if (effect == 5) // Speed Debuff
            {
                target.sPDefence = (int)(target.sPDefence * 0.8f);
                attackInfo += $"{target.name}'s Speed Reduced!";
            }
            if (effect == 6) // Self Heal Half
            {
                attacker.hP = Mathf.Clamp(attacker.hP + (int)(attacker.maxHP / 2), 0, attacker.maxHP);
                attackInfo += $"{attacker.name} Restored HP!";
            }
            if (effect == 7) // Attack Buff
            {
                attacker.attack = (int)(attacker.attack * 1.2f);
                attackInfo += $"{attacker.name}'s Atk. Increased!";
            }
            if (effect == 8) // Defence buff
            {
                attacker.defence = (int)(attacker.defence * 1.2f);
                attackInfo += $"{attacker.name}'s Def. Increased!";
            }
            if (effect == 9) // SP Attack buff
            {
                attacker.sPAttack = (int)(attacker.sPAttack * 1.2f);
                attackInfo += $"{attacker.name}'s SP Atk. Increased!";
            }
            if (effect == 10) // SP Defecne Buff
            {
                attacker.sPDefence = (int)(attacker.sPDefence * 1.2f);
                attackInfo += $"{attacker.name}'s SP Def. Increased!";
            }
            if (effect == 11) // Speed buff
            {
                attacker.speed = (int)(attacker.speed * 1.2f);
                attackInfo += $"{attacker.name}'s Speed Increased!";
            }
        }

        //Damage

        if (choice.damage > 0)
        {
            float critical = 1; // 1.5
            if (UnityEngine.Random.Range(0f, 100f) < 4.17)
            {
                critical = 1.5f;
                attackInfo += "Critical Hit! ";
            }
            float attackPower = choice.damage;
            float atk = 0;
            float def = 1;
            if (choice.type == 0)
            {
                atk = (attacker.attack + attacker.sPAttack) / 2;
                def = Mathf.Clamp((target.defence + target.sPDefence) / 2, 1, Mathf.Infinity);
            }
            if (choice.type == 1)
            {
                atk = attacker.attack;
                def = Mathf.Clamp(target.defence, 1, Mathf.Infinity);
            }
            if (choice.type == 2)
            {
                atk = attacker.sPAttack;
                def = Mathf.Clamp(target.sPDefence, 1, Mathf.Infinity);
            }
            float random = UnityEngine.Random.Range(0.85f, 1f); // 0.85,1
            float levleMultiplier = ((2 * attacker.level / 5) + 2);
            float typeAdvantage = 1;
            if ((target.Base.catagory == Type.Quark && choice.catagory == Type.Boson) ||
                (target.Base.catagory == Type.Lepton && choice.catagory == Type.Quark) ||
                (target.Base.catagory == Type.Boson && choice.catagory == Type.Lepton) ||
                (target.Base.catagory == Type.Positive && choice.catagory == Type.Negitive) ||
                (target.Base.catagory == Type.Negitive && choice.catagory == Type.Positive))
            {
                typeAdvantage *= 2;
                attackInfo += "Super Effective! ";
            }
            if ((target.Base.catagory == Type.Boson && choice.catagory == Type.Quark) ||
                (target.Base.catagory == Type.Quark && choice.catagory == Type.Lepton) ||
                (target.Base.catagory == Type.Lepton && choice.catagory == Type.Boson) ||
                (target.Base.catagory == Type.Positive && choice.catagory == Type.Positive) ||
                (target.Base.catagory == Type.Negitive && choice.catagory == Type.Negitive))
            {
                typeAdvantage /= 2;
                attackInfo += "Not Very Effective! ";
            }
            if ((target.Base.secondCatagory == Type.Quark && choice.catagory == Type.Boson) ||
                (target.Base.secondCatagory == Type.Lepton && choice.catagory == Type.Quark) ||
                (target.Base.secondCatagory == Type.Boson && choice.catagory == Type.Lepton) ||
                (target.Base.secondCatagory == Type.Positive && choice.catagory == Type.Negitive) ||
                (target.Base.secondCatagory == Type.Negitive && choice.catagory == Type.Positive))
            {
                typeAdvantage *= 2;
                attackInfo += "Super Effective! ";
            }
            if ((target.Base.secondCatagory == Type.Boson && choice.catagory == Type.Quark) ||
                (target.Base.secondCatagory == Type.Quark && choice.catagory == Type.Lepton) ||
                (target.Base.secondCatagory == Type.Lepton && choice.catagory == Type.Boson) ||
                (target.Base.secondCatagory == Type.Positive && choice.catagory == Type.Positive) ||
                (target.Base.secondCatagory == Type.Negitive && choice.catagory == Type.Negitive))
            {
                typeAdvantage /= 2;
                attackInfo += "Not Very Effective! ";
            }
            float damage = (((levleMultiplier * attackPower * atk / def) / 50) + 2) * critical * random * typeAdvantage;
            target.hP = target.hP - (int)damage;
            extra = attackInfo + ((int)damage).ToString() + " Damage!";
            return;
        }
        extra = attackInfo;
    }

    private void UpdateUI()
    {
        p1HP.maxValue = p1.maxHP;
        p1HP.value = p1.hP;
        p1Name.text = p1.name + " LV:" + p1.level.ToString();
        p2HP.maxValue = p2.maxHP;
        p2HP.value = p2.hP;
        p2Name.text = p2.name + " LV:" + p2.level.ToString();
        attackButton1.text = p1.atk1.name;
        attackButton2.text = p1.atk2.name;
        attackButton3.text = p1.atk3.name;
        attackButton4.text = p1.atk4.name;
        p1Pic.sprite = p1.Base.Image;
        p2Pic.sprite = p2.Base.Image;
        p1Pic.color = p1.isFaint == 0 ? Color.white : Color.black;
    }

    public void Capture()
    {
        choicePannel.SetActive(false);
        if (UnityEngine.Random.Range(0, 100f) >= 38)
        {
            SuccesfullCapture();
        }
        else
        {
            StartCoroutine(FailedCapture());
        }
    }

    public IEnumerator FailedCapture()
    {
        battleText.text = $"{p1.name} Failed to capture {p2.name}!";
        battleTextPannel.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        battleText.text = "";
        battleTextPannel.SetActive(false);

        StartCoroutine(FoeAttack());
        yield return new WaitForSeconds(3);
        if (p1.hP <= 0)
        {
            StartCoroutine(FoeWon());
            yield break;
        }
        choicePannel.SetActive(true);
    }

    private void SuccesfullCapture()
    {
        particleStorage.save.Save();
        choicePannel.SetActive(false);
        int i = PlayerPrefs.GetInt("TeamScize") + 1;
        PlayerPrefs.SetString($"Unit{i}Name", p2Internal.name);
        PlayerPrefs.SetInt($"Unit{i}UniqueIdentifier", UnityEngine.Random.Range(-100000, 100000));
        PlayerPrefs.SetInt($"Unit{i}Base", IndexerManagement.ParticleToIndex(p2Internal.Base));
        PlayerPrefs.SetInt($"Unit{i}Level", p2Internal.level);
        PlayerPrefs.SetInt($"Unit{i}IsFaint", 0);
        PlayerPrefs.SetInt($"Unit{i}EXP", p2Internal.level * 100);
        PlayerPrefs.SetInt($"Unit{i}MaxHP", p2Internal.maxHP);
        PlayerPrefs.SetInt($"Unit{i}HP", p2Internal.hP);
        PlayerPrefs.SetInt($"Unit{i}Attack", p2Internal.attack);
        PlayerPrefs.SetInt($"Unit{i}SPAttack", p2Internal.sPAttack);
        PlayerPrefs.SetInt($"Unit{i}Defence", p2Internal.defence);
        PlayerPrefs.SetInt($"Unit{i}SPDefence", p2Internal.sPDefence);
        PlayerPrefs.SetInt($"Unit{i}Speed", p2Internal.speed);
        PlayerPrefs.SetInt($"Unit{i}Attack1", IndexerManagement.AttackToIndex(p2Internal.atk1));
        PlayerPrefs.SetInt($"Unit{i}Attack2", IndexerManagement.AttackToIndex(p2Internal.atk2));
        PlayerPrefs.SetInt($"Unit{i}Attack3", IndexerManagement.AttackToIndex(p2Internal.atk3));
        PlayerPrefs.SetInt($"Unit{i}Attack4", IndexerManagement.AttackToIndex(p2Internal.atk4));
        PlayerPrefs.SetString($"Unit{i}FoundAt", "A battle shortly after the big bang");
        PlayerPrefs.SetString($"Unit{i}Owner", PlayerPrefs.GetString("PlayerName"));
        PlayerPrefs.SetInt("TeamScize", i);
        StartCoroutine(PlayerWon());
        particleStorage.save.Load();
    }
    
    public void Switch()
    {
        PlayerPrefs.SetInt("Unit0HP", p1.hP);
        PlayerPrefs.SetInt("Unit0IsFaint", p1.isFaint);
        particleStorage.save.Load();
        unitsPannel.SetActive(true);
        isSwitching = true;
        StartCoroutine(Switching());
    }

    public void DoneSwitching()
    {
        if (PlayerPrefs.GetInt("Unit0IsFaint") == 0)
        {
            unitsPannel.SetActive(false);
            isSwitching = false;
        }
    }

    public bool isSwitching;

    public bool isInSwitch;

    public IEnumerator Switching()
    {
        isInSwitch = true;
        attackPannel.SetActive(false);
        choicePannel.SetActive(false);
        string oldName = p1.name;
        battleText.text = $"{oldName} is being swaped out...";
        battleTextPannel.SetActive(true);
        while (isSwitching)
        {
            yield return new WaitForEndOfFrame();
        }
        LoadPlayer();
        UpdateUI();
        battleText.text = $"{oldName} was swaped out for {p1.name}";
        yield return new WaitForSeconds(1.1f);
        battleText.text = "";
        battleTextPannel.SetActive(false);
        StartCoroutine(FoeAttack());
        yield return new WaitForSeconds(3);
        if (p1.hP <= 0)
        {
            StartCoroutine(FoeWon());
            yield break;
        }
        choicePannel.SetActive(true);
        isInSwitch = false;
    }

    public void LoadPlayer()
    {
        Unit player = new Unit();
        player.name = PlayerPrefs.GetString("Unit0Name");
        player.Base = index.particles[PlayerPrefs.GetInt("Unit0Base")];
        player.level = PlayerPrefs.GetInt("Unit0Level");
        player.maxHP = PlayerPrefs.GetInt("Unit0MaxHP");
        player.hP = PlayerPrefs.GetInt("Unit0HP");
        player.attack = PlayerPrefs.GetInt("Unit0Attack");
        player.sPAttack = PlayerPrefs.GetInt("Unit0SPAttack");
        player.defence = PlayerPrefs.GetInt("Unit0Defence");
        player.sPDefence = PlayerPrefs.GetInt("Unit0SPDefence");
        player.speed = PlayerPrefs.GetInt("Unit0Speed");
        player.atk1 = index.attacks[PlayerPrefs.GetInt("Unit0Attack1")];
        player.atk2 = index.attacks[PlayerPrefs.GetInt("Unit0Attack2")];
        player.atk3 = index.attacks[PlayerPrefs.GetInt("Unit0Attack3")];
        player.atk4 = index.attacks[PlayerPrefs.GetInt("Unit0Attack4")];
        player.isFaint = PlayerPrefs.GetInt("Unit0IsFaint");
        p1 = player;
        p1Internal = p1;
    }

    public void LoadFoe()
    {
        p2 = FindObjectOfType<ScenePersist>().BattleTarget;
        p2Internal = p2;
    }
}

[System.Serializable]
public struct Unit
{
    public string name;
    public Particle Base;
    public int level;
    public int maxHP;
    public int hP;
    public int attack;
    public int sPAttack;
    public int defence;
    public int sPDefence;
    public int speed;
    public int isFaint;
    public Attack atk1;
    public Attack atk2;
    public Attack atk3;
    public Attack atk4;
}
public enum Type
{
    Neutral,
    Quark,
    Lepton,
    Boson,
    Positive,
    Negitive,
}