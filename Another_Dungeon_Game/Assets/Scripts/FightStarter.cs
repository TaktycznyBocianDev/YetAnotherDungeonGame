using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStarter : MonoBehaviour
{
    [Header("Canvas to activate")]
    [SerializeField] GameObject canvasFight;

    [Header("Background to deactivate")]
    [SerializeField] GameObject backGround;

    [Header("Object that is responsible for fight mechanic")]
    [SerializeField] GameObject fightEngine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvasFight.SetActive(true);
        //backGround.SetActive(false);
        gameObject.GetComponent<PlayerWalking>().EnableWalking(false);
        fightEngine.GetComponent<FightHandler>().SetUpForFight(gameObject, collision.gameObject);

    }

}
