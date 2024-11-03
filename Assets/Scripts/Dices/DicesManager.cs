using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DicesManager : MonoBehaviour
{
    [SerializeField] private Text firstDiceText;
    [SerializeField] private Text secondDiceText;
    [SerializeField] private Text thirdDiceText;
    [SerializeField] private Text totalDicesText;
    [SerializeField] private Text totalAllText;
    [SerializeField] private Text throwCountText;
    [SerializeField] private Button throwButton;

    private int _firstDiceResult;
    private int _secondDiceResult;
    private int _thirdDiceResult;
    private int _totalDices;
    private int _totalAll;
    private int _throwCount;

    private int _counter1 = 1;
    private int _randomNum1;
    private int _randomNum2;
    private int _randomNum3;

    private void Start()
    {
        firstDiceText.text = 6.ToString();
        secondDiceText.text = 6.ToString();
        thirdDiceText.text = 6.ToString();

        totalDicesText.text = _totalDices.ToString();
        totalAllText.text = _totalAll.ToString();
        throwCountText.text = _throwCount.ToString();

        _randomNum1 = Random.Range(1, 11);
        _randomNum2 = Random.Range(5, 16);
        _randomNum3 = Random.Range(10, 21);
    }

    public void StartDiceRollSimulation()
    {
        StartCoroutine(RollDiceSimulation());
    }

    private IEnumerator RollDiceSimulation()
    {
        const float simulationDuration = 2f; 
        const float interval = 0.1f; 
        var elapsedTime = 0f;

        while (elapsedTime < simulationDuration)
        {
            _firstDiceResult = Random.Range(1, 7); 
            _secondDiceResult = Random.Range(1, 7); 
            _thirdDiceResult = Random.Range(1, 7); 
            
            firstDiceText.text = _firstDiceResult.ToString();
            secondDiceText.text = _secondDiceResult.ToString();
            thirdDiceText.text = _thirdDiceResult.ToString();
            
            elapsedTime += interval;
            throwButton.interactable = false;
            yield return new WaitForSeconds(interval);
            throwButton.interactable = true;

        }
        CalculateNumbers();

        throwCountText.text = (++_throwCount).ToString();
        ThrowCountControl();
        AddDiceResults();
        
    }

    private void CalculateNumbers()
    {
        int number;
        if (_randomNum1 == _throwCount || _randomNum2 == _throwCount || _randomNum3 == _throwCount)
        {
            var number1 = ButtonManager.Instance.number1;
            var number2 = ButtonManager.Instance.number2;
            var number3 = ButtonManager.Instance.number3;

            _firstDiceResult = Random.Range(1, number1 - 1); //Max seçilen sayının 2 eksiği olabilir

            _secondDiceResult = Random.Range(1, number1 - _firstDiceResult);

            _thirdDiceResult = number1 - _firstDiceResult - _secondDiceResult;

            firstDiceText.text = _firstDiceResult.ToString();
            secondDiceText.text = _secondDiceResult.ToString();
            thirdDiceText.text = _thirdDiceResult.ToString();
        }
    }

    private void ThrowCountControl()
    {
        if (_throwCount == 20)
        {
            throwButton.interactable = false;

            Debug.Log("You threw it 20 times.");
            //TOTAL 200......
        }
    }

    private void AddDiceResults()
    {
        _totalDices += _firstDiceResult + _secondDiceResult + _thirdDiceResult;
        totalDicesText.text = _totalDices.ToString();
        AddAllDiceResults();
        _totalDices = 0;
    }

    private void AddAllDiceResults()
    {
        _totalAll += _totalDices;
        totalAllText.text = _totalAll.ToString();
    }

    
}
