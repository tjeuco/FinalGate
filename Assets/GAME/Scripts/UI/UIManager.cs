using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Canvas Main")]
    [SerializeField] private Canvas canvasMainMenu;
    [SerializeField] private Canvas canvasGameplay;
    [SerializeField] private Canvas canvasSettings;
    [SerializeField] private Canvas canvasGameOver;
    [SerializeField] private Canvas canvasGameWin;
    [SerializeField] private Canvas canvasHightScore;

    [SerializeField] public Canvas canvasHp;

    Dictionary<UIScreen, Canvas> screens;
    protected override void Awake()
    {
        base.Awake();
        screens = new Dictionary<UIScreen, Canvas>()
        { 
            {UIScreen.MainMenu, canvasMainMenu},
            {UIScreen.Gameplay, canvasGameplay},
            {UIScreen.Settings, canvasSettings},
            {UIScreen.GameOver, canvasGameOver},
            {UIScreen.GameWin, canvasGameWin },
            {UIScreen.HightScore, canvasHightScore}
        };
    }
    private void Start()
    {
        this.ShowOneCanvas(UIScreen.MainMenu);
    }
    public void ShowOneCanvas(UIScreen screen)
    {
        foreach (var item in screens)
        {
            if (item.Value != null)
            {
                item.Value.gameObject.SetActive(item.Key == screen);
            }
        }
        if (screen == UIScreen.Gameplay)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void OnOffSettingsPopup()
    {
        if (this.canvasSettings == null) return;
        if (!this.canvasSettings.gameObject.activeSelf)
        {
            Time.timeScale = 0f;
            this.canvasSettings.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            this.canvasSettings.gameObject.SetActive(false);
        }
    }

    public void OnOffCanvas(Canvas canvas, bool active) 
    {
        if (canvas == null) return;
        canvas.gameObject.SetActive(active);
    }

    public void PlayNow()
    {
        this.ShowOneCanvas(UIScreen.Gameplay);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        this.ShowOneCanvas(UIScreen.MainMenu);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    public void GameWin() 
    { 
        this.ShowOneCanvas(UIScreen.GameWin);
    }

}

public enum UIScreen
{
    None,
    MainMenu,
    Gameplay,
    Settings,
    GameOver,
    GameWin,
    HightScore
}