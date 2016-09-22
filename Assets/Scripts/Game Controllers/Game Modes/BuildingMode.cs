﻿using UnityEngine;
using System.Collections;

public class BuildingMode : GameMode
{
    private StrategyManager strategyManagerInstance;
    private BuildingManager buildingManagerInstance;
    public System.Type toBeBuiltType;
    private Building preview;

    public BuildingMode(StrategyManager strategyManagerInstance, BuildingManager buildingManagerInstance)
    {
        
        this.strategyManagerInstance = strategyManagerInstance;
        this.buildingManagerInstance = buildingManagerInstance;
        this.toBeBuiltType = typeof(ProductionBuilding);
        setPreview();
    }

    public void RightMouseClicked()
    {
        Exit();
    }

    public void LeftMouseClicked()
    {
        buildingManagerInstance.Build(toBeBuiltType, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
        Exit();
    }

    public void Update()
    {
        if(preview!=null) preview.transform.position= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
    }

    public void setPreview()
    {
        this.preview = buildingManagerInstance.preview(toBeBuiltType);
    }

    public void setToBeBuiltType(System.Type buildingType)
    {
        this.toBeBuiltType = buildingType;
    }
    public void Exit()
    {
        BuildingManager.Destroy(preview);
        strategyManagerInstance.enterDefaultMode();
    }

    public void Select(GameObject gameObject)
    {
        setToBeBuiltType(gameObject.GetType());
        setPreview();
    }
}