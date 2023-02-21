using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StatsSet", menuName = "Stats/StatsSet", order = 1)]
public class StatsTemplate : ScriptableObject
{

    [Header("Basic stats")]
    public int strenght;
    public int dexterity;
    public int wisdom;

    //Hp is calculate from strenght + dexterity + 3
    public int GetHealthPoints()
    {
        return strenght + dexterity + 3;
    }

    [Header("For player - take from weapon")]
    public int atack;

    [Header("For player - take from armor")]
    public int armor;

    public Sprite face;

}
