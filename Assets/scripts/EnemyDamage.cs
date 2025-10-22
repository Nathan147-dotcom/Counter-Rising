using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public int enemyDamage;
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerHealth>().damagePlayer(enemyDamage);
        }
    }
}
