using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefabs;
    public GameObject upgradePrefabs;
    public int cost;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }


}
