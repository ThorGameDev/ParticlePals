using UnityEngine;

public class Foe : MonoBehaviour
{
    public int lV;
    public Unit thisParticle;
    public Indexer indexer;
    public SpriteRenderer sR;
    public float speed;
    public GameObject player;
    public float distanceFromOrigin;
    // Start is called before the first frame update
    void Start()
    {
        thisParticle.level = lV;
        thisParticle.Base = indexer.particles[UnityEngine.Random.Range(0, indexer.particles.Length)];
        thisParticle.name = thisParticle.Base.name;
        thisParticle.hP = (int)((thisParticle.Base.maxHP - thisParticle.Base.minHP) / 100f * thisParticle.level) + thisParticle.Base.minHP;
        thisParticle.maxHP = thisParticle.hP;
        thisParticle.attack = (int)((thisParticle.Base.maxAttack - thisParticle.Base.minAttack) / 100f * thisParticle.level) + thisParticle.Base.minAttack;
        thisParticle.sPAttack = (int)((thisParticle.Base.maxSPAttack - thisParticle.Base.minSPAttack) / 100f * thisParticle.level) + thisParticle.Base.minSPAttack;
        thisParticle.defence = (int)((thisParticle.Base.maxDefence - thisParticle.Base.minDefence) / 100f * thisParticle.level) + thisParticle.Base.minDefence;
        thisParticle.sPDefence = (int)((thisParticle.Base.maxSPDefence - thisParticle.Base.minSPDefence) / 100f * thisParticle.level) + thisParticle.Base.minSPDefence;
        thisParticle.speed = (int)((thisParticle.Base.maxSpeed - thisParticle.Base.minSpeed) / 100f * thisParticle.level) + thisParticle.Base.minSpeed;
        thisParticle.atk1 = thisParticle.Base.attacks[UnityEngine.Random.Range(0, thisParticle.Base.attacks.Length)];
        thisParticle.atk2 = thisParticle.Base.attacks[UnityEngine.Random.Range(0, thisParticle.Base.attacks.Length)];
        thisParticle.atk3 = thisParticle.Base.attacks[UnityEngine.Random.Range(0, thisParticle.Base.attacks.Length)];
        thisParticle.atk4 = thisParticle.Base.attacks[UnityEngine.Random.Range(0, thisParticle.Base.attacks.Length)];
        sR.sprite = thisParticle.Base.Image;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3();
        direction =  player.transform.position - this.transform.position;
        float dist = Vector2.Distance(this.transform.position, Vector2.zero);
        direction = direction.normalized;
        if (dist >= distanceFromOrigin)
        {
            Vector2 dir = Vector3.zero - this.transform.position;
            dir = dir.normalized;
            direction = new Vector3(dir.x - direction.x, dir.y - direction.y, 0);
        }
        this.transform.position += direction * Time.deltaTime * speed;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<ScenePersist>().BattleTarget = thisParticle;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
        }
    }
}
