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
        private float moveTime = 2f;
        [SerializeField]
        private float animateTime = .5f;

        protected Stack<RectTransform> emptyCells;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var cell in cellsRect)
                emptyCells.Push(cell);


        }

        // Triggering moving cells to exchange
        internal void MoveCell(RectTransform cell, Vector2 destination)
        {
            // Need empty cells, for gridLayoutGroup
            var empty = GetFromPool();

            StartCoroutine(CellMoveCorout(cell, empty));
        }

        private IEnumerator CellMoveCorout(RectTransform cell, RectTransform empty)
        {

        }

        private IEnumerator AnimateCorout()
        {

        }

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