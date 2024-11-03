using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private Text finalTotalCountText;

    [SerializeField] private Button throwButton;
    [SerializeField] private GameObject finalTotalUI;
    
    private const int MaxTotal = 200;
    private int _totalAll;
    private int _totalDices;
    private int _throwCount;
    
    private int _firstDiceResult;
    private int _secondDiceResult;
    private int _thirdDiceResult;

    private int _randomNum1;
    private int _randomNum2;
    private int _randomNum3;
    
    private int _minValue = 1;
    private int _maxValue = 7;

    private void Start()
    {
        firstDiceText.text = 6.ToString();
        secondDiceText.text = 6.ToString();
        thirdDiceText.text = 6.ToString();

        totalDicesText.text = _totalDices.ToString();
        totalAllText.text = _totalAll.ToString();
        throwCountText.text = _throwCount.ToString();
        
        RandomNumFind();
    }

    public void StartDiceRollSimulation()
    {
        StartCoroutine(RollDiceSimulation());
    }

    private IEnumerator RollDiceSimulation()
    {
        const float simulationDuration = 1f;
        const float interval = 0.1f; 
        var elapsedTime = 0f;
        
        _throwCount++;
        throwCountText.text = _throwCount.ToString();
        
        while (elapsedTime < simulationDuration)
        {
            CreateRandomDices();
            
            elapsedTime += interval;
            throwButton.interactable = false;
            yield return new WaitForSeconds(interval);
            throwButton.interactable = true;

        }
        
        ThrowCountControl();
        CalculateSelectedNumbers();
        AddDiceResults();
        
        if (_throwCount == 20)
        {
            yield return new WaitForSeconds(3);

            finalTotalUI.SetActive(true);

            finalTotalCountText.text = _totalAll.ToString();
            
        }
        
    }

    private void ThrowCountControl()
    {
        switch (_throwCount)
        {
            case 10:
            {
                switch (_totalAll)
                {
                    case < 80:
                        DetermineUpperMinMax();
                        break;
                    case >= 80 and < 100:
                        DetermineMediumMinMax();
                        break;
                    case >= 100 and < 200:
                        DetermineLowerMinMax();
                        break;
                }

                CreateRandomDices();
                break;
            }
            case 13:
            {
                switch (_totalAll)
                {
                    case < 110:
                        DetermineUpperMinMax();
                        break;
                    case >= 110 and < 130:
                        DetermineMediumMinMax();
                        break;
                    case >= 130 and < 200:
                        DetermineLowerMinMax();
                        break;
                }
                CreateRandomDices();
                break;
            }
            case 15:
            {
                switch (_totalAll)
                {
                    case < 130:
                        DetermineUpperMinMax();
                        break;
                    case >= 130 and < 150:
                        DetermineMediumMinMax();
                        break;
                    case >= 150 and < 200:
                        DetermineLowerMinMax();
                        break;
                }
                CreateRandomDices();
                break;
            }
            case 16:
            {
                switch (_totalAll)
                {
                    case < 140:
                        DetermineUpperMinMax();
                        break;
                    case >= 140 and < 160:
                        DetermineMediumMinMax();
                        break;
                    case >= 160 and < 180:
                        DetermineLowerMinMax();
                        break;
                }
            
                CreateRandomDices();
                break;
            }
            case 17:
            {
                switch (_totalAll)
                {
                    case < 150:
                        DetermineUpperMinMax();
                        break;
                    case >= 150 and < 170:
                        DetermineMediumMinMax();
                        break;
                    case >= 170 and < 190:
                        DetermineLowerMinMax();
                        break;
                }
            
                CreateRandomDices();
                break;
            }
            case 18:
            {
                switch (_totalAll)
                {
                    case < 160:
                        DetermineUpperMinMax();
                        break;
                    case >= 160 and < 180:
                        DetermineMediumMinMax();
                        break;
                    case >= 180 and < 200:
                        DetermineLowerMinMax();
                        break;
                }
            
                CreateRandomDices();
                break;
            }
            case 19:
            {
                switch (_totalAll)
                {
                    case < 170:
                        DetermineUpperMinMax();
                        break;
                    case >= 170 and < 182:
                        DetermineMediumMinMax();
                        break;
                    case >= 182 and < 198:
                        DetermineLowerMinMax();
                        break;
                }
            
                CreateRandomDices();
                break;
            }
            case 20:
            {
                var remainder = MaxTotal - _totalAll;
                CreateRandomDicesToTotal(remainder);
            
                throwButton.interactable = false;
                break;
            }
        }
    }

    private void DetermineUpperMinMax()
    {
        _minValue = 4;
        _maxValue = 7;
    }
    private void DetermineMediumMinMax()
    {
        _minValue = 1;
        _maxValue = 7;
    }
    private void DetermineLowerMinMax()
    {
        _minValue = 1;
        _maxValue = 4;
    }
    private void CreateRandomDices()
    {
        _firstDiceResult = Random.Range(_minValue, _maxValue); 
        _secondDiceResult = Random.Range(_minValue, _maxValue); 
        _thirdDiceResult = Random.Range(_minValue, _maxValue); 
            
        firstDiceText.text = _firstDiceResult.ToString();
        secondDiceText.text = _secondDiceResult.ToString();
        thirdDiceText.text = _thirdDiceResult.ToString();
    }

    private void CreateRandomDicesToTotal(int number)
    {
        do 
        {
            _firstDiceResult = Random.Range(1, 7);

            _secondDiceResult = Random.Range(1, 7);

            _thirdDiceResult = Random.Range(1, 7);
        }while(number != _firstDiceResult + _secondDiceResult + _thirdDiceResult);
            
        firstDiceText.text = _firstDiceResult.ToString();
        secondDiceText.text = _secondDiceResult.ToString();
        thirdDiceText.text = _thirdDiceResult.ToString();

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
    
    private void CalculateSelectedNumbers()
    {
        if (_randomNum1 != _throwCount && _randomNum2 != _throwCount && _randomNum3 != _throwCount) return;
        
        var selectedNumber = 0;
        
        if (_randomNum1 == _throwCount)
        {
            selectedNumber = ButtonManager.Instance.number1;
        }
        else if (_randomNum2 == _throwCount)
        {
            selectedNumber = ButtonManager.Instance.number2;
        }
        else if (_randomNum3 == _throwCount)
        {
            selectedNumber = ButtonManager.Instance.number3;
        }

        CreateRandomDicesToTotal(selectedNumber);

    }
    
    private void RandomNumFind()
    {
        _randomNum1 = Random.Range(1, 11);

        do {
            _randomNum2 = Random.Range(5, 16);
        } while (_randomNum1 == _randomNum2);

        do
        {
            _randomNum3 = Random.Range(10, 19);  //19 ve 20. zarlar toplama göre belirlenmeli.
        } while (_randomNum1 == _randomNum3 || _randomNum2 == _randomNum3 );


        Debug.Log(_randomNum1 + " " + _randomNum2 + " " + _randomNum3);
        
    }
    
}
