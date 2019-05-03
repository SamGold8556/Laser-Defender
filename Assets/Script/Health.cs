using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [Header("Player Info")]
    [SerializeField] float health = 100;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 1.0f;
    [SerializeField] float shotCounter;
    [SerializeField] int enemyScoreValue = 150;
    [SerializeField] AudioClip playerDeath;

    [Header("Shooting")]
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyProjectileSpeed = 10f;

    [Header("Shooting Sound Effect")]
    AudioSource enemySoundEffects;
    [SerializeField] AudioClip fireAudioClip;
    [SerializeField] AudioClip deathAudioClip;

    [Header("Particle Effects")]
    [SerializeField] GameObject explosionParticleEffect;

 

    private void Awake()
    {
        enemySoundEffects = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        GameSession gameSession = FindObjectOfType<GameSession>();
	}
	
	// Update is called once per frame
	void Update () {
		CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    public void Fire()
    {
        GameObject laser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, enemyProjectileSpeed);

        enemySoundEffects.PlayOneShot(fireAudioClip, .1f);

        //AudioClip shotAudio = shootingSoundEffects[Random.Range(0, shootingSoundEffects.Length)];
        //otAudio.Play();
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    private void Die()
    {
        // enemySoundEffects.PlayOneShot(deathAudioClip, .7f);
        FindObjectOfType<GameSession>().addToScore(enemyScoreValue);
        AudioSource.PlayClipAtPoint(deathAudioClip, Camera.main.transform.position, 0.1f);
        GameObject explosionInstance = Instantiate(explosionParticleEffect, transform.position, transform.rotation);
        Destroy(explosionInstance, 1f);
        Destroy(gameObject);
    }
}
