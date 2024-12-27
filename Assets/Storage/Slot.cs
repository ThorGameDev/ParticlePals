using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot : MonoBehaviour
{
    public ParticleStorage particleStorage;
    public int slotId;
    public TMP_Text nameDisplay;
    public Image pictureOfThing;

    void Start()
    {
        UpdateDesplay();
    }

    public void UpdateDesplay()
    {
        pictureOfThing.color = PlayerPrefs.GetInt($"Unit{slotId}IsFaint") == 0 ? Color.white : new Color32(40,40,40,255);
        nameDisplay.text = PlayerPrefs.GetString($"Unit{slotId}Name");
        pictureOfThing.sprite = IndexerManagement.index.particles[PlayerPrefs.GetInt($"Unit{slotId}Base")].Image;
    }

    public void CallHoldCommand()
    {
        particleStorage.OnClickInventorySlot(this);
    }
}
