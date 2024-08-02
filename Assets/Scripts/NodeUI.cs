using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public Text costUpgrade;
    public Text costSell;
    public Button upgrade;
    public GameObject ui;

    public void SetTarget(Node _target)
    {
        target = _target;
        if (!target.isUpgrade)
        {
            costUpgrade.text = target.turretBlueprint.upgradeCost.ToString() + "$";
            upgrade.interactable = true;
        }
        else
        {
            costUpgrade.text = "Done";
            upgrade.interactable = false;
        }
        
        costSell.text =target.turretBlueprint.GetSellAmount().ToString() + "$";
        transform.position = target.GetBuildPosition();
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }

}
