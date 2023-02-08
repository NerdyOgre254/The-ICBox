using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    private int playerScore;
    private float gameTime = 61;
    public GameObject playerObj;
    public int enemyNumber;
    public GameObject enemyPrefab;
    private float gameBounds = 49.0f; //test value only
    public int enemiesAlive;
    public GameObject[] terrain;

    //main menu UI assets
    public TextMeshProUGUI titleText;
    public GameObject startGameButton;
    public GameObject gameControls;

    //in game UI assets
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI crosshair;
    public TextMeshProUGUI gameOverText;
    public GameObject restartGameButton;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Count down game time if in game. Manage game text for enemies and time.
        if (!isGameOver)
		{
            gameTime -= Time.deltaTime;
            if (gameTime <= 0)
            {
                GameOver();
            }
            timeText.text = "Time: " + (int)gameTime;
            enemiesText.text = "Intruders: " + enemiesAlive;
        }
        
    }

    public void ShowMainMenu()
	{
        isGameOver = true;
        //display title
        titleText.gameObject.SetActive(true);

        //display buttons for New Game
        startGameButton.gameObject.SetActive(true);

        //display controls
        gameControls.gameObject.SetActive(true);
	}

    public void StartGame()
	{
        // Lock cursor in place while game is running
        Cursor.lockState = CursorLockMode.Locked;

        // Deactivate Main Menu UI elements
        titleText.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(false);
        gameControls.gameObject.SetActive(false);

        // Deactivate Game Over UI elements
        gameOverText.gameObject.SetActive(false);
        restartGameButton.gameObject.SetActive(false);

        // Activate Game UI elements
        scoreText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        enemiesText.gameObject.SetActive(true);
        crosshair.gameObject.SetActive(true);

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
        gameTime = 61;
        timeText.text = "Time: " + (int)gameTime;

        // reset enemy number
        enemiesText.text = "Intruders: " + enemiesAlive;

        // Set isGameOver Flag to false to allow mouselook
        isGameOver = false;
	}

    //spawns a number of enemy targets
    public void SpawnTargets(int numberOfTargets)	
    {
        for (int i = 0; i < numberOfTargets; i++)
		{
            bool validEnemyPlacement = false;
            while (!validEnemyPlacement)
			{
                // find a random spot within the game boundary (currently a cube, currently manually set)
                Vector3 spawnPos = RandomVector(-gameBounds, gameBounds);
                float targetRadius = transform.localScale.x / 2;

                if(!Physics.CheckSphere(spawnPos, targetRadius))
				{
                    validEnemyPlacement = true;
                    Instantiate(enemyPrefab, spawnPos, Quaternion.identity);    // sets rotation to 0,0,0
                }
            }
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

    public void GameOver()
	{
        Cursor.lockState = CursorLockMode.None;
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        restartGameButton.gameObject.SetActive(true);
	}

    void PlayStandardGame()
	{
        //start game

        //if there are no more targets, spawn more

        //if time runs out, game over
	}

    void PlayEnduranceGame()
	{
        //start game

        //on enemy destruction, increase time by a certain amount

        //if there are no more targets, spawn more

        //if time runs out, game over
	}
}
