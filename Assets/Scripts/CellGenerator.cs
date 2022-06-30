using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridProject
{
    /// <summary>
    /// Generates the required array of cells
    /// Using with only one cell type
    /// In childrens must have at least 1 cell
    /// Grid layout group in the same object
    /// </summary>
    public class CellGenerator<T> : MonoBehaviour
    {
        private List<Cell<T>> availableCells;

        private int cellCount => availableCells == null ? 0 : availableCells.Count;

        private void Awake()
        {
            GetAvailableCells();

        }

        // From start, find cells in childrens
        private void GetAvailableCells()
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

        // When some need a square of cells
        internal void Generate(int width, int height)
        {
            if(cellCount < width * height)
            {
                CreateNewCells(width * height - cellCount);
            }
            else
            {
                // Deactive unnecessary cells
                ExcessCells(cellCount - width * height);
            }
        }

        private void CreateNewCells(int howMuch)
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

        private void ExcessCells(int howMuch)
        {
            for(int i = 0; i < howMuch; i++)
            {
                availableCells[i].Activate(false);
            }
        }
    }
}