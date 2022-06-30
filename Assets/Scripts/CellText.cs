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
        protected override void SetEnable(bool isEnable)
        {
            cellMainType.enabled = isEnable;
        }

        internal override void Generate()
        {

        }


        internal override void Init(object initVal)
        {
            base.Init(initVal);
            cellMainType.text = initVal.ToString();
        }

    }
}