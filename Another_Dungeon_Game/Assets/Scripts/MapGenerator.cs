using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [Header("How big should our mape size be?")]
    public int mapSize;

    [Header("Test tileset")]
    public GameObject test;

    private MapPoint[] points;

    private void Start()
    {
        points = SetMapPoints(mapSize);
        for (int i = 0; i < points.Length; i++)
        {
            Instantiate(test, points[i].point, Quaternion.identity);
        }
        Debug.Log(points.Length.ToString());
    }

    public MapPoint[] SetMapPoints(int maxCoordinates)
    {
        MapPoint[] mapPoints = new MapPoint[maxCoordinates*maxCoordinates];
        int x = 0, y = 0;

        for (int i = 0; i < maxCoordinates*maxCoordinates; i++)
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

}

public class MapPoint
{

    public Vector3 point;
    public int value;
    public int status;

    public MapPoint(Vector3 point, int value, int status)
    {
        this.point = point;
        this.value = value;
        this.status = status;
    }

}

