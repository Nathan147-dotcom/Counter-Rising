using UnityEngine;

public class meteor : MonoBehaviour
{
    public int enemyDamage;
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            
            other.gameObject.GetComponent<PlayerHealth>().damagePlayer(enemyDamage);
            Destroy(gameObject);
        }
        else if(other.CompareTag("Ground")){
            Destroy(gameObject);
        }
    }
}
