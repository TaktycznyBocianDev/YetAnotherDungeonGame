using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkMazeGenerator : MonoBehaviour
{
    [Header("Player in order to make it active in time")]
    [SerializeField] GameObject player;
    [Header("How much steps we want in dungeon?")]
    [SerializeField] int maximumSteps; 
    [Header("Empty object that will place ground tiles")]
    [SerializeField] GameObject walkingDead;
    [Header("Test tilesets")]
    [SerializeField] GameObject groundTiles;
    [Header("How many enemies we want in our dungeon?")]
    [SerializeField] int maximumEnemiesAmount;   
    [Header("Enemies List")]
    [SerializeField] GameObject[] enemiesList;
    [Header("Merchant, doctor, etc...")]
    [SerializeField] GameObject merchant;
    [SerializeField] GameObject doctor;

    //Values that are used for iteration in recursion 
    private int groundStepsCurrentAmount = 1; //0 is for start!
    private int enemiesCurrentAmount = 0;

    // Constant directions
    private const int UP_DIRECTION = 0;
    private const int DOWN_DIRECTION = 1;
    private const int RIGHT_DIRECTION = 2;
    private const int LEFT_DIRECTION = 3;

    //Constant chances for merchant and doctor spawn - lower means more possible
    private const float MERCHANT_CHANCE = 90;
    private const float DOCTOR_CHANCE = 80;

    //First and last ground tiles:
    private GameObject first, last;
    //List of ground tiles:
    private GameObject[] tilesOfGround;
    //Lists of Vector3 positions - for ground tales and enemy
    private List<Vector3> visitedPostions;
    private List<Vector3> entityPositions;

    private void Start()
    {
        StartingSetUp();      
        WalkToGenerateGround();
        GenerateEnemies(enemiesList);
        int rand = Random.Range(0,101);
        if (rand >= MERCHANT_CHANCE)
        {
            GenerateEntities(merchant);
        }
        if (rand >= DOCTOR_CHANCE)
        {
            GenerateEntities(doctor);
        }
        MakePlayerReady(player);
       
    }

    private void StartingSetUp()
    {
        //Initialization for lists and collections
        visitedPostions = new List<Vector3>();
        entityPositions = new List<Vector3>();
        entityPositions.Add(new Vector3(0, 0, 0));
        tilesOfGround = new GameObject[maximumSteps];

        //Instantiate first ground tile - inital one
        first = Instantiate(groundTiles, walkingDead.transform.position, Quaternion.identity);
        first.GetComponent<SpriteRenderer>().color = Color.green;

        for (int i = 0; i < tilesOfGround.Length; i++)
        {
            tilesOfGround[i] = first;
        }
    }

    public void  WalkToGenerateGround()
    {
        while (groundStepsCurrentAmount < maximumSteps)
        {
            //Add  current pos as visited
            visitedPostions.Add(walkingDead.transform.position);

            //Move one step
            int rand = Random.Range(0, 4);
            Vector3 whereToGo = rand switch
            {
                UP_DIRECTION => new Vector3(0, 1, 0),  // Move up
                DOWN_DIRECTION => new Vector3(0, -1, 0), // Move down
                RIGHT_DIRECTION => new Vector3(1, 0, 0),  // Move right
                LEFT_DIRECTION => new Vector3(-1, 0, 0), // Move left
                _ => new Vector3(0, 0, 0),  // Invalid direction
            };

            bool isThatNewPosition = true;
            Vector3 theoriticalPlace = walkingDead.transform.position + whereToGo;

            for (int i = 0; i < visitedPostions.Count; i++)
            {
                if (visitedPostions[i] == theoriticalPlace)
                {
                    isThatNewPosition = false;
                }
            }

            if (isThatNewPosition)
            {
                walkingDead.transform.position += whereToGo;

                //Place new ground
                GameObject nextOne = Instantiate(groundTiles, walkingDead.transform.position, Quaternion.identity);

                //It will be a fine addition to my collection!
                tilesOfGround[groundStepsCurrentAmount] = nextOne;

                //You made another step, I'm so proud
                groundStepsCurrentAmount++;

                //Test if you should go next or stop
                if (groundStepsCurrentAmount == maximumSteps)
                {
                    visitedPostions.Add(walkingDead.transform.position);
                    last = nextOne;
                    last.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }

    public void GenerateEnemies(GameObject[] enemiesTable)
    {
        bool enemyIsHere = false;

        while (enemiesCurrentAmount < maximumEnemiesAmount)
        {
            enemyIsHere = false;

            //Pick random enemy
            int rand = Random.Range(0, enemiesTable.Length);
            GameObject enemyOfChoise = enemiesTable[rand];

            //Pick place for enemy, left first 3 places without monsters
            int secrand = Random.Range(4, visitedPostions.Count - 1); //First and last are special places
            Vector3 positionForEnemy = visitedPostions[secrand];

            enemyIsHere = entityPositions.Contains(positionForEnemy);

            if (!enemyIsHere)
            {
                //Add this position to list
                entityPositions.Add(positionForEnemy);
                //Place this enemy
                Instantiate(enemyOfChoise, positionForEnemy, Quaternion.identity);

                //We add new enemy!
                enemiesCurrentAmount++;

                if (enemiesCurrentAmount == maximumEnemiesAmount)
                {
                    Debug.Log("Enemies goes Brrrrrrrrrrrrrrrrr");
                }

            }
        }

              
    }

    public void GenerateEntities(GameObject spawnMe)
    {
        bool isThereSomebody = false;

        //Choose random position
        int rand = Random.Range(1, visitedPostions.Count - 1); //First and last are special places
        Vector3 entityPos = visitedPostions[rand];

        //Check if there is enemy already here?
        foreach (Vector3 ePos in entityPositions)
        {
            if (ePos == entityPos)
            {
                isThereSomebody = true;
            }
        }

        if (!isThereSomebody)
        {

            //Add this position to list
            entityPositions.Add(entityPos);
            //Place this enemy
            Instantiate(spawnMe, entityPos, Quaternion.identity);
        }
        else
        {
            GenerateEntities(spawnMe);
        }

    }

    private void MakePlayerReady(GameObject player)
    {
        player.SetActive(true);
        player.GetComponent<PlayerWalking>().SetGroundPosition(visitedPostions);
    }
}
