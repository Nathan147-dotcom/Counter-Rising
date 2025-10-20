using UnityEngine;

public class FixedEnemyJumper : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float detectRadiusRange;
    public float jumpCooldown;
    public float jumpImpulse;
    float lastJumpTime;

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(player.transform.position, transform.position);
        Vector3 scale = transform.localScale;

        if(dist < detectRadiusRange){
        if(player.transform.position.x > transform.position.x){
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else{
            transform.Translate(speed * Time.deltaTime * -1, 0, 0);
        }

        transform.localScale = scale;

        if (Time.time > lastJumpTime + jumpCooldown) {

                lastJumpTime = Time.time;

                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        }
        }
    }
}
