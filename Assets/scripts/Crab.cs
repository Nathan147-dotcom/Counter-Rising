using System;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public GameObject player;
    public bool flip;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale();

        if(player.transform.position.x>transform.position.x){
            scale.x = Mathf.Abs(scale.x) * -1 * flip(flip? -1: 1); 
            transform.Translate(speed *  Time.deltaTime, 0, 0);
        }
        else{
            scale.x = Mathf.Abs(scale.x)* flip(flip? -1: 1);
            transform.Translate(speed *  Time.deltaTime, 0, 0);
        }
            

        transform.localScale = scale;
    }
}
