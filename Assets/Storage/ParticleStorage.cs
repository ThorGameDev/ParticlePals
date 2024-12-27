using UnityEngine;

public class ParticleStorage : MonoBehaviour
{
    Slot heald = null;
    public SaveData save;
    private void Start()
    {
        save = FindObjectOfType<SaveData>();
    }
    public void OnClickInventorySlot(Slot slot)
    {
        if (heald == null)
        {
            heald = slot;
        }
        else
        {
            OwnedParticle temp = save.team[heald.slotId];
            save.team[heald.slotId] = save.team[slot.slotId];
            save.team[slot.slotId] = temp;
            save.Save();
            slot.UpdateDesplay();
            heald.UpdateDesplay();
            heald = null;
        }
    }
}
