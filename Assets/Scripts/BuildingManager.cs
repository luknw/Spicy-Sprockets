﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BuildingManager : MonoBehaviour
{

    private List<Building> Built;
    private Map mapInstance;
    public Building buildingPrefab;
    public Building tentPrefab;
    private bool active = false;
    private string val;

    public void Build(Vector2 location)
    {
        Building newBuilding = Instantiate(buildingPrefab);
        newBuilding.transform.position = Camera.main.ScreenToWorldPoint(location);
        newBuilding.transform.localScale = new Vector3(20, 20, 20);
        newBuilding.transform.SetParent(mapInstance.transform, true);
        this.active = false;
    }

    public void Build(Vector3 location, string val)
    {
        if (!this.active) return;
        switch (val)
        {
            case "Shit":
                Building newBuilding = Instantiate(buildingPrefab);
                newBuilding.transform.position = Camera.main.ScreenToWorldPoint(location);
                newBuilding.transform.localScale = new Vector3(20, 20, 20);
                newBuilding.transform.SetParent(mapInstance.transform, true);
                break;
            case "Tent":
                Building newTent = Instantiate(tentPrefab);
                newTent.transform.position = Camera.main.ScreenToWorldPoint(location);
                newTent.transform.localScale = new Vector3(20, 20, 20);
                newTent.transform.SetParent(mapInstance.transform, true);
                break;
        }
        this.active = false;
    }

	public Building FollowMouseBuilding(Vector3 location, string val)
	{
		switch (val)
		{
		case "Shit":
			Building newBuilding = Instantiate (buildingPrefab);
			newBuilding.transform.position = Camera.main.ScreenToWorldPoint (location);
			newBuilding.transform.localScale = new Vector3 (20, 20, 20);
			newBuilding.transform.SetParent (mapInstance.transform, true);
			return newBuilding;
		case "Tent":
			Building newTent = Instantiate (tentPrefab);
			newTent.transform.position = Camera.main.ScreenToWorldPoint (location);
			newTent.transform.localScale = new Vector3 (20, 20, 20);
			newTent.transform.SetParent (mapInstance.transform, true);
			return newTent;
		}
		return null;
	}

    public void SetMapInstance(Map MapInstance)
    {
        this.mapInstance = MapInstance;
    }
   
    public void elo()
    {
        Debug.Log("Elo");
    }

    void Start()
    {
		active = false;
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void setActive(bool active)
    {
        this.active = active;
    }

    public bool getActive()
    {
        return active;
    }
    public void setValue(string value)
    {
        val = value;
    }
    public string getValue()
    {
        return val;
    }
}
