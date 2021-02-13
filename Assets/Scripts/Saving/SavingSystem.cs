using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Dodawanka.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

namespace Dodawanka.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        [SerializeField] private EntriesController entriesController = null;                       
        public void Save(string saveFile)
        {            

            string path = GetPathFromSaveFile(saveFile);
            using (FileStream stream = File.Open(path, FileMode.Append))
            {
                string playerName = GetPlayerName();
                int playerScore = GetPlayerScore();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, playerName);
                formatter.Serialize(stream, playerScore);
            }
        }


        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Dictionary<string, int> scoreTable = new Dictionary<string, int>();
            using (FileStream stream = File.Open(path, FileMode.Open))
            {            
                while(stream.Position != stream.Length)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    string playerName = (string)formatter.Deserialize(stream);
                    int playerScore = (int)formatter.Deserialize(stream);
                    if (scoreTable.ContainsKey(playerName) && scoreTable[playerName] > playerScore) continue;
                    scoreTable[playerName] = playerScore;
                }
            }
            entriesController.CreateEntries(scoreTable);

        }
        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
        private int GetPlayerScore()
        {
            return FindObjectOfType<Results>().Score();
        }

        private string GetPlayerName()
        {
            //Może pobierać NULLA, zależy od tego jak się będzie wykonywać. Potencjalnie może znajdować się tu bug.
            return FindObjectOfType<InputController>().Name();
       }
    }
}