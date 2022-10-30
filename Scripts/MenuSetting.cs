using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSetting : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    private bool _menuOn = false;

    private void Update()
    {
        if(PlayerStats.Instance.health > 0) 
        { 
            if (!_menuOn)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Time.timeScale = 0;
                    Menu.SetActive(true);
                    _menuOn = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Time.timeScale = 1;
                    Menu.SetActive(false);
                    _menuOn = false;
                }
            }
        }
    }

    public void Continue()
    {
        Menu.SetActive(false);
        _menuOn = false;
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
        Time.timeScale = 1;
    }
}
