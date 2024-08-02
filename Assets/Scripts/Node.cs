using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverMouse;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgrade = false;
    private Renderer render;
    private Color startColor;

    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.Instance;
        render = GetComponent<Renderer>();
        startColor = render.material.color;
    }


    


    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }


        if(!buildManager.CanBuild)
        {
            return;
        }


        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }


    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerState.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerState.Money -= blueprint.cost;
        GameObject _turret = Instantiate(blueprint.prefabs, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;
        GameObject effect = Instantiate(buildManager.buildEffect,GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2f);
        Debug.Log("Turret build success! Money left: " + PlayerState.Money);
    }

    public void UpgradeTurret()
    {
        if (PlayerState.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerState.Money -= turretBlueprint.upgradeCost;
        Destroy(turret);
        GameObject _turret = Instantiate(turretBlueprint.upgradePrefabs, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2f);

        isUpgrade = true;
        Debug.Log("Turret upgrade success! Money left: " + PlayerState.Money);
    }

    public void SellTurret()
    {
        PlayerState.Money += turretBlueprint.GetSellAmount();
        Destroy(turret);
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2f);
        turretBlueprint = null;
        isUpgrade = false;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }


        if (!buildManager.CanBuild)
        {
            return;
        }


        if (buildManager.HasMoney)
        {
            render.material.color = hoverMouse;
        }
        else
        {
            render.material.color = notEnoughMoneyColor;
        }

        
    }

    private void OnMouseExit()
    {
        render.material.color = startColor;
    }
}
