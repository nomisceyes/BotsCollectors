using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _score = 0;

    public event Action<int> ScoreChanged;

    private void Awake() =>    
        ScoreChanged?.Invoke(_score);    

    public void AddScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }
}