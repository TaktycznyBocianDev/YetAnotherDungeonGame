using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [Header("How big should our mape size be?")]
    public int mapSize;

    [Header("How many tiles should be ground")]
    public int groundTilesAmount;

    [Header("Test tileset")]
    public GameObject test;
    public GameObject testBlack;

    private MapPoint[] points;

    private void Start()
    {
        points = SetMapPoints(mapSize);
        SetGround(points);
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].status == 0)
            {
                Instantiate(test, points[i].point, Quaternion.identity);
            }
            
        }
        Debug.Log(points.Length.ToString());
        
    }

    public MapPoint[] SetMapPoints(int maxCoordinates)
    {
        MapPoint[] mapPoints = new MapPoint[maxCoordinates * maxCoordinates];
        int x = 0, y = 0;

        for (int i = 0; i < maxCoordinates * maxCoordinates; i++)
        {

            mapPoints[i] = new MapPoint(new Vector3(x, y, 0), Random.Range(0, 7), 0);

            y++;
            if (y == maxCoordinates)
            {
                x++;
                y = 0;
            }
        }

        return mapPoints;

    }

    public void SetGround(MapPoint[] mapPoints)
    {
        mapPoints[Random.Range(0, mapPoints.Length - 1)].status = 1;
        int howMuchIsGroundAlready = 1;

        for (int i = 0; i < groundTilesAmount; i++)
        {

            for (int j = 0; j < mapPoints.Length; j++)
            {

                if (mapPoints[j].status == 1)
                {
                    mapPoints[j].InfectOthers(j, mapPoints, mapSize);
                    howMuchIsGroundAlready++;
                } 

            }

            if (howMuchIsGroundAlready == groundTilesAmount)
            {
                break;
            }
        }
    }


    //int up = start - mapSize;
    //int down = start + mapSize;
    //int left = start - 1;
    //int right = start + 1;

    //if (up < 0 || up > mapPoints.Length)
    //{
    //    up = start;
    //}
    //if (down < 0 || down > mapPoints.Length - 1)
    //{
    //    down = start;
    //}
    //if (left < 0 || left > mapPoints.Length)
    //{
    //    left = start;
    //}
    //if (right < 0 || right > mapPoints.Length)
    //{
    //    right = start;
    //}


    //Debug.Log("Upper one: " + mapPoints[up].value);
    //Debug.Log("Lower one: " + mapPoints[down].value);
    //Debug.Log("Left one: " + mapPoints[left].value);
    //Debug.Log("Right one: " + mapPoints[down].value);
    //Debug.Log("Min: " + Mathf.Min(mapPoints[up].value, mapPoints[down].value, mapPoints[left].value, mapPoints[down].value));

    //MapPoint mapPoint = new MapPoint();
    //MapPoint[] points = { mapPoints[up], mapPoints[down], mapPoints[left], mapPoints[down] };
    //Debug.Log(mapPoint.Iterator(points, Mathf.Min(mapPoints[up].value, mapPoints[down].value, mapPoints[left].value, mapPoints[down].value)).ToString());

}


public class MapPoint
{

    public Vector3 point;
    public int value;
    public int status;

    public void InfectOthers(int indexOfMyself, MapPoint[] mapPoints, int mapSize)
    {
        int randomness = Random.Range(0, 5);
        Debug.Log("Random value of infection: " + randomness);

        if (randomness == 0)
        {
            int up = indexOfMyself - mapSize;
            if (up < 0 || up > mapPoints.Length)
            {
                up = indexOfMyself;
            }

            if (!(mapPoints[up].status == 1))
            {
                mapPoints[up].status = 1;
            }
            else
            {
                InfectOthers(indexOfMyself, mapPoints, mapSize);
            }
            mapPoints[up].ToString();
        }
        else if (randomness == 1)
        {
            int down = indexOfMyself + mapSize;
            if (down < 0 || down > mapPoints.Length-1)
            {
                down = indexOfMyself;
            }

            if (!(mapPoints[down].status == 1))
            {
                mapPoints[down].status = 1;
            }
            else
            {
                InfectOthers(indexOfMyself, mapPoints, mapSize);
            }
            
            mapPoints[down].ToString();
        }
        else if (randomness == 2)
        {
            int right = indexOfMyself + 1;
            if (right < 0 || right > mapPoints.Length-1)
            {
                right = indexOfMyself;
            }

            if (!(mapPoints[right].status == 1))
            {
                mapPoints[right].status = 1;
            }
            else
            {
                InfectOthers(indexOfMyself, mapPoints, mapSize);
            }
            
            mapPoints[right].ToString();
        }
        else if (randomness == 4)
        {
            int left = indexOfMyself - 1;
            if (left < 0 || left > mapPoints.Length)
            {
                left = indexOfMyself;
            }

            if (!(mapPoints[left].status == 1))
            {
                mapPoints[left].status = 1;
            }
            else
            {
                InfectOthers(indexOfMyself, mapPoints, mapSize);
            }
           
            mapPoints[left].ToString();
        }
    }

    public MapPoint(Vector3 point, int value, int status)
    {
        this.point = point;
        this.value = value;
        this.status = status;
    }

    public MapPoint Iterator(MapPoint[] mapPoints, int value)
    {
        MapPoint tmp = new MapPoint(new Vector3(0, 0, 0), 0, 0);

        for (int i = 0; i < mapPoints.Length; i++)
        {
            if (mapPoints[i].value == value)
            {
                tmp = mapPoints[i];
            }
            else
            {

            }
        }

        return tmp;
    }

    public int IGetter(MapPoint[] mapPoints, MapPoint mapPoint)
    {
        int tmp = 0;

        for (int i = 0; i < mapPoints.Length; i++)
        {
            if (point[i].Equals(mapPoint.value))
            {
                tmp = i;
                break;
            }
        }

        return tmp;
    }

    override
    public string ToString()
    {
        return point.x + ", " + point.y + " |Value: " + value + "|Status: " + status;
    }
}

