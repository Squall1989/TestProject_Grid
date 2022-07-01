using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GridProject
{
    public class MainControllerUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform cellField;
        [SerializeField]
        private CellGeneratorText generatorText;
        [SerializeField]
        private ControllerInputUI controllerInput;
        [SerializeField]
        private CellController cellMoveController;
        [SerializeField]
        private bool randomizeInParallel;

        private GridLayoutGroup cellLayoutGroup;


        private void Awake()
        {
            cellLayoutGroup = cellField.GetComponent<GridLayoutGroup>();
        }

        private void Start()
        {
            SubscribeInput();
        }

        private void SubscribeInput()
        {
            controllerInput.OnGenerate += ReSize;
            controllerInput.OnGenerate += generatorText.Generate;
            controllerInput.OnRandomize += () => StartCoroutine(RandomizateCellsCorout());
        }

        private IEnumerator  RandomizateCellsCorout()
        {
            int pairCount = generatorText.cellCount;
            generatorText.ResetIndexGenerator();

            for (int i = 0; i < pairCount; i++)
            {
                yield return new  WaitUntil(() => isMovingNext());
                cellPairPrepair();
            }

            void cellPairPrepair()
            {
                var cellPair = generatorText.GetUniqueCellPair();

                int siblingC1 = cellPair.c1.transform.GetSiblingIndex();
                int siblingC2 = cellPair.c2.transform.GetSiblingIndex();

                Vector2 posC1 = cellPair.c1.transform.position;
                Vector2 posC2 = cellPair.c2.transform.position;

                

                // Move first cell to second cell pos and conversely
                cellMoveController.MoveCell(cellPair.c1.rectTR, posC2, siblingC2);
                cellMoveController.MoveCell(cellPair.c2.rectTR, posC1, siblingC1);
            }

            // if parallel -> starting all cells exchanges; if not and still moving -> wait
            bool isMovingNext() => randomizeInParallel || !cellMoveController.IsMoving;
        }

        protected void ReSize(int width, int height)
        {
            // Constraint is fixed columns or rows
            if (cellLayoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
                cellLayoutGroup.constraintCount = width;
            else
                cellLayoutGroup.constraintCount = height;

            float cellSide = CalcCellSide(width, height);

            cellLayoutGroup.cellSize = new Vector2(cellSide, cellSide);
        }

        private float CalcCellSide(int width, int height)
        {
            float sizeX = cellField.rect.width;
            float sizeY = cellField.rect.height;

            float side = sizeX / width;


            if (side * height > sizeY)
                side = sizeY / height;

            return side;
        }

        private float CalcUnsafeHeight() => (Screen.height - Screen.safeArea.height) / 2f;
    }
}