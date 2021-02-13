using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Dodawanka.Progress;


namespace Dodawanka.Interface
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int repetition = 1;
        private List<int> figureAugend = new List<int>();
        private List<int> progressionList = new List<int>();     
        private Progression progression = null;
        private int ile = 4;
        private int sum = 0;
        private bool generated = false;
        private float time = 0.05f;
        private int licznik = 0;
        private int counter = 0;
        private bool firstProgression = false;
        private bool secondProgression = false;        
        public bool IsPrime(int number)
        {
            if (number == 1) return false;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
        public bool RandomPrime(int l, int r) // Musi być odpowiedni przedział, w którym jest wystarczająca dużo liczb pierwszych
        {
            int begin = Random.Range(l, r);
            for (int i = begin; i < r; i++)
            {
                if (IsPrime(i))
                {
                    if (figureAugend.Contains(i)) continue;
                    figureAugend.Add(i);
                    return true;
                }
            }
            return false;
        }
        public int GenerateSum()
        {
            Progress();                      
            while (licznik < 4)
            {
                if (RandomPrime(progression.From(), progression.SecondProgression(secondProgression))) licznik++;                
            }
            licznik = 0;
            ile = 4;
            sum = 0;
            int i = 0;
            while (i < 4)
            {
                int r = Random.Range(0, ile - 1);
                if (i == 3) r = ile;
                figureAugend[i] = progression.FirstProgression(figureAugend[i], firstProgression);
                sum += (figureAugend[i] * r);
                ile -= r;
                i++;
            }
            StartCoroutine(ClearSignal());
            print(sum);
            if (progressionList.Contains(sum)) counter++;
            progressionList.Add(sum);
            return sum;
        }

        public void Progress()
        {            
            progression = GetComponent<Progression>();                        
            if (counter == repetition)
            {
                if (firstProgression == false) firstProgression = true;
                else secondProgression = true;
                progressionList.Clear();
                counter = 0;
            }
        }

        public int Augend()
        {
            if (figureAugend.Count == 0) throw new InvalidDataException();
            int augend = figureAugend[figureAugend.Count - 1];
            figureAugend.RemoveAt(figureAugend.Count - 1);
            return augend;
        }
        public IEnumerator ClearSignal()
        {
            generated = true;
            yield return new WaitForSeconds(time);
            generated = false;
        }
        public bool Generated()
        {
            return generated;
        }
        public bool AugendsLeft()
        {
            return figureAugend.Count != 0;
        }
    }
}
