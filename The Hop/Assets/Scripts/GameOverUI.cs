using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    #region UI
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestHeightText;
    [SerializeField] TextMeshProUGUI maxBestHeightText;
    #endregion

    void Awake()
    {
        scoreText.text += PlayerPrefs.GetInt("Score").ToString();
        bestHeightText.text += PlayerPrefs.GetInt("BestHeight").ToString();
        maxBestHeightText.text += PlayerPrefs.GetInt("MaxBestHeight").ToString();
    }


}
