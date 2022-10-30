using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDeathSetting : MonoBehaviour
{
    [SerializeField] private GameObject MenuDeath;
    [SerializeField] private GameObject dmgArea;
    private GameObject Player;

    private void Update()
    {
        Player = GameObject.Find("Character");

        if (Player.transform.position.y <= -60)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void NewGame()
    {
        MenuDeath.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Revive()
    {
        //money -= 100;
        PlayerStats.Instance.health = PlayerStats.Instance.maxHealth;
        MenuDeath.SetActive(false);
        Vector2 direction = (Player.transform.position - transform.position);
        GameObject dmg = Instantiate(dmgArea, new Vector2(Player.transform.position.x - 0.1f, Player.transform.position.y), Quaternion.LookRotation(direction));
        Destroy(dmg, 0.5f);
        Time.timeScale = 1;
    }
}
