
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health; 
    private int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0){
            Destroy(gameObject);
        }
    }

    public void DamageEnemy(int damage){
        currentHealth -= damage;
    }
}
