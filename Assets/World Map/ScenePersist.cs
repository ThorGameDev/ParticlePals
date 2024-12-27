using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    public Unit BattleTarget
    {
        get
        {
            return m_BattleTarget;
        }
        set
        {
            m_BattleTarget = value;
        }
    }
    private static Unit m_BattleTarget;
}
