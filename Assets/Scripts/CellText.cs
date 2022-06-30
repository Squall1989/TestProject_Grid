using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GridProject
{
    /// <summary>
    /// The text cell, visualize using TMPto
    /// 
    /// </summary>
    public class CellText : Cell<TextMeshProUGUI>
    {


        internal override void Activate(bool isActive)
        {
            cellMainType.enabled = isActive;
        }

        internal override void Animate(float animStartTime, bool isStart)
        {

        }

        internal override void Generate()
        {

        }

        internal override void Move(float moveTime)
        {

        }
    }
}