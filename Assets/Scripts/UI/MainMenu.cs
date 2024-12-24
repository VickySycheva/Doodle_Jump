using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public ColorSelector currentToggle;

    [SerializeField] Canvas startScreen;
    [SerializeField] Button startButton;
    [SerializeField] TextMeshProUGUI bestScore;
    [SerializeField] List<ColorSelector> colorToggles;

    public void Init(Action onStart)
    {
        ActivateStartScreen(true);

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => onStart.Invoke());

        foreach (var color in colorToggles)
        {
            // color.Init();
        }
    }

    public void ActivateStartScreen (bool isActive)
    {
        startScreen.gameObject.SetActive (isActive);
        
        if(PlayerPrefs.HasKey("Best score"))
        {
            bestScore.text = "Best score: " + PlayerPrefs.GetInt("Best score");
        }
    }
}