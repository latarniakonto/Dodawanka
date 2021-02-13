using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dodawanka.Interface;

namespace Dodawanka.Logic
{
    public class FiugreController : MonoBehaviour
    {
        [SerializeField] private GameObject figureToSpawn;
        [SerializeField] private Vector3 startingPosition;
        [SerializeField] private Text textAugend;


        private AddingController addingController = null;
        private LevelGenerator levelGenerator = null;


        private int augend;
        private Stack<GameObject> gameObjectsList = new Stack<GameObject>();
        void Start() // Pobiera potrzebne komponenty na samym początku
        {
            levelGenerator = FindObjectOfType<LevelGenerator>();
            transform.position = startingPosition;
            addingController = GetComponentInParent<AddingController>();
            augend = levelGenerator.Augend();
            textAugend.text = augend.ToString();
        }

        void Update() //Co klatkę wykonuje to co się w nim znajduje
        {
            if (levelGenerator.Generated())
            {
                ClearBoard();
                if (levelGenerator.AugendsLeft())
                {
                    augend = levelGenerator.Augend();
                    textAugend.text = augend.ToString();
                }
            }
            textAugend.text = augend.ToString();
            SpawnFigure();
        }

        private void SpawnFigure()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == this.transform)
                    {
                        if (addingController.CanAdd())
                        {
                            Vector3 nextPosition = addingController.NextPosition(augend);
                            gameObjectsList.Push(Instantiate(figureToSpawn, nextPosition, Quaternion.identity));
                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown(1) && gameObjectsList.Count > 0)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == this.transform)
                    {
                        GameObject toDelete = gameObjectsList.Pop();
                        addingController.FigureDeleted(toDelete, augend);
                        Destroy(toDelete);
                    }
                }
            }
        }
        private void ClearBoard()
        {
            addingController.FreeAllPositions();
            while (gameObjectsList.Count != 0)
            {
                GameObject toDelete = gameObjectsList.Pop();
                Destroy(toDelete);
            }
        }
    }
}