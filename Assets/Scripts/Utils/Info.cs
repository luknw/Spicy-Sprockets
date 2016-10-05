﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Resources;
using System.IO;
using System;
using System.Xml.Linq;
using GameControllers;

public class Info {
	public ResourceType ResourceTypes;
	public Dictionary<string, Resource> Resources = new Dictionary<string, Resource>();
	public Population ThePeople;
	public Money MyMoney = new Money();
	public GameController gameController;
	public List<Building> Buildings;

	public Info(string otherPath = ""){
		gameController = GameObject.Find ("Game Controller").GetComponent<GameController>();
        //Get path to file with resource type
        string path = otherPath == "" ? Directory.GetCurrentDirectory() + @"\Assets\Data\ResourceTypes.xml" : otherPath;
		XDocument doc = XDocument.Load (path);
		//Constructor loads types from xml
		ResourceTypes = new ResourceType (doc.Root.Elements ());
		//data holds information about resource types
		var data = ResourceTypes.Data;
		foreach (var key in data.Keys) {
			//Create resource instances - arguments mean as follows: name, initial amount, quality, info instance
			Resource res = new Resource (key, Int32.Parse(data [key] ["initial"]), Quality.Lux, this); 
			Resources.Add (key, res);
		}
	}
}