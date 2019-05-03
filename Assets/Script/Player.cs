using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int playerHealth = 200;
    AudioSource playerDeathSFX;
    [SerializeField] AudioClip playerDeathAudioClip;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Level sceneManager;
    Coroutine firingCoroutine;

    float xMin;
    float xMax;

    float yMin;
    float yMax;

    // Use this for initialization
    private void Awake()
    {
        playerDeathSFX = GetComponent<AudioSource>();
    }

    void Start () {

        SetUpMoveBoundaries();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        playerHealth -= damageDealer.getDamage();
        damageDealer.Hit();

        if (playerHealth <= 0)
        {
            AudioSource.PlayClipAtPoint(playerDeathAudioClip, new Vector3(transform.position.x, transform.position.y, 0));
            //playerDeathSFX.Play();
            Destroy(gameObject);
            FindObjectOfType<Level>().LoadGameOver();
                    }
    }

    private void SetUpMoveBoundaries()
    {
       
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    // Update is called once per frame
    void Update () {

        Move();
        Fire();
		
	}

    IEnumerator FireCountinously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        } 
    }

    private void Fire()
    {
       
        if (Input.GetButtonDown("Fire1"))
        {
           firingCoroutine = StartCoroutine(FireCountinously());
        } 
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    public void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPOS = Mathf.Clamp(transform.position.x + deltaX, xMin + padding, xMax - padding);
        var newYPOS = Mathf.Clamp(transform.position.y + deltaY, yMin + padding, yMax - padding);

        transform.position = new Vector2(newXPOS, newYPOS);
    }
}
