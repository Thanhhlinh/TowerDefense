using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }
    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;


    public GameObject buildEffect;
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerState.Money >= turretToBuild.cost; } }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        Instance = this;
    }
   
    void Update()
    {
        
    }

    

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;

        nodeUI.SetTarget(node);

    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
