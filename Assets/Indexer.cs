using UnityEngine;

[CreateAssetMenu(fileName = "Index", menuName = "ScriptableObjects/Index", order = 1)]
public class Indexer : ScriptableObject
{
    public Particle[] particles;
    public Attack[] attacks;
    public Attack flail;
}
public static class IndexerManagement
{
    public static Indexer index;
    public static void SetIndex(Indexer indexer)
    {
        index = indexer;
    }
    public static int AttackToIndex(Attack attack)
    {
        for (int i = 0; i <= index.attacks.Length; i++)
        {
            if (attack == index.attacks[i])
            {
                return i;
            }
        }
        return -1;
    }
    public static int ParticleToIndex(Particle particle)
    {
        for (int i = 0; i <= index.particles.Length; i++)
        {
            if (particle == index.particles[i])
            {
                return i;
            }
        }
        return -1;
    }
}

