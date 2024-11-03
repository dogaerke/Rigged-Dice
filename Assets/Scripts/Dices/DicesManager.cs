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
    private const int MinRangeValue = 1;
    private const int MaxRangeValue = 7;

    private readonly List<Vector3Int> _allDicesList = new List<Vector3Int>();
    
    private int _totalAll;
    private int _totalDices;
    private int _throwCount;
    
    private int _firstDiceResult;
    private int _secondDiceResult;
    private int _thirdDiceResult;

    private int _randomNum1;
    private int _randomNum2;
    private int _randomNum3;
    
    private void OnEnable()
    {
        firstDiceText.text = 6.ToString();
        secondDiceText.text = 6.ToString();
        thirdDiceText.text = 6.ToString();

        totalDicesText.text = _totalDices.ToString();
        totalAllText.text = _totalAll.ToString();
        throwCountText.text = _throwCount.ToString();
        
        RandomNumFind();
        
        FindRandomValuesToMaxTotal();
        EditList();
        
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
        
        while (elapsedTime < simulationDuration)
        {
            _firstDiceResult = Random.Range(MinRangeValue, MaxRangeValue); 
            _secondDiceResult = Random.Range(MinRangeValue, MaxRangeValue); 
            _thirdDiceResult = Random.Range(MinRangeValue, MaxRangeValue); 
            
            firstDiceText.text = _firstDiceResult.ToString();
            secondDiceText.text = _secondDiceResult.ToString();
            thirdDiceText.text = _thirdDiceResult.ToString();
            
            elapsedTime += interval;
            throwButton.interactable = false;
            yield return new WaitForSeconds(interval);
            throwButton.interactable = true;

        }
        
        PrintDice(_allDicesList[_throwCount]);
        AddDiceResults(_allDicesList[_throwCount]);
        
        throwCountText.text = (++_throwCount).ToString();
        
        if (_throwCount == 20)
        {
            throwButton.interactable = false;
            yield return new WaitForSeconds(2);
            finalTotalUI.SetActive(true);
            finalTotalCountText.text = _totalAll.ToString();
            
        }
        
    }

    private void PrintDice(Vector3Int nums)
    {
        firstDiceText.text = nums.x.ToString();
        secondDiceText.text = nums.y.ToString();
        thirdDiceText.text = nums.z.ToString();

    }
    private void AddDiceResults(Vector3Int nums)
    {
        _totalDices += nums.x + nums.y + nums.z;
        totalDicesText.text = _totalDices.ToString();
        AddAllDiceResults();
        _totalDices = 0;
    }

    private void AddAllDiceResults()
    {
        _totalAll += _totalDices;
        totalAllText.text = _totalAll.ToString();
    }
    
    private void EditList()
    {
        (_allDicesList[_randomNum1 - 1], _allDicesList[17]) = (_allDicesList[17], _allDicesList[_randomNum1 - 1]);
        (_allDicesList[_randomNum2 - 1], _allDicesList[18]) = (_allDicesList[18], _allDicesList[_randomNum2 - 1]);
        (_allDicesList[_randomNum3 - 1], _allDicesList[19]) = (_allDicesList[19], _allDicesList[_randomNum3 - 1]);
    }

    private void FindRandomValuesToMaxTotal()
    {
        do
        {
            _allDicesList.Clear();
            for (var i = 0; i < 17; i++)
                AddToListRandomDices();
            
            AddToListSelectedNumbers();
            
        } while (AddListVariables() != MaxTotal);
    }


    private int AddListVariables()
    {
        var total = 0;
        for (var i = 0; i < 20; i++)
        {
            total += _allDicesList[i].x + _allDicesList[i].y + _allDicesList[i].z;
        }

        return total;
    }
    private void AddToListRandomDices()
    {
        _firstDiceResult = Random.Range(MinRangeValue, MaxRangeValue); 
        _secondDiceResult = Random.Range(MinRangeValue, MaxRangeValue); 
        _thirdDiceResult = Random.Range(MinRangeValue, MaxRangeValue); 
        
        _allDicesList.Add(new Vector3Int(_firstDiceResult, _secondDiceResult, _thirdDiceResult));

    }
    private void CreateRandomDicesToTotal(int number)
    {
        do 
        {
            _firstDiceResult = Random.Range(1, 7);

            _secondDiceResult = Random.Range(1, 7);

            _thirdDiceResult = Random.Range(1, 7);
        }while(number != _firstDiceResult + _secondDiceResult + _thirdDiceResult);
            
        _allDicesList.Add(new Vector3Int(_firstDiceResult, _secondDiceResult, _thirdDiceResult));

    }

    private void AddToListSelectedNumbers()
    {
        CreateRandomDicesToTotal(ButtonManager.Instance.number1);
        CreateRandomDicesToTotal(ButtonManager.Instance.number2);
        CreateRandomDicesToTotal(ButtonManager.Instance.number3);

    }
    
    private void RandomNumFind()
    {
        _randomNum1 = Random.Range(1, 11);

        do {
            _randomNum2 = Random.Range(5, 16);
        } while (_randomNum1 == _randomNum2);

        do
        {
            _randomNum3 = Random.Range(10, 21);
        } while (_randomNum1 == _randomNum3 || _randomNum2 == _randomNum3 );


        Debug.Log("Random Throw Orders for Selected Numbers: " + _randomNum1 + ", " + _randomNum2 + ", " + _randomNum3);
        
    }
    
}
