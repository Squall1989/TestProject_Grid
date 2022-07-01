using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        // Size of grid
        protected float startSizeX, startSizeY;

        protected int activeIndex = 0;

        protected List<Cell<T>> availableCells;

        internal int ActiveIndex => activeIndex;

        protected int cellCount => availableCells == null ? 0 : availableCells.Count;

        internal int activeCells => cellCount - activeIndex;

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

            if (arrayCells.Length == 0)
            {
                throw new Exception($"Need at least one cell of this type: {typeof(Cell<T>)} in generator childrens");
            }

            // Add to main list
            availableCells.AddRange(arrayCells);

        }


        // Start generate
        internal virtual void Generate(int width, int height)
        {

            if (cellCount < width * height)
            {
                CreateNewCells(width * height - cellCount);
            }

            activeIndex = cellCount - width * height;
            // Deactive unnecessary cells
            ExcessCells(activeIndex);
        }


        protected void CreateNewCells(int howMuch)
        {
            // Change list correct capacity
            availableCells.Capacity = cellCount + howMuch;

            for (int i = 0; i < howMuch; i++)
            {
                var newCell = Instantiate(availableCells[0], transform);
                newCell.transform.localScale = Vector3.one;

                availableCells.Add(newCell);
            }
        }

        protected void ExcessCells(int howMuch)
        {
            for (int i = 0; i < availableCells.Count; i++)
            {
                bool isActive = i >= howMuch;

                availableCells[i].Activate(isActive);

                if (isActive)
                    GenerateCell(availableCells[i]);
            }
        }

        List<int> indexList;

        internal void ResetIndexGenerator()
        {
            // Fill the list with cell list indexes
            indexList = Enumerable.Range(activeIndex, availableCells.Count - activeIndex).ToList();
            
        }

        internal (Cell<T> c1, Cell<T> c2) GetUniqueCellPair() => (RandomCell(), RandomCell());
        // Get cell from random index and remove index from list
        private Cell<T> RandomCell()
        {
            int cellIndex = indexList[GenerateIndex()];
            Cell<T> cell = availableCells[cellIndex];
            indexList.Remove(cellIndex);
            return cell;
        }

        private int GenerateIndex() => Random.Range(0, indexList.Count);

        protected abstract void GenerateCell(Cell<T> cell);
    }
}