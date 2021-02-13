using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dodawanka.Progress
{
    public class Progression : MonoBehaviour
    {
        //Trzeba jakiś range ustalić;
        [SerializeField] private int newNumebersTo = 8;// Nie może być za duże       
        const int numbersFrom = 2; // Gdyby ktoś zmienił wartość tego, mógłby tym samy rozwalić algorytm znajdujący liczby pierwsze
        private int numbersTo = 8;
        private bool firstProgress = false;
        private bool secondProgress = false;
        public bool FirstProgress { get => firstProgress; }
        public bool SecondProgress { get => secondProgress; }


        public int From()
        {
            return numbersFrom;
        }                
        public int FirstProgression(int number, bool canProgress)
        {
            if (!canProgress) return number;
            firstProgress = true;
            number += (-2 * Random.Range(0, 2) * number);            
            return number;
        }
        public int SecondProgression(bool canProgress)
        {
            if (!canProgress) return numbersTo;
            secondProgress = true;
            numbersTo = newNumebersTo;            
            return numbersTo;
        }
    }
}