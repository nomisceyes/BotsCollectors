using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;

    private Text _scoreText;

    private void Awake() =>
        _scoreText = GetComponent<Text>();

    private void OnEnable() =>
        _scoreCounter.ScoreChanged += OnScoreChanged;

    private void OnDisable() =>
        _scoreCounter.ScoreChanged -= OnScoreChanged;

    private void OnScoreChanged(int score) =>
        _scoreText.text = score.ToString();
}