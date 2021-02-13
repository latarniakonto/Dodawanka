using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dodawanka.SceneManagement;
using System.IO;


namespace Dodawanka.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        public void SavePlayer()
        {           
            if (!GetComponent<InputController>().Stored()) return; 
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void LoadPlayers()
        {            
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }        
    }
}