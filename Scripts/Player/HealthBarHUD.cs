using System.Collections;
using UnityEngine;

public class HealthBarHUD : MonoBehaviour
{
    [SerializeField] private float _hurt;
    [SerializeField] private float _healh;
    [SerializeField] private float _timeAnim;
    [SerializeField] private Animator _playerHit;
    [SerializeField] private bool _addHealh;
    private float _timeLeft = 0;
    private bool _hit = false;

    private void FixedUpdate()
    {
        _playerHit = GameObject.Find("Character").GetComponent<Animator>();
    }

    public void AddHealth()
    {
        PlayerStats.Instance.AddHealth();
    }

    public void DelHealth()
    {
        PlayerStats.Instance.DelHealth();
    }

    public void Heal(float _healh)
    {
            PlayerStats.Instance.Heal(_healh);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(PlayerStats.Instance.health > 0)
        {
            if (collision.gameObject.tag == "Player")
            {
                _timeLeft = _timeAnim;
                if (_addHealh == true)
                {
                    AddHealth();
                    Destroy(gameObject);
                }
                if (_healh > 0 && PlayerStats.Instance.health < PlayerStats.Instance.maxHealth)
                {
                    Heal(_healh);
                    Destroy(gameObject);
                }

                _hit = true;
                _playerHit.SetBool("Hit", _hit);
                StartCoroutine(StartTimer());
            }
        }
    }

    private IEnumerator StartTimer()
    {
        PlayerStats.Instance.TakeDamage(_hurt);
        GetComponent<BoxCollider2D>().enabled = false;
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            yield return null;
        }
        _hit = false;
        _playerHit.SetBool("Hit", _hit);
        _timeLeft = _timeAnim;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
