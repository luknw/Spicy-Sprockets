﻿using System;
using System.Collections;
using Assets.Scripts.Buildings;
using Assets.Scripts.Utils;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Assets.Scripts.Game_Controllers.Game_Modes
{
    /// <summary>
    /// Modifies behaviour of mouse clicks to enable creating buildings
    /// </summary>
    public class BuildingMode : IGameMode
    {
        /// <summary>
        /// Type of building that player wishes to create
        /// </summary>
        public System.Type ToBeBuiltType;
        /// <summary>
        /// Building sprite hovering after mouse cursor
        /// </summary>
        private Building _preview;
<<<<<<< HEAD
        private int time;
        private bool canBeBuilt;
=======
>>>>>>> origin/develop

        /// <summary>
        /// Initialize the mode to create a building of type buildingType
        /// </summary>
        /// <param name="buildingType">Type of building player wishes to create</param>
        public BuildingMode(Type buildingType)
        {
            this.ToBeBuiltType = buildingType;
            //Create a sprite of given type that hovers after cursor
            SetPreview();
        }

        /// <summary>
        /// Defines game behaviour on right mouse click
        /// </summary>
        public void RightMouseClicked()
        {
            Exit();
        }
        
        private bool Overlaps(BoxCollider2D what, int layer)
        {
            LayerMask a = 1 << layer;
            return what.IsTouchingLayers(a);
        }

        //feedback todo
        private void CantBuildShit (string why, Vector2 where)
        {
            switch (why)
            {
                case "overlap":
                    break;
                default:
                    return;
                
            }

        }

<<<<<<< HEAD

        
        public void LeftMouseClicked()
        {
            
            if (canBeBuilt)
            {
                Controllers.CurrentBuildingManager.Build(ToBeBuiltType, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                Exit();
            }
            else
            {
                CantBuildShit("overlap", new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            }
=======
        /// <summary>
        /// Defines game behaviour on left mouse click
        /// </summary>
        public void LeftMouseClicked()
        {
            // get mouse position and create a building of desired type placed by the cursor
            Controllers.CurrentBuildingManager.Build(ToBeBuiltType, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
            // return to default mode
            Exit();
>>>>>>> origin/develop
        }

        /// <summary>
        /// Needs to be called manually since it's not a MonoBehaviour
        /// </summary>
        public void Update()
        {
            // Check if preview exists
            if (_preview == null) return;
<<<<<<< HEAD
            
            
=======
            // if it does, place it by the mouse cursor
>>>>>>> origin/develop
            var previewPosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            previewPosition.z = 0;
            _preview.transform.position = previewPosition;


            var collider = _preview.gameObject.GetComponent<BoxCollider2D>();
            if (Overlaps(collider,8))//||InsufficientResources(_preview))
            {
                canBeBuilt = false;
                
            } else
            {
                canBeBuilt = true;
                
            }
            if (canBeBuilt)
            {
                _preview.MyRenderer.color = Color.green;
            } else _preview.MyRenderer.color = Color.red;


            if (Input.GetMouseButton(0)&&_preview.GetComponent<BoxCollider2D>().IsTouchingLayers(1))
            {
                LeftMouseClicked();
            }
            if (Input.GetMouseButton(1))
            {
                RightMouseClicked();
            }
        }

        /// <summary>
        /// Create a preview which has same sprite as desired building but is not its instance so it does not start coroutines etc.
        /// </summary>
        public void SetPreview()
        {
            // create the game object which obeys BuildingPreview script and renders a sprite
            var gameObject = new GameObject("Preview", typeof(SpriteRenderer), typeof(BuildingPreview));
            // Actual preview is the script component of the game object
            _preview = (Building) gameObject.GetComponent(typeof(BuildingPreview));
            // set sprite to the same as desired type
            _preview.SetSprite(ToBeBuiltType);
<<<<<<< HEAD
            
            var buildingPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            buildingPosition.z = 0;
            _preview.transform.position = Camera.main.ScreenToWorldPoint(buildingPosition);
            _preview.transform.SetParent(_gameController.GetCurrentCity().MapInstance.transform, true);
            Collider.addCollider(gameObject, new Vector2(2, 1), Camera.main.ScreenToWorldPoint(buildingPosition), _gameController.GetCurrentCity().MapInstance.transform);
            Collider.addFakeRigidBody(gameObject);
=======
            // place it by the mouse cursor
            var buildingPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            buildingPosition.z = 0;
            _preview.transform.position = Camera.main.ScreenToWorldPoint(buildingPosition);
            // make the preview a child of map
            _preview.transform.SetParent(Controllers.CurrentCityController.MapInstance.transform, true);
            // change size of the sprite
>>>>>>> origin/develop
            Util.Rescale(_preview.GetComponent<SpriteRenderer>(), 60, 60);
        }

        /// <summary>
        /// Go back to default mode
        /// </summary>
        public void Exit()
        {
            // no need for preview anymore
            Object.Destroy(_preview.gameObject);
            Controllers.GameController.EnterDefaultMode();
        }

        /// <summary>
        /// A method that chooses which type should be built and creates a preview
        /// </summary>
        /// <param name="gameObject">Object which type is to be copied</param>
        public void Select(GameObject gameObject)
        {
            ToBeBuiltType = gameObject.GetType();
            SetPreview();
        }
    }
}
