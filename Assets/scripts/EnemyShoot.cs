using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    public Transform firePoint;
    public GameObject ammoType;
    private float timer;
    private GameObject player;
    AudioManager audioManager;

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
                audioManager.PlaySFX(audioManager.lazerShoot);
            }
        }
        
    }

    void Shoot()
    {
        Instantiate(ammoType, firePoint.position, Quaternion.identity);
    }

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


}
