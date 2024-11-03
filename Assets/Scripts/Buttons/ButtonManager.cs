using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonManager : MonoBehaviour
{
    public int number1;
    public int number2;
    public int number3;
    
    public int pressCount;
    public string buttonText;
    public static ButtonManager Instance { get; private set; }
    
    [SerializeField] private GameObject numbersUI;
    [SerializeField] private GameObject dicesUI;
    [SerializeField] private Text numbersText;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
    
        else
            Instance = this;
        
    }

    public void DetermineNumbers()
    {
        switch (pressCount)
        {
            case 0:
                int.TryParse(buttonText, out number1);        
                Debug.Log("First Selected Number: " + number1);

                break;
            case 1:
                int.TryParse(buttonText, out number2);
                Debug.Log("Second Selected Number: " + number2);

                break;
            case 2:
                int.TryParse(buttonText, out number3);
                Debug.Log("Third Selected Number: " + number3);

                dicesUI.SetActive(true);
                numbersUI.SetActive(false);
                WriteNumbersToText();
                break;
        }
    }
    private void WriteNumbersToText()
    {
        numbersText.text = $"{number1} - {number2} - {number3}";

    }
    
}
