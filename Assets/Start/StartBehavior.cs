using UnityEngine;
using TMPro;
public class StartBehavior : MonoBehaviour
{
    public GameObject Title;
    private Vector2 Position;
    private bool InSceneTransist = false;

    public GameObject FirstPlayPannel;
    public GameObject PlayPannel;
    public GameObject NewGamePannel;
    public GameObject ConfirmPannel;
    public TMP_InputField Name;
    public bool LookingAtPannel;
    public bool FirstPlay;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        FirstPlay = PlayerPrefs.GetInt("FirstPlay") == 0? true : false;
        Debug.Log(PlayerPrefs.GetInt("FirstPlay"));
        Position = Title.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Title.transform.position = Position + new Vector2(0,Mathf.Sin(Time.time) * 20);
        if (Input.anyKeyDown && LookingAtPannel == false)
        {
            LookingAtPannel = true;
            if (FirstPlay)
            {
                FirstPlayPannel.SetActive(true);
            }
            else
            {
                PlayPannel.SetActive(true);
            }

        }
    }

    public void BeginGame()
    {
        if (InSceneTransist == false)
        {
            InSceneTransist = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("World Map");
        }
    }
    public void NewGame()
    {
        if (FirstPlay)
        {
            FirstPlayPannel.SetActive(false);
            NewGamePannel.SetActive(true);
        }
        else
        {
            ConfirmPannel.SetActive(true);
            PlayPannel.SetActive(false);
        }
    }
    public void ConfirmNewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("PlayerName", Name.text);
        UnityEngine.SceneManagement.SceneManager.LoadScene("World Map");
    }
    public void Yes()
    {
        NewGamePannel.SetActive(true);
        ConfirmPannel.SetActive(false);
    }
    public void No()
    {
        ConfirmPannel.SetActive(false);
        PlayPannel.SetActive(true);
    }
    public void Back()
    {
        PlayPannel.SetActive(false);
        FirstPlayPannel.SetActive(false);
        NewGamePannel.SetActive(false);
        LookingAtPannel = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
