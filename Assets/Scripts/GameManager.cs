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
    public GameObject[] terrain;

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

        //load in terrain
        SpawnTerrain(10);

        //load in enemies
        SpawnTargets(enemyNumber);        
	}

    public void SpawnTargets(int numberOfTargets)	
    {
        for (int i = 0; i < numberOfTargets; i++)
		{
            // find a random spot within the game boundary (currently a cube, currently manually set)
            Vector3 spawnPos = RandomVector(-gameBounds, gameBounds);            
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);    // sets rotation to 0,0,0
        }
        enemiesAlive = numberOfTargets;
	}

    public void SpawnTerrain(int numberOfTerrains)
	{
        for (int i = 0; i < numberOfTerrains; i++)
		{
            // get a random terrain from the list
            int terrainIndex = Random.Range(0, terrain.Length);

            // get a random spot on the map
            Vector3 spawnPosition = RandomVector(-gameBounds, gameBounds);

            // get a random rotation
            Quaternion spawnQuaternion = RandomQuaternion();

            // spawn object with that position and rotation
            Instantiate(terrain[terrainIndex], spawnPosition, spawnQuaternion);
		}
	}

    public Vector3 RandomVector(float min, float max)
	{
        float xPos = Random.Range(min, max);
        float yPos = Random.Range(min, max);
        float zPos = Random.Range(min, max);
        return new Vector3(xPos, yPos, zPos);
    }

    public Quaternion RandomQuaternion()
	{
        float xRotation = Random.Range(0, 360);
        float yRotation = Random.Range(0, 360);
        float zRotation = Random.Range(0, 360);
        Vector3 randomRotationEulers = new Vector3(xRotation, yRotation, zRotation);
        Quaternion randomQuaternion = new Quaternion();
        randomQuaternion.eulerAngles = randomRotationEulers;
        return randomQuaternion;
	}
}
