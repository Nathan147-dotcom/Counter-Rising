using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public int enemyDamage;
    public float damageDelay = 1f;
    private float lastDamageTime;
    
    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            if(Time.time > lastDamageTime + damageDelay){
            other.gameObject.GetComponent<PlayerHealth>().damagePlayer(enemyDamage);
            lastDamageTime = Time.time;
            }
        }
    }
}
