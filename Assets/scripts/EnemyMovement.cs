using UnityEngine;

public class enemies : MonoBehaviour
{
    public GameObject player;
    public bool flip;
    public float speed;
    public float detectRadiusRange;

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if(dist < detectRadiusRange){
        if(player.transform.position.x > transform.position.x){
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else{
            scale.x = Mathf.Abs(scale.x) * (flip ? -1: 1);
            transform.Translate(speed * Time.deltaTime * -1, 0, 0);
        }

        transform.localScale = scale;
        }
    }
}
