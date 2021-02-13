using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dodawanka.SceneManagement
{
    public class Entry : ScriptableObject, IComparable<Entry>
    {        
        private string playerName;
        private int playerScore;
        public Entry(string name, int score)
        {            
            playerName = name;
            playerScore = score;
        }

        public int CompareTo(Entry other)
        {
            if (this.playerScore < other.playerScore) return 1;
            else if (this.playerScore > other.playerScore) return -1;
            else if (this.playerName == other.playerName) return 0;
            return 1;            
        }

        public string Name()
        {
            return playerName;
        }
        public int Score()
        {
            return playerScore;
        }        
    }
}