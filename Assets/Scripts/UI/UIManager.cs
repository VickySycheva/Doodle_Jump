using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("Start Screen"), Space]    
    [SerializeField] Canvas startScreen;
    [SerializeField] Button startButton;
    [SerializeField] TextMeshProUGUI bestScoreMainMenu;
    [SerializeField] List<ColorSelector> colorToggles;

    [Header("Game Screen"), Space]    
    [SerializeField] Canvas gameScreen;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] Button restartGameButton;
    [SerializeField] Button backGameButton;

    [Header("End Screen"), Space]
    [SerializeField] Canvas endScreen;
    [SerializeField] TextMeshProUGUI totalCountText;
    [SerializeField] Button restartButton;
    [SerializeField] Button backToMenuButton;
    [SerializeField] TextMeshProUGUI bestScoreEndMenu;

    public ColorSelector currentToggle;

    public void Init(Action onStart, Action onRestart, Action onBack)
    {
        ActivateStartScreen(true);

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => onStart.Invoke());

        // mainMenu.Init(onStart);

        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(() => onRestart.Invoke());

        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => onRestart.Invoke());

        backGameButton.onClick.RemoveAllListeners();
        backGameButton.onClick.AddListener(() => onBack.Invoke());

        backToMenuButton.onClick.RemoveAllListeners();
        backToMenuButton.onClick.AddListener(() => onBack.Invoke());

        ActivateEndScreen(false);
        ActivateGameScreen(false);

        foreach (var color in colorToggles)
        {
            color.Init(this);
        }
    }

    public void UpdateCountText(int count)
    {
        countText.text = "Count: " + count;
    }

    public void UpdateEndCountText (int count, int bestScore)
    {
        totalCountText.text = "Score: " + count;
        bestScoreEndMenu.text = "Best score: " + bestScore;
    }

    public void ActivateStartScreen (bool isActive)
    {
        startScreen.gameObject.SetActive (isActive);
        if(PlayerPrefs.HasKey("Best score"))
        {
            bestScoreMainMenu.text = "Best score: " + PlayerPrefs.GetInt("Best score");
        }
    }

    public void ActivateGameScreen (bool isActive)
    {
        gameScreen.gameObject.SetActive (isActive);
    }

    public void ActivateEndScreen(bool isActive)
    {
        endScreen.gameObject.SetActive (isActive);
    }

    void BackToMenu()
    {
        ActivateGameScreen(false);
        ActivateEndScreen(false);
        ActivateStartScreen(true);
        // mainMenu.ActivateStartScreen(true);
    }
}
