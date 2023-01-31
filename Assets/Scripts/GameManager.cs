using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerObj;
    public int enemyNumber;
    public GameObject enemyPrefab;
    private float gameBounds = 49.0f; //test value only
    public int enemiesAlive;
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainMenu()
	{

	}

    public void StartGame()
	{
        // reset player position
        playerObj.transform.position = new Vector3(0, 0, 0);
        playerObj.transform.eulerAngles = new Vector3(0, 0, 0);

        //load in enemies
        SpawnTargets(enemyNumber);        
	}

    public void SpawnTargets(int numberOfTargets)	
    {
        for (int i = 0; i < numberOfTargets; i++)
		{
            // find a random spot within the game boundary (currently a cube, currently manually set)
            float xPos = Random.Range(-gameBounds, gameBounds);
            float yPos = Random.Range(-gameBounds, gameBounds);
            float zPos = Random.Range(-gameBounds, gameBounds);
            Vector3 spawnPos = new Vector3(xPos, yPos, zPos);

            
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);    // sets rotation to 0,0,0
        }
        enemiesAlive = numberOfTargets;
	}

    
}
