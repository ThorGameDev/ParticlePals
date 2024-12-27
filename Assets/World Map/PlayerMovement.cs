using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float distanceFromOrigin;
    public SpriteRenderer sR;
    public Indexer indexer;
    public Transform CameraObject;
    // Update is called once per frame
    public void Start()
    {
        transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
        CameraObject.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    public void OnDestroy()
    {
        PlayerPrefs.SetFloat("PlayerX",transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
    }
    void Update()
    {
        sR.sprite = indexer.particles[PlayerPrefs.GetInt("Unit0Base")].Image;
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction.y += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction.y -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction.x += 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction.x -= 1;
        }
        float dist = Vector2.Distance(this.transform.position, Vector2.zero);
        direction = direction.normalized;
        if (dist >= distanceFromOrigin)
        {
            Vector2 dir = Vector3.zero - this.transform.position;
            dir = dir.normalized;
            direction = new Vector3(dir.x - direction.x, dir.y - direction.y,0);
        }
        this.transform.position += direction * Time.deltaTime * speed;
    }
}
