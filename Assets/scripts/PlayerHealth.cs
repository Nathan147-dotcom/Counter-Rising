using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    public Slider healthBar;
    private int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = health;
        healthBar.maxValue = health;
        healthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0){
            Destroy(gameObject);
        }   
    }

    public void damagePlayer(int damage){
        currentHealth -= damage;
        healthBar.value = currentHealth;
    }
}
