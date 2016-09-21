﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using JetBrains.Annotations;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private StrategyManager strategyManager;
    public IntVector2 size;
    
    void Start ()
    {
        Physics.queriesHitTriggers = true;
        strategyManager = gameObject.transform.parent.GetComponent<StrategyManager>();
    }

    void OnMouseDown()
    {
        strategyManager.mapClicked();
    }

    public int objectIndex()
    {
        return gameObject.transform.GetSiblingIndex();
    }
    
}
