using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState
{
    GS_GAME,
    GS_GAME_OVER
}
public class GameController : MonoBehaviour
{
    #region ON GAME OVER 
    public delegate void OnGameOver();
    public static event OnGameOver onGameOver;
    #endregion

    #region ON RESET GAME
    public delegate void OnRestartGame();
    public static event OnRestartGame onRestartGame;
    #endregion    

    public GameObject gameOverScreen;
    private GameState gameState;
    public static GameController gameController;
    private float delayOnEnablingGameOverScreen = 2f;

    void Awake()
    {
        gameState = GameState.GS_GAME;
        gameController = this;
        DisableGameOverScreen();
        onGameOver += EnableGameOverScreen;
        onRestartGame += DisableGameOverScreen;
        onRestartGame += LoadStartScene;
    }
    public void GameOver()
    {
        SetGameState(GameState.GS_GAME_OVER);
    }


    private void SetGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.GS_GAME_OVER:
                gameState = GameState.GS_GAME_OVER;
                onGameOver?.Invoke();
                break;
            case GameState.GS_GAME:
                gameState = GameState.GS_GAME;
                break;
        }

    }
    public GameState GetGameState()
    {
        return gameState;
    }

    void EnableGameOverScreen()
    {
        StartCoroutine(DelayedEnablingGameOverScreen());
    }

    void DisableGameOverScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void onClickRestart()
    {
        onRestartGame?.Invoke();
        DisableGameOverScreen();
        LoadStartScene();
    }

    void LoadStartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }

    private void OnDisable()
    {
        onGameOver -= EnableGameOverScreen;
        onRestartGame -= DisableGameOverScreen;
        onRestartGame -= LoadStartScene;
    }

    IEnumerator DelayedEnablingGameOverScreen()
    {
        yield return new WaitForSeconds(delayOnEnablingGameOverScreen);
        gameOverScreen.SetActive(true);

    }


}
