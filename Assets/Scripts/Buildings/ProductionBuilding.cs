﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ProductionBuilding : Building
{
    private ResourceType.Type processedResource;
    private int time;
    private int resQuantity;
    private int processTime = 300;
    private List<Resource> playerResources;
    Predicate<Resource> ironFinder = (Resource res) => { return res.name == "Iron"; };

    void Start(ResourceType.Type res)
    {
        myColor = new Color(0.5f, 0.2f, 0.25f);
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = mySprite;
        renderer.color = myColor;
        renderer.sortingOrder = 1;
        processedResource = res;
        time = 0;
    }

    void Update()
    {
        if (resQuantity != 0)
        {
            time++;
            if (time == processTime)
            {
                time = 0;
                Process();
            }
        }
    }

    private void Process()
    {
        Resource product;

        resQuantity--;

        switch (processedResource)
        {
            case ResourceType.Type.Coal:
                playerResources[playerResources.FindIndex(ironFinder)]++;
                break;
        } 
    }
}
