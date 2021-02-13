using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Dodawanka.SceneManagement
{
    public class Results : MonoBehaviour
    {
        private Text results = null;
        private int score = -1;

        private void OnEnable()
        {
            if (!PlayerPrefs.HasKey("score")) return;
            score = PlayerPrefs.GetInt("score");
            PlayerPrefs.DeleteKey("score");
            results = GetComponent<Text>();
            if (results == null) return;
            results.text = "Your score: " + score.ToString();
        }
        public int Score()
        {
            return score;
        }
    }
}