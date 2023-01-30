using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float projectileSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //move in the direction it's supposed to go
        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        //check for collision with other objects - wall, enemies, player perhaps
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Terrain"))
		{
            Debug.Log("Contact");
            Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("Contact");
            Destroy(gameObject);
        }
    }
}
