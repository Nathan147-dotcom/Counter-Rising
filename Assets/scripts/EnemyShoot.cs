using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    public Transform firePoint;
    public GameObject ammoType;
    private float timer;
    private GameObject player;
    

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);
        timer += Time.deltaTime;
        if(dist < 40){
            if(timer > 2){
                timer = 0;
                timer += Time.deltaTime;
                Shoot();
            }
        }
        
    }

    void Shoot()
    {
        Instantiate(ammoType, firePoint.position, Quaternion.identity);
    }


}
