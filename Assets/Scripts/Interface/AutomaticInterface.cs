﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Game_Controllers;
using Assets.Scripts.Res;
using Assets.Scripts.Utils;
using Assets.Static;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Interface
{
    public class AutomaticInterface : MonoBehaviour
    {
        /// <summary>
        /// The panel which allows performing most of actions in game
        /// </summary>
        public GameObject MainPanel;
        private Rect _mainPanelRect = new Rect(0.9f, 0.3f, 0.1f, 0.7f);
        /// <summary>
        /// Panel with amounts of resources in current city
        /// </summary>
        public GameObject ResourcePanel;
        private Rect _resourcePanelRect = new Rect(0.1f, 0, 0.75f, 0.05f);
        /// <summary>
        /// Panel which shows minimap
        /// </summary>
        public GameObject MiniMapPanel;
        private Rect _miniMapPanelRect = new Rect(0.9f, 0, 0.1f, 0.25f);

        /// <summary>
        /// A button that switches between maps (interfaces)
        /// </summary>
        public GameObject GlobalMapButton;
        private Rect _globalMapButtonRect = new Rect(0.02f, 0.9f, 0.05f, 0.1f);

        public GameObject PeopleAndMoneyPanel;

        private Rect _centerRect = new Rect(0.2f, 0.1f, 0.6f, 0.8f);
        private Rect _exitRect = new Rect(0, 0.95f, 0.05f, 0.05f);
        private Rect _leftHalfRect = new Rect(0, 0, 0.5f, 0.5f);
        private Rect _rightHalfRect = new Rect(0.5f, 0, 0.5f, 0.5f);
        private Rect _fullRect = new Rect(0, 0, 1, 1);
        private Rect _peopleMoneyRect = new Rect(0, 0, 0.1f, 0.1f);

        private Dictionary<string, ExitablePanel> _buttonPanels;

        /// <summary>
        /// List of local map elements (without repeating ones)
        /// </summary>
        private List<GameObject> Local = new List<GameObject>();
        /// <summary>
        /// List of global map elements (without repeating ones)
        /// </summary>
        private List<GameObject> Global = new List<GameObject>();

        public void Start ()
        {
            CreateInterface(); //Creates all the interfaces

            Local.Add(MainPanel);
            Local.Add(ResourcePanel);
            Local.Add(MiniMapPanel);
            Local.Add(PeopleAndMoneyPanel);
            //TODO: Add also the panels activated through the main panel buttons (because they stay open)
            //Or find another way like disabling buttons while some panel is open (maybe interactive button script?)
            //Also we ought to disable the map, but then I think the resources will stop being gathered. Something to think about
            

            SwitchToInterface("Local"); //Starting at local interface (can change) - means that any other interfaces are created but deactivated
        }

       /// <summary>
       /// A method used to create all the interfaces of the game
       /// </summary>
        private void CreateInterface()
        {
            //Local Interface
            CreateLocalInterface();
            //Global Interface
            CreateGlobalInterface();
            //Repeating elements
            CreateRepeatingElements();
        }

        /// <summary>
        /// Activates 'name' interface and deactivates others
        /// </summary>
        /// <param name="name"></param>
        private void SwitchToInterface(string name)
        {
            switch (name)
            {
                case "Local":
                    foreach(var item in Local)
                    {
                        item.SetActive(true);
                    }
                    foreach(var item in Global)
                    {
                        item.SetActive(false);
                    }
                    GlobalMapButton.GetComponent<Button>().onClick.AddListener(() => SwitchToInterface("Global")); //Changing listener of globalmapbutton
                    break;

                case "Global":
                    foreach (var item in Local)
                    {
                        item.SetActive(false);
                    }
                    foreach (var item in Global)
                    {
                        item.SetActive(true);
                    }
                    GlobalMapButton.GetComponent<Button>().onClick.AddListener(() => SwitchToInterface("Local")); //Changing listener of globalmapbutton
                    break;
            }
        }

        /// <summary>
        /// A method used to create elements of the local interface
        /// </summary>
        private void CreateLocalInterface()
        {
            CreateLocalPanels();
            CreateLocalButtons();
        }

        /// <summary>
        /// A method used to create elements of the global interface
        /// </summary>
        private void CreateGlobalInterface()
        {
            CreateGlobalPanels();
            CreateGlobalButtons();
        }

        /// <summary>
        /// A method used to create repeating elements (like globalmapbutton) so they are instantiated no matter on which scene we start
        /// </summary>
        private void CreateRepeatingElements()
        {
            GlobalMapButton = Instantiate(Prefabs.CasualButton);
            Util.SetUIObjectPosition(GlobalMapButton, _globalMapButtonRect, transform);
        }

        //Elements of the local interface

        private void CreateLocalPanels()
        {
            MainPanel = Instantiate(Prefabs.VerticalGroupPanel);
            Util.SetUIObjectPosition(MainPanel, _mainPanelRect, transform);

            MiniMapPanel = Instantiate(Prefabs.Panel);
            Util.SetUIObjectPosition(MiniMapPanel, _miniMapPanelRect, transform);
            
            CreateResourcePanel();

            PeopleAndMoney();
        }

        private void CreateLocalButtons()
        {
            _buttonPanels = new Dictionary<string, ExitablePanel>
            {
                {"Production", CreateExitablePanel(Prefabs.Panel)},
                {"Character", CreateExitablePanel(Prefabs.Panel)},
                {"Diplomacy", CreateExitablePanel(Prefabs.Panel)},
                {"Law", CreateExitablePanel(Prefabs.Panel)},
                {"Science", CreateExitablePanel(Prefabs.Panel)},
                {"Build", CreateExitablePanel(Prefabs.GridGroupPanel)},
                {"Trade", CreateExitablePanel(Prefabs.Panel)}
            };
            foreach (var namesToPanels in _buttonPanels)
            {
                var panel = namesToPanels.Value;
                panel.name = namesToPanels.Key + "Panel";
                Util.SetUIObjectPosition(panel.gameObject, _centerRect, transform);

                var buttonGameObject = Instantiate(Prefabs.CogwheelButton);
                buttonGameObject.name = namesToPanels.Key + "Button";
                buttonGameObject.transform.SetParent(MainPanel.transform);
                AddNotRotatingTextToButton(buttonGameObject, namesToPanels.Key);
                buttonGameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    foreach (var pair in _buttonPanels)
                    {
                        pair.Value.gameObject.SetActive(false);
                    }
                    panel.gameObject.SetActive(true);
                });
                panel.gameObject.SetActive(false);
            }

            FillBuildingsPanel();
            _buttonPanels["Trade"].Content.AddComponent<Trade>();
        }

        //Elements of the global interface

        private void CreateGlobalButtons()
        {

        }

        private void CreateGlobalPanels()
        {

        }

        //Other methods

        private ExitablePanel CreateExitablePanel(GameObject child)
        {
            var panel = Instantiate(Prefabs.ExitablePanel);
            var inner = Instantiate(child);
            Util.SetUIObjectPosition(inner, _fullRect, panel.transform);
            var exitable = panel.GetComponent<ExitablePanel>();
            exitable.Content = inner;
            return exitable;
        }

        private void AddNotRotatingTextToButton(GameObject button, string text)
        {
            var buttonRectTransform = button.GetComponent<RectTransform>();
            var textGO = Instantiate(Prefabs.NotRotatingText);

            textGO.transform.SetParent(button.transform);
            textGO.GetComponent<Text>().text = text;

            var textRectTransform = textGO.GetComponent<RectTransform>();
            textRectTransform.anchorMax = textRectTransform.anchorMin = buttonRectTransform.pivot;
            textRectTransform.offsetMax = buttonRectTransform.sizeDelta;
            textRectTransform.offsetMin = -buttonRectTransform.sizeDelta;
        }

        private void CreateResourcePanel()
        {
            ResourcePanel = Instantiate(Prefabs.HorizontalGroupPanel);
            Util.SetUIObjectPosition(ResourcePanel, _resourcePanelRect, transform);

            foreach (var type in Controllers.ConstantData.ResourceTypes)
            {
                var indicator = Instantiate(Prefabs.ResourceIndicator);
                indicator.transform.SetParent(ResourcePanel.transform);
                indicator.GetComponentInChildren<Image>().sprite = Sprites.ResourceSprite(type);
                indicator.GetComponentInChildren<Text>().text =
                    Controllers.CurrentInfo.Resources[type].Amount.ToString();
                indicator.GetComponent<ResourceData>().Type = type;
            }
        }


        public static Rect CenterOfScreenRect(float width, float height)
        {
            var relativeWidth = width/Screen.width;
            var relativeHeight = height/Screen.height;
            var posx = (Screen.width - width)/(2.0f * Screen.width);
            var posy = (Screen.height - height)/(2.0f * Screen.height);
            return new Rect(posx, posy, relativeWidth, relativeHeight);
        }

        private void FillBuildingsPanel()
        {
            var buildingPanel = _buttonPanels["Build"];
            foreach (var building in Controllers.ConstantData.BuildingCosts.Keys)
            {
                var button = Instantiate(Prefabs.BuildButton);
                button.transform.SetParent(buildingPanel.Content.transform);
                button.GetComponent<BuildButton>().SetUp(building, buildingPanel.gameObject);
            }
        }

        private void PeopleAndMoney()
        {
            PeopleAndMoneyPanel = Instantiate(Prefabs.VerticalGroupPanel);
            Util.SetUIObjectPosition(PeopleAndMoneyPanel, _peopleMoneyRect, transform);

            var people = Instantiate(Prefabs.ResourceIndicator);
            var peopleData = people.GetComponent<ResourceData>();
            peopleData.PopulationRef = Controllers.CurrentInfo.ThePeople;
            people.transform.SetParent(PeopleAndMoneyPanel.transform);
            people.GetComponentInChildren<Image>().sprite = Sprites.SpecialResourceSprite(typeof(Population));
            var rt = (RectTransform) people.transform;
            rt.sizeDelta = new Vector2(0, 0);

            var money = Instantiate(Prefabs.ResourceIndicator);
            var moneyData = money.GetComponent<ResourceData>();
            moneyData.MoneyRef = Controllers.CurrentInfo.MyMoney;
            money.transform.SetParent(PeopleAndMoneyPanel.transform);
            money.GetComponentInChildren<Image>().sprite = Sprites.SpecialResourceSprite(typeof(Money));
            rt = (RectTransform) money.transform;
            rt.sizeDelta = new Vector2(0, 0);
        }
    }
}
