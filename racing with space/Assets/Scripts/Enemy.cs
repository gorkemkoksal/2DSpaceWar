using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] int enemyScore = 149;
    [SerializeField] int health = 300;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject laserPreFab;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathVolume;
    [Header("Laser")]
    [SerializeField] float shootCounter;
    [SerializeField] float minTimeBtwShoots;
    [SerializeField] float maxTimeBtwShoots;
    [SerializeField] float laserSpeed;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserSoundVolume;
    
    private void Start()
    {
        shootCounter = Random.Range(minTimeBtwShoots, maxTimeBtwShoots);
    }
    private void Update()
    {
        CountDownShoots();
    }

    private void CountDownShoots()
    {
        shootCounter -= Time.deltaTime;
        if (shootCounter <= 0f)
        {
            Fire();
            shootCounter = Random.Range(minTimeBtwShoots, maxTimeBtwShoots);
        }
    }
    private void Fire()
    {
       AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
       var laser=Instantiate(laserPreFab,
       transform.position,
       Quaternion.identity);
       laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
                 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damages damages = other.gameObject.GetComponent<Damages>();
        HitProcess(damages);
    }

    private void HitProcess(Damages damages)
    {
        health -= damages.GetDamage();
        Debug.Log(health);
        damages.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(enemyScore);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
    }
}
