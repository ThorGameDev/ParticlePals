using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    public GameObject player;
    public float lerpSpeed;
    // Update is called once per frame
    void FixedUpdate()
    {
        float X = Mathf.Lerp(this.transform.position.x,player.transform.position.x,Time.fixedDeltaTime * lerpSpeed);
        float Y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, Time.fixedDeltaTime * lerpSpeed);
        this.transform.position = new Vector3(X, Y, -10);
    }
}
