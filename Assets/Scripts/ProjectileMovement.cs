using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float projectileSpeed;
    private GameManager gameManager;
    private GameObject projectileSource;
    private AudioSource projectileAudio;
    public AudioClip collisionSound;

    // Start is called before the first frame update
    void Start()
    {
        projectileAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        projectileSource = GameObject.Find("Player");
        projectileSpeed += projectileSource.GetComponent<Rigidbody>().velocity.magnitude;   // this works but increases the speed of the projectile even if you're going backwards.
        transform.Rotate(new Vector3(90, 0, 0));    // rotate capsule to be more projectile-like
    }

    // Update is called once per frame
    void Update()
    {
        //move in the direction it's supposed to go
        transform.Translate(Vector3.up * Time.deltaTime * projectileSpeed);
    }

	private void OnCollisionEnter(Collision collision)
	{
        Debug.Log("Contact");
        projectileAudio.PlayOneShot(collisionSound, 1.0f);
	}
	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Terrain"))
        { 
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
		{
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameManager.enemiesAlive--;
            gameManager.UpdateScore(1);
		}
    }
}
