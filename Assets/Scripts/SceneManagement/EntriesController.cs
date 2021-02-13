using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Dodawanka.SceneManagement
{
    public class EntriesController : MonoBehaviour
    {
        [SerializeField] private int howManyToShow = 10;
        [SerializeField] private GameObject entryObject = null;
        [SerializeField] private Vector3 startinPosition;
        [SerializeField] private float offset = 3f;
        [SerializeField] private float next = 3f;
        [SerializeField] private Transform parent = null;
        private SortedList<Entry, Entry> entries = new SortedList<Entry, Entry>();
        private List<GameObject> entryGameObjects = new List<GameObject>();
        private Vector3 position;
                        

        public void CreateEntries(Dictionary<string, int> scoreTable)
        {            
            foreach(var e in scoreTable)
            {                
                Entry entry = new Entry(e.Key, e.Value);
                entries[entry] = entry;                
            }
            InstantiateEntries();
        }

        public void InstantiateEntries()
        {
            int i = 1;
            foreach(var e in entries)
            {
                if (i > howManyToShow) continue;
                GameObject instantiateEntry = entryObject;
                Text[] text = instantiateEntry.GetComponentsInChildren<Text>();
                text[0].text = i.ToString();
                text[1].text = e.Value.Name();
                text[2].text = e.Value.Score().ToString();
                i++;

                GameObject instanitatedEntry = Instantiate(instantiateEntry);
                instanitatedEntry.transform.SetParent(parent);
                position = startinPosition;
                Positioning(instanitatedEntry.GetComponentsInChildren<RectTransform>());
                entryGameObjects.Add(instanitatedEntry);
            }                       
        }
        public void Positioning(RectTransform[] rects)
        {
            int i = 0;
            foreach(var rect in rects)
            {
                if (rect.name != "Rank" && rect.name != "Player" && rect.name != "Score") continue;           

                rect.localPosition = new Vector3(startinPosition.x + i * offset, startinPosition.y, startinPosition.z);
                i++;
            }
           position += new Vector3(next, next, next);
        }
        public void ClearHighScores()
        {
            while(entryGameObjects.Count != 0)
            {
                GameObject toDestroy = entryGameObjects[entryGameObjects.Count - 1];
                entryGameObjects.RemoveAt(entryGameObjects.Count - 1);
                Destroy(toDestroy);
            }
            entries.Clear();
        }
    }
}