using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private GameObject throwDicesUI;
    [SerializeField] private GameObject finalTotalUI;
    [SerializeField] private GameObject selectNumberUI;
    [SerializeField] private GameObject selectedNumbersBackground;
    private void Start()
    {
        selectNumberUI.SetActive(true);
        selectedNumbersBackground.SetActive(true);
        finalTotalUI.SetActive(false);
        throwDicesUI.SetActive(false);
    }

}
