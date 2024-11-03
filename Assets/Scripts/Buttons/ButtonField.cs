using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonField : MonoBehaviour
{
    private TextMeshProUGUI _buttonText;
    
    private void Start()
    {
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ButtonPressed()
    {
        if(!_buttonText) return;
        var buttonManager = ButtonManager.Instance;
        
        buttonManager.buttonText = _buttonText.text;
        buttonManager.DetermineNumbers();
        buttonManager.pressCount++;
        
    }
}
