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
        internal virtual object CellVal { get; private set; }

        internal abstract void Generate();

        protected abstract void SetEnable(bool isEnable);

        internal virtual void Activate(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        // initVal may be significant type, then need unboxing
        internal virtual void Init(object initVal)
        {
            CellVal = initVal;
            Activate(true);
        }
    }
}