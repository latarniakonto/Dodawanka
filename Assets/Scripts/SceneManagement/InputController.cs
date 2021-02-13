using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dodawanka.SceneManagement
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private InputField inputField = null;
        [SerializeField] private Text savingText;
        [SerializeField] private float fadeOutTime = 1f;


        private string playerName = "";
        private void Start()
        {
            inputField.enabled = true;
        }

        public void StoreName()
        {
            playerName = inputField.GetComponentsInChildren<Text>()[1].text;
            if (!Stored()) return;                               
            if(playerName.Length > 10)
            {
                TooLong();
                playerName = null;
                return;
            }
            print(playerName == "");
            inputField.enabled = false;
            savingText.text = "Saving " + playerName;
            StartCoroutine(Fadeout(fadeOutTime, true));            
        }
        private void TooLong()
        {
            savingText.text = "Too long player name.";
            StartCoroutine(Fadeout(fadeOutTime, false));    
        }
        public void FadeInImediately()
        {
            savingText.text = "";
            Color color = savingText.color;
            color.a = 1;
            savingText.color = color;            
        }
        public IEnumerator Fadeout(float time, bool succes)
        {
            Color color;
            while (savingText.color.a > 0)
            {
                color = savingText.color;
                color.a -= (Time.deltaTime / time);
                savingText.color = color;
                yield return null;
            }
            if(!succes)
            {
                FadeInImediately();
            }
        }
        public bool Stored()
        {
            return playerName != "";
        }
        public string Name()
        {
            return playerName;         
        }
    }
}