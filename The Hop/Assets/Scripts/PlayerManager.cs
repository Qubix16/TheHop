using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    #region UI
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestHeightText;
    #endregion

    #region STATS
    private int bestHeight = 0;
    public delegate void OnCoinGrab();
    public static OnCoinGrab onCoinGrab;

    private int score = 0;
    public delegate void OnBestHeight(int newHeight);
    public static OnBestHeight onBestHeight;
    #endregion

    void Awake()
    {
        onBestHeight += SetBestHeight;
        onBestHeight += UpdateMaxBestHeight;
        onCoinGrab = AddToScore;
        GameController.onRestartGame += RestartStats;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreText();
        UpdateBestHeightText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    private void UpdateBestHeightText()
    {
        bestHeightText.text = bestHeight.ToString();
    }
    private void UpdateMaxBestHeight(int height)
    {
        int maxBestHieght = PlayerPrefs.GetInt("MaxBestHeight");
        if (height > maxBestHieght)
        {
            PlayerPrefs.SetInt("MaxBestHeight", height);
        }
    }
    private void AddToScore()
    {
        score++;
        PlayerPrefs.SetInt("Score", score);
    }

    private void SetBestHeight(int newHeight)
    {
        bestHeight = newHeight;
        PlayerPrefs.SetInt("BestHeight", bestHeight);
    }
    void RestartStats()
    {
        score = 0;
        PlayerPrefs.SetInt("Score", 0);
        bestHeight = 0;
        PlayerPrefs.SetInt("BestHeight", 0);
    }
    private void OnDisable()
    {
        onBestHeight -= SetBestHeight;
        onBestHeight -= UpdateMaxBestHeight;
        onCoinGrab -= AddToScore;
        GameController.onRestartGame -= RestartStats;
    }
}
