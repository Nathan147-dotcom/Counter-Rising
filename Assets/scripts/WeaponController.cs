using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour

{

    public Transform firePoint;
    public GameObject ammoType;
    
    public float shotSpeed;
    public float shotCounter;
    public float fireRate;
    
    private Animator playerAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gunRotate();

        if(Input.GetMouseButton(0)){
            shotCounter -= Time.deltaTime;
            if (shotCounter<=0){
                shotCounter = fireRate;
                Shoot();
            }
        }
        else{
            shotCounter = 0;
        }
    }

    void gunRotate(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - gunPos.x;
        mousePos.y = mousePos.y - gunPos.y;
        float gunAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x){
            transform.rotation=Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
            }
        else{
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));        
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
