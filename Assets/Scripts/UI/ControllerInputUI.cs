using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace GridProject
{
    /// <summary>
    /// Input events controller
    /// </summary>
    public class ControllerInputUI : MonoBehaviour, ISafeAreaMovable
    {
        [SerializeField]
        private TMP_InputField inputWidth;
        [SerializeField]
        private TMP_InputField inputHeight;
        [SerializeField]
        private Button generateButton;
        [SerializeField]
        private Button randomizeButton;

        [SerializeField]
        private int minSideSize, maxSideSize;

        private int fieldWidth, fieldHeight;

        public Action<int, int> OnGenerate;
        public Action OnRandomize;

        private Vector2 startPos;
        
        public Vector2 StartPos { get =>  startPos; set => startPos = value; }

        public RectTransform MovableRect => transform as RectTransform;

        private void Awake()
        {
            inputWidth.onEndEdit.AddListener(ChangeWidth);
            inputHeight.onEndEdit.AddListener(ChangeHeight);

            generateButton.onClick.AddListener(() => OnGenerate?.Invoke(fieldWidth, fieldHeight));
            randomizeButton.onClick.AddListener(() => OnRandomize?.Invoke());
            // Set min size
            CheckSize(ref fieldWidth);
            CheckSize(ref fieldHeight);
        }

        private void Start()
        {
            MainControllerUI.Instance.RegisterSafeAreaMovable(this);
        }

        private void ChangeHeight(string heightStr)
        {
            ChangeSide(inputHeight, heightStr, ref fieldHeight);
        }

        private void ChangeWidth(string widthStr)
        {
            ChangeSide(inputWidth, widthStr, ref fieldWidth);
        }

        private void ChangeSide(TMP_InputField inputField, string sizeStr, ref int sideVal)
        {
            sideVal = StrToInt(sizeStr);
            CheckSize(ref sideVal);
            // If CheckSIze changed value
            inputField.text = sideVal.ToString();
        }

        private void CheckSize(ref int size)
        {
            if (size < minSideSize)
                size = minSideSize;
            else if (size > maxSideSize)
                size = maxSideSize;
        }

        private int StrToInt(string val)
        {
            try
            {
                int intVal = Convert.ToInt32(val);
                return intVal;
            }
            catch (Exception e)
            {
                Debug.LogError("Got a converting exception: " + e);
                return 0;
            }
        }
    }
}