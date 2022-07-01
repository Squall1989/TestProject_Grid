using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridProject
{
    public class CellController : MonoBehaviour, IPoolable<RectTransform>
    {
        [SerializeField]
        protected RectTransform[] cellsRect;
        [SerializeField]
        private const float moveTime = 2f;
        [SerializeField]
        private const float animateTime = .5f;

        protected Stack<RectTransform> emptyCells = new Stack<RectTransform>();

        private bool isMoving;

        internal bool IsMoving => isMoving;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var cell in cellsRect)
                emptyCells.Push(cell);


        }

        // Triggering moving cells to exchange
        internal void MoveCell(RectTransform cell, Vector2 targer, int newSibPos)
        {
            // Empty cell, without graphic
            RectTransform empty = GetFromPool();

            int cellSiblingPos = cell.GetSiblingIndex();

            // Exchange empty cell and moving(animating) cell
            Substitute(empty, cell, cellSiblingPos);

            // When end moving, exchange poses
            StartCoroutine(CellMoveCorout(cell, targer, () => Substitute(cell, empty, newSibPos)));

        }

        private IEnumerator CellMoveCorout(Transform cell, Vector3 target, Action endAction)
        {
            float startDist = distance();
            Vector3 moveVector = (target - cell.position).normalized;

            do
            {
                Debug.Log($"speed: {speed()} distance: {distance()}");

                cell.position += moveVector * speed();
                isMoving = true;

                yield return null;

            } while (distance() <= startDist);

            isMoving = false;

            endAction.Invoke();

            float speed() => startDist * Time.deltaTime / moveTime;
            float distance() => (cell.position - target).magnitude;
        }

        private void Substitute(RectTransform outsideCell, RectTransform cellInGrid, int sibPos)
        {
            Transform cellParent = cellInGrid.parent;
            Vector2 cellSize = cellInGrid.sizeDelta;

            // Paste sell in grid
            outsideCell.parent = cellParent;
            outsideCell.SetSiblingIndex(sibPos);

            // Remove cell from grid
            cellInGrid.parent = transform;
            cellInGrid.localScale = Vector3.one;
            cellInGrid.sizeDelta = cellSize;
        }

        //private IEnumerator AnimateCorout()
        //{
        //
        //}

        public RectTransform GetFromPool()
        {
            if (emptyCells.Count == 0)
            {
                RectTransform newEmptyCell = Instantiate(cellsRect[0], transform);
                return newEmptyCell;
            }
            else
                return emptyCells.Pop();
        }

        public void ReturnToPool(RectTransform returned)
        {
            emptyCells.Push(returned);
        }
    }
}