using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject foe;
    public float distanceFromOrigin;
    public float distanceFromPlayer;
    public float minDistanceFromPlayer;
    public float interval;
    public float timeInInterval;
    // Update is called once per frame
    void Update()
    {
        timeInInterval += Time.deltaTime;
        if(timeInInterval >= interval)
        {
            timeInInterval -= interval;
            Vector2 point = UnityEngine.Random.insideUnitCircle * distanceFromPlayer + new Vector2(player.transform.position.x,player.transform.position.y);
            while(Vector2.Distance(point,Vector2.zero) >= distanceFromOrigin || Vector2.Distance(point,player.transform.position) <= minDistanceFromPlayer)
            {
                point = UnityEngine.Random.insideUnitCircle * distanceFromPlayer + new Vector2(player.transform.position.x, player.transform.position.y);
            }
            GameObject newFoe = Instantiate(foe);
            newFoe.transform.position = point;
            Foe foeBehavior = newFoe.GetComponent<Foe>();
            int lv0 = PlayerPrefs.GetInt("Unit0Level");
            int lv1 = PlayerPrefs.GetInt("Unit1Level", lv0);
            int lv2 = PlayerPrefs.GetInt("Unit2Level", lv1);
            int lv3 = PlayerPrefs.GetInt("Unit2Level", lv2);
            int lv4 = PlayerPrefs.GetInt("Unit2Level", lv3);
            int lv5 = PlayerPrefs.GetInt("Unit2Level", lv4);
            foeBehavior.lV = Mathf.Clamp(((lv0 + lv0 + lv0 + lv1 + lv2 + lv3 + lv4 + lv5) / 8) + UnityEngine.Random.Range(-2,1),1,100);
            foeBehavior.player = player;
        }
    }
}
