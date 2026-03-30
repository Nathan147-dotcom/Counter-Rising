using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour

{

    public Transform firePoint;
    public GameObject ammoType;
    
    public float shotSpeed;
    public float shotCounter;
    public float fireRate;
    
    private Animator playerAnim;

    AudioManager audioManager;

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
            HandleMouseInput();
        #endif
        
    }

    void HandleMouseInput(){
        RotateTowardScreenPoint(Input.mousePosition);

        if(Input.GetMouseButton(0)){
            shotCounter -= Time.deltaTime;
            if (shotCounter<=0){
                shotCounter = fireRate;
                Shoot();
                audioManager.PlaySFX(audioManager.shoot);
            }
        }
        else{
            shotCounter = 0;
        }
    }

    public void HandleTouchInput(Touch touch){
        RotateTowardScreenPoint(touch.position);
        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved){
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0){
                shotCounter = fireRate;
                Shoot();
                audioManager.PlaySFX(audioManager.shoot);
            }
        }
        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled){
                shotCounter = 0;
            }
    }

    void RotateTowardScreenPoint(Vector2 screenPoint){
        Vector2 originalPoint = screenPoint;
        Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
        screenPoint.x = screenPoint.x - gunPos.x;
        screenPoint.y = screenPoint.y - gunPos.y;
        float gunAngle = Mathf.Atan2(screenPoint.y, screenPoint.x) * Mathf.Rad2Deg;
        if(Camera.main.ScreenToWorldPoint(originalPoint).x < transform.position.x){
            transform.rotation = Quaternion.Euler(180f, 0f, -gunAngle);
            }
        else{
            transform.rotation = Quaternion.Euler(0f, 0f, gunAngle);   
            }
    }
    //Prefab
    void Shoot(){

        
        GameObject shot = Instantiate(ammoType, firePoint.position, firePoint.rotation);
        Rigidbody2D shotRb = shot.GetComponent<Rigidbody2D>();
        shotRb.AddForce(firePoint.right * shotSpeed * playerDirection(), ForceMode2D.Impulse);
        Destroy(shot.gameObject, 1f);
    }
    int playerDirection(){

            if(transform.root.localScale.x<0f){
                return -1;
            }
            else{
                return +1;
            }
        }
}
