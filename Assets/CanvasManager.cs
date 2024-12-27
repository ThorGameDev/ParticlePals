using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public SaveData save;
    public ParticleStorage ps;
    public GameObject holderButton;
    public GameObject unitsPannel;
    public GameObject unitsHolder;
    public GameObject statusPanel;
    public GameObject particleDex;
    public RectTransform MainPanel;
    public float inX;
    public float outX;
    public float lerpSpeed;
    public bool IsOut;
    public TMP_Text particleStatus;
    public TMP_Text playerName;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = Vector3.one;
        int teamScize = PlayerPrefs.GetInt("TeamScize");
        for (int i = 0; i <= teamScize; i++)
        {
            GameObject newHolderButton = Instantiate(holderButton);
            Slot s = newHolderButton.GetComponent<Slot>();
            s.slotId = i;
            s.particleStorage = ps;
            newHolderButton.transform.SetParent(unitsHolder.transform);
        }
        playerName.text = PlayerPrefs.GetString("PlayerName");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IsOut = !IsOut;
        }
        if (IsOut)
        {
            float X = Mathf.Lerp(MainPanel.anchoredPosition.x, outX, Time.fixedDeltaTime * lerpSpeed);
            MainPanel.anchoredPosition = new Vector3(X,0,0);
            Time.timeScale = 0;
        }
        else
        {
            float X = Mathf.Lerp(MainPanel.anchoredPosition.x, inX, Time.fixedDeltaTime * lerpSpeed);
            MainPanel.anchoredPosition = new Vector3(X, 0, 0);
            Time.timeScale = 1;
            unitsPannel.SetActive(false);
            statusPanel.SetActive(false);
        }
        Particle Bace = IndexerManagement.index.particles[PlayerPrefs.GetInt("Unit0Base")];
        particleStatus.text =
            $"\nName: {PlayerPrefs.GetString("Unit0Name")}\n" +
            $"\nParticle: {Bace.name}\n" +
            $"\nDescription: {Bace.description}\n" +
            $"\nLevel: {PlayerPrefs.GetInt("Unit0Level")}\n" +
            $"\nEXP: {PlayerPrefs.GetInt("Unit0EXP")}\n" +
            $"\nHP: {PlayerPrefs.GetInt("Unit0HP")}/{PlayerPrefs.GetInt("Unit0MaxHP")}\n" +
            $"\nAttack: {PlayerPrefs.GetInt("Unit0Attack")}\n" +
            $"\nSpecial Attack: {PlayerPrefs.GetInt("Unit0SPAttack")}\n" +
            $"\nDefence: {PlayerPrefs.GetInt("Unit0Defence")}\n" +
            $"\nSpecial Defence: {PlayerPrefs.GetInt("Unit0SPDefence")}\n" +
            $"\nSpeed: {PlayerPrefs.GetInt("Unit0Speed")}\n" +
            $"\nFound At: {PlayerPrefs.GetString("Unit0FoundAt")}\n" +
            $"\nOriginal Owner: {PlayerPrefs.GetString("Unit0Owner")}\n" +
            $"\nAttack 1: {IndexerManagement.index.attacks[PlayerPrefs.GetInt("Unit0Attack1")].name}\n" +
            $"\nAttack 2: {IndexerManagement.index.attacks[PlayerPrefs.GetInt("Unit0Attack2")].name}\n" +
            $"\nAttack 3: {IndexerManagement.index.attacks[PlayerPrefs.GetInt("Unit0Attack3")].name}\n" +
            $"\nAttack 4: {IndexerManagement.index.attacks[PlayerPrefs.GetInt("Unit0Attack4")].name}\n";
    }
    public void QuitGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }
    public void ShowUnitsPannel()
    {
        bool Enable = unitsPannel.activeSelf;
        unitsPannel.SetActive(!Enable);
        statusPanel.SetActive(false);
    }
    public void ShowStatusPanel()
    {
        bool Enable = statusPanel.activeSelf;
        unitsPannel.SetActive(false);
        statusPanel.SetActive(!Enable);
    }
}
