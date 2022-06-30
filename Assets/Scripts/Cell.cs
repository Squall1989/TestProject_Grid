using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridProject
{
    /// <summary>
    /// Some cell, used on field
    /// Based on generated type
    /// </summary>
    public abstract class Cell<T> : MonoBehaviour
    {
        [SerializeField]
        protected T cellMainType;
        internal virtual T CellVal { get; private set; }

        internal abstract void Generate();


        internal abstract void Activate(bool isActive);

        internal abstract void Animate(float animStartTime, bool isStart);

        internal abstract void Move(float moveTime);

        internal virtual void EndMove(T newVal)
        {
            CellVal = newVal;
            Activate(true);
        }

        protected virtual void Init(T initVal)
        {
            CellVal = initVal;
            Activate(true);
        }
    }
}