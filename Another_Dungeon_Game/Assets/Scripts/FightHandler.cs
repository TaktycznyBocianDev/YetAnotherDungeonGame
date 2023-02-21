using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightHandler : MonoBehaviour
{

    private GameObject player, enemy;

    private StatsTemplate playerStats, enemyStats;

    [Header("UI images where we display player and enemy")]
    [SerializeField] Image playerImage, enenmyImage;

    public void SetUpForFight(GameObject player, GameObject enemy)
    {
        this.player = player;
        this.enemy = enemy;

        this.playerStats = player.GetComponent<StatsHolder>().stats;
        this.enemyStats = enemy.GetComponent<StatsHolder>().stats;

        playerImage.sprite = playerStats.face;
        enenmyImage.sprite = enemyStats.face;

    }


}
