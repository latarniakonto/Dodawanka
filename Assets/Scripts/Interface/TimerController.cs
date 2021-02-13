using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Dodawanka.Interface
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private Text timerText = null;
        [SerializeField] private float startTime = 30f;
        [SerializeField] float timeToAdd = 1f;
        private bool finished = false;
        private float additionalTime = 0f;
        void Update()
        {
            TimeUpdate();
        }
        public bool Finished()
        {
            return finished;
        }

        private void TimeUpdate()
        {
            if (finished) return;
            float t = startTime - Time.timeSinceLevelLoad + additionalTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f0");
            timerText.text = minutes + ":" + seconds;
            if (t <= 0f)
            {
                additionalTime = 0f;
                finished = true;
            }
        }
        public void AddTime()
        {
            additionalTime += timeToAdd;
        }
    }
}