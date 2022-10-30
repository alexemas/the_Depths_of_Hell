using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlManager : MonoBehaviour
{
    public int LevelUnlock;
    public Button[] buttons;

    void Start()
    {
        LevelUnlock = PlayerPrefs.GetInt("levels", 1);

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].interactable = false;

        for (int i = 0; i < LevelUnlock; i++)
            buttons[i].interactable = true;

    }

    public void LoadLevel(string levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
}
