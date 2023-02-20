using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{

    private List<Vector3> groundTilesPositions;
    private bool moveAround = true;

    private void Update()
    {
        if (moveAround)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position += Vector3.up;
                CheckIsPlayerIsOnGround();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position += Vector3.down;
                CheckIsPlayerIsOnGround();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position += Vector3.right;
                CheckIsPlayerIsOnGround();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position += Vector3.left;
                CheckIsPlayerIsOnGround();
            }
        }

    }

    public void EnableWalking(bool enableWalking)
    {
        moveAround = enableWalking;
    }

    public void SetGroundPosition(List<Vector3> groundPositions)
    {
        groundTilesPositions = groundPositions;
    }

    private void CheckIsPlayerIsOnGround()
    {
        if (!(groundTilesPositions.Contains(transform.position)))
        {
            Debug.Log("Nope, you can not stand here!");
            transform.position = Vector3.zero;
        }
    }

}
