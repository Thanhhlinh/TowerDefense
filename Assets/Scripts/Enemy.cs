using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float healthStart = 100;
    public float healthCurrent;
    public int bonus = 50;
    bool isDead = false;
    

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start()
    {
        speed = startSpeed;
        healthCurrent = healthStart;
    }

    public void TakeDamage(float amount)
    {
        healthCurrent -= amount;
        healthBar.fillAmount = healthCurrent / (float)healthStart;
        if(healthCurrent <= 0&& !isDead)
        {
            Die();
        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * amount;
    }
    void Die()
    {
        isDead = true;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        PlayerState.Money += bonus;
        WaveSpawner.EnemiesAlive--;
        Destroy(effect, 1f);
        Destroy(gameObject);
    }


    
}
