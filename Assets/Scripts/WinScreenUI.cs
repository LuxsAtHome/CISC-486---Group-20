using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenUI : MonoBehaviour
{
    // Name of the scene you want to restart (your game level)
    [SerializeField] private string gameSceneName = "TestScene";
    [SerializeField] private string mainMenuSceneName = "MainMenu";  // optional

    private void Start()
    {
        // Unlock cursor in win screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }



    public void QuitGame()
    {
        // Only works in build, not in editor
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
