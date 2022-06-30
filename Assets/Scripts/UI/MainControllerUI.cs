using System;
using System.Collections;
using System.Collections.Generic;
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

        private GridLayoutGroup cellLayoutGroup;

        [SerializeField]
        private const float borderSize = 50f;

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

            float side = sizeY / height;

            if (side * width > sizeX)
                side = sizeX / width;

            return side;
        }

        private float CalcUnsafeHeight() => (Screen.height - Screen.safeArea.height) / 2f;
    }
}