using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridProject
{
    /// <summary>
    /// Generates the required array of cells
    /// Using with only one cell type
    /// In childrens must have at least 1 cell
    /// Grid layout group in the same object
    /// </summary>
    public abstract class CellGenerator<T> : MonoBehaviour
    {
        protected float startSizeX, startSizeY;

        protected List<Cell<T>> availableCells;

        protected int cellCount => availableCells == null ? 0 : availableCells.Count;

        protected virtual void Awake()
        {
            startSizeX = (transform as RectTransform).sizeDelta.x;
            startSizeY = (transform as RectTransform).sizeDelta.y;

            GetAvailableCells();
        }

        // From start, find cells in childrens
        protected void GetAvailableCells()
        {
            // get in children
            var arrayCells = transform.GetComponentsInChildren<Cell<T>>(true);
            availableCells = new List<Cell<T>>(arrayCells.Length);

            if(arrayCells.Length == 0)
            {
                throw new Exception($"Need at least one cell of this type: {typeof(Cell<T>)} in generator childrens");
            }

            // Add to main list
            foreach (var cell in arrayCells)
            {
                availableCells.Add(cell);
            }
            
        }


        // Start generate
        internal virtual void Generate(int width, int height)
        {

            if(cellCount < width * height)
            {
                CreateNewCells(width * height - cellCount);
            }
            
            // Deactive unnecessary cells
            ExcessCells(cellCount - width * height);
        }


        protected void CreateNewCells(int howMuch)
        {
            // Change list correct capacity
            availableCells.Capacity = cellCount + howMuch;

            for(int i = 0; i < howMuch; i++)
            {
                var newCell = Instantiate(availableCells[0], transform);
                newCell.transform.localScale = Vector3.one;

                availableCells.Add(newCell);
            }
        }

        protected void ExcessCells(int howMuch)
        {
            for(int i = 0; i < availableCells.Count; i++)
            {
                bool isActive = i >= howMuch;

                availableCells[i].Activate(isActive);

                if (isActive)
                    GenerateCell(availableCells[i]);
            }
        }

        protected abstract void GenerateCell(Cell<T> cell);
    }
}