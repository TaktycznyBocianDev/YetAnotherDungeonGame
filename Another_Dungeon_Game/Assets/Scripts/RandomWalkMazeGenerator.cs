using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkMazeGenerator : MonoBehaviour
{
    [Header("How much steps we want in dungeon?")]
    public int stepsMax;
    private int steps = 1; //0 is for start!

    [Header("Empty object that will place gtround tiles")]
    public GameObject walkingDead;

    [Header("Test tileset")]
    public GameObject test;
    public GameObject wall;

    private GameObject first, last;
    private GameObject[] tiles;
    private List<Vector3> visitedPlaces;

    

    private void Start()
    {
        visitedPlaces = new List<Vector3>();
        tiles = new GameObject[stepsMax];
        first = Instantiate(test, walkingDead.transform.position, Quaternion.identity);
        first.GetComponent<SpriteRenderer>().color = Color.green;

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = first;
        }
        
        Walking();


    }



    public void  Walking()
    {
        //Add  current pos as visited
        visitedPlaces.Add(walkingDead.transform.position);

         //Move one step
        int rand = Random.Range(0,4);
        Vector3 whereToGo = new Vector3(0, 0, 0); //Where walker should go?

        switch (rand)
        {
            case 0: //Move up
                whereToGo.x = 0;
                whereToGo.y = 1;
                break;
            case 1: //Move down
                whereToGo.x = 0;
                whereToGo.y = -1;
                break;
            case 2: //Move right
                whereToGo.x = 1;
                whereToGo.y = 0;
                break;
            case 3: //Move left
                whereToGo.x = -1;
                whereToGo.y = 0;
                break;
            default:
                break;
        }

        bool isThatNewPosition = true;
        Vector3 theoriticalPlace = walkingDead.transform.position + whereToGo;

        foreach (var point in visitedPlaces)
        {
            if (point == theoriticalPlace)
            {
                isThatNewPosition = false;
            }
        }
      

        if (isThatNewPosition)
        {
            walkingDead.transform.position += whereToGo;

            //Place new ground
            GameObject nextOne = Instantiate(test, walkingDead.transform.position, Quaternion.identity);

            //It will be a fine addition to my collection!
            tiles[steps] = nextOne;

            //You made another step, I'm so proud
            steps++;

            //Test if you should go next or stop
            if (steps == stepsMax)
            {
                last = nextOne;
                last.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                Walking();

            }
        }
        else
        {
            Walking();
        }

        
    }

    

}
