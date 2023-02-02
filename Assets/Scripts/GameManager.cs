using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int playerScore;
    private float gameTime = 61;
    public GameObject playerObj;
    public int enemyNumber;
    public GameObject enemyPrefab;
    private float gameBounds = 49.0f; //test value only
    public int enemiesAlive;
    public GameObject[] terrain;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI enemiesText;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        timeText.text = "Time: " + (int)gameTime;
        enemiesText.text = "Intruders: " + enemiesAlive;
    }

    public void ShowMainMenu()
	{

	}

    public void StartGame()
	{
        // reset player position
        playerObj.transform.position = new Vector3(0, 0, 0);
        playerObj.transform.eulerAngles = new Vector3(0, 0, 0);

        // load in terrain
        SpawnTerrain(10);

        // load in enemies
        SpawnTargets(enemyNumber);

        // reset score
        playerScore = 0;
        UpdateScore(0);

        // reset player timer
        gameTime = 60;
        timeText.text = "Time: " + (int)gameTime;

        // reset enemy number
        enemiesText.text = "Intruders: " + enemiesAlive;
	}

    //spawns a number of enemy targets
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
            // get a random piece of terrain from the list
            int terrainIndex = Random.Range(0, terrain.Length);
            bool validTerrainPlacement = false;

            while(!validTerrainPlacement)
			{
                // get a random spot on the map
                Vector3 spawnPosition = RandomVector(-gameBounds, gameBounds);

                // get the halfExtents of each dimension
                float xHalf = terrain[terrainIndex].transform.localScale.x / 2;
                float yHalf = terrain[terrainIndex].transform.localScale.y / 2;
                float zHalf = terrain[terrainIndex].transform.localScale.z / 2;
                Vector3 terrainHalfExtent = new Vector3(xHalf, yHalf, zHalf);

                // get a random rotation
                Quaternion spawnQuaternion = RandomQuaternion();

                // if the checkbox function returns false i.e. no overlap in the box, then spawn and move to next terrain to be spawned
                if(!Physics.CheckBox(spawnPosition, terrainHalfExtent, spawnQuaternion))
				{
                    validTerrainPlacement = true;
                    // spawn object with that position and rotation
                    Instantiate(terrain[terrainIndex], spawnPosition, spawnQuaternion);
                }
            }
		}
	}

    // provides random vector within argument-defined ranges
    public Vector3 RandomVector(float min, float max)
	{
        float xPos = Random.Range(min, max);
        float yPos = Random.Range(min, max);
        float zPos = Random.Range(min, max);
        return new Vector3(xPos, yPos, zPos);
    }
    // provides random Quaternion in full range of rotation
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

    public void UpdateScore(int scoreToAdd)
	{
        playerScore += scoreToAdd;
        scoreText.text = "Score: " + playerScore;
	}
}
