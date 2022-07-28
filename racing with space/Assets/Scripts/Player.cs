using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player")]
    Vector2 playerSpeed;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xPadding = 1f;
    [SerializeField] float yPadding = 0.3f;
    [SerializeField] int health = 500;
    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume;
    [Header("Lazer")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed;
    [SerializeField] float fireRate;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0,1)] float shootSoundVolume;

    Coroutine fireLoop;
    float xMin, yMin, xMax, yMax;
    void Start()
    {
        SetUpMoveBoundaries();      
    }
    void Update()
    {
        Move();
        Fire();
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireLoop = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireLoop);
        }
    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                   laserPrefab,
                   transform.position,
                   Quaternion.identity);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, shootSoundVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(fireRate);
        }

    }
    /*void Move()
    {
        Vector3 delta = playerSpeed;
        transform.position += delta;
    }
    void OnMove()
    {
        playerSpeed = new Vector2(1f, 1f) * moveSpeed * Time.deltaTime;
    }
   */ private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal")* Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
       
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Damages damages = other.gameObject.GetComponent<Damages>();
        if (!damages) { return; }
        HitProcess(damages);
    }
    private void HitProcess(Damages damages)
    {
        health -= damages.GetDamage();
        damages.Hit();
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, deathSoundVolume);
            Destroy(gameObject,0.1f);
            FindObjectOfType<Level>().LoadGameOver();

        }
    }
    public int GetHealth()
    {
        return health;
    }
}
