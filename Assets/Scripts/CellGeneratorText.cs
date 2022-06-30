using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using random = UnityEngine.Random;

namespace GridProject
{

    public class CellGeneratorText : CellGenerator<TextMeshProUGUI>
    {

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";


        protected override void Awake()
        {
            base.Awake();

            
        }

        protected override void GenerateCell(Cell<TextMeshProUGUI> cell)
        {
            object ch = chars[random.Range(0, chars.Length)];
            cell.Init(ch);

        }

        internal override void Generate(int width, int height)
        {
            base.Generate(width, height);

        }
    }
}