using Dodawanka.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Dodawanka.Saving;
//using UnityEngine.SocialPlatforms.Impl;

namespace Dodawanka.SceneManagement
{
    public class SceneController : MonoBehaviour
    {                      
        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }
        public void QuitGame()
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }        
        public void BackToMainMenu()
        {
            SceneManager.LoadScene(0);
        }  
        public void GameOver()
        {            
            SceneManager.LoadScene(2);            
        }             
    }
}