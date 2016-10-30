﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game_Controllers;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    public class Population : MonoBehaviour {
        public int Number = 100;
        public float Hygiene = 0.1f;
        public Dictionary<string, float> Religions = new Dictionary<string, float>();
        public Dictionary<string, float> Cultures = new Dictionary<string, float>();
        public Dictionary<string, float> Education = new Dictionary<string, float>();
        public int[] Age = new int[100];
        //TODO: class Statistic working like a Dictionary<string, float> which values add up to 1.0f (100%)

        public void Start(){
            StartCoroutine ("Grow");
            Religions.Add ("Hinduism", 1.0f);
            Cultures.Add ("Hindu", 1.0f);
        }

        public IEnumerator Grow(){
            while (true)
            {
                var growing = Number/20;
                var space = Controllers.CurrentInfo.GetPopulationLimit() - Number;
                if (space > growing)
                    Number += growing;
                else
                    Number += space;
                yield return new WaitForSeconds(1);
            }
        }
    }
}
