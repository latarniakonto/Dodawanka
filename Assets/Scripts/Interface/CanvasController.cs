using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Dodawanka.SceneManagement;
using Dodawanka.Progress;

namespace Dodawanka.Interface
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private Text scoreText = null;
        [SerializeField] private Text sumText = null;
        [SerializeField] private int firstProgressPoints = 10;
        [SerializeField] private int secondProgressPoints = 20;
        private int score = 0;
        private int sum = 20;
        private int pointsToAdd = 10;
        private LevelGenerator levelGenerator = null;
        private TimerController timerController = null;
        private SceneController sceneController = null;
        private Progression progress = null;


        // Start is called before the first frame update
        private void Awake()
        {            
            levelGenerator = FindObjectOfType<LevelGenerator>();
            scoreText.text = "Score: " + score.ToString();
            sum = levelGenerator.GenerateSum();
            sumText.text = sum.ToString();
            timerController = GetComponent<TimerController>();
            sceneController = GetComponent<SceneController>();
            progress = FindObjectOfType<Progression>();
        }
        private void Update()
        {           
            if (timerController.Finished())
            {
                sceneController.GameOver();
            }
        }
        public void UpdateScore()
        {
            int toAdd = pointsToAdd;
            if (progress.FirstProgress) toAdd += firstProgressPoints;
            if (progress.SecondProgress) toAdd += secondProgressPoints;
            score += toAdd;
            scoreText.text = "Score: " + score.ToString();
            UpdateSum(levelGenerator.GenerateSum());
            timerController.AddTime();
        }
        public void UpdateSum(int changeSum)
        {
            sum = changeSum;
            sumText.text = changeSum.ToString();
        }
        public int Sum()
        {
            return sum;
        }
        private void OnDisable() //Zachowuje wynik gracza miedzy scenami
        {
            PlayerPrefs.SetInt("score", score);
        }
    }
}