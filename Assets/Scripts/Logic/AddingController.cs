using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dodawanka.Interface;
using System.ComponentModel;

namespace Dodawanka.Logic
{
    public class AddingController : MonoBehaviour
    {
        [SerializeField] private Vector3 nextPosition;

        private CanvasController canvasController = null;

        private int howManyFigures = 0;
        private int sum = 0;
        private (bool isFree, Vector3 position)[] positions = new (bool, Vector3)[4];
        void Start()
        {
            canvasController = FindObjectOfType<CanvasController>();
            sum = canvasController.Sum();
            for (int i = 0; i < 4; i++)
            {
                if (i != 0 && i % 2 == 0) nextPosition.y += 2f;
                positions[i].isFree = true;
                positions[i].position.x = nextPosition.x + 2 * (i % 2);
                positions[i].position.y = nextPosition.y;
                positions[i].position.z = nextPosition.z;
            }
        }
        public Vector3 NextPosition(int augend)
        {
            for (int i = 0; i < 4; i++)
            {
                if (positions[i].isFree)
                {
                    OnceAdded(augend, i);
                    break;
                }
            }
            return nextPosition;
        }

        private void OnceAdded(int augend, int i)
        {            
            nextPosition = positions[i].position;
            positions[i].isFree = false;
            howManyFigures++;
            sum -= augend;
            if (sum == 0 && howManyFigures == 4)
            {
                canvasController.UpdateScore();
                howManyFigures = 0;
                sum = canvasController.Sum();
            }
            else
            {
                canvasController.UpdateSum(sum);
            }
        }
        public void FigureDeleted(GameObject deletedFigure, int augend)
        {
            for (int i = 0; i < 4; i++)
            {
                if (deletedFigure.transform.position == positions[i].position)
                {
                    OnceDeleted(augend, i);
                    break;
                }
            }
        }
        private void OnceDeleted(int augend, int i)
        {
            positions[i].isFree = true;
            howManyFigures--;
            sum += augend;
            canvasController.UpdateSum(sum);
        }
        public void FreeAllPositions()
        {
            for (int i = 0; i < 4; i++)
            {
                positions[i].isFree = true;
            }
        }
        public bool CanAdd()
        {
            return howManyFigures != 4;
        }
    }
}