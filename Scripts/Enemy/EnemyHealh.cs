using System.Collections;
using UnityEngine;

public class EnemyHealh : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _boom;
    private GameObject _bullClone;
    [SerializeField] private float _healh;
    [SerializeField] private float Damage;
    [SerializeField] private float DamageBoom;
    [SerializeField] private float DamageAll;
    public int score;

    void OnTriggerEnter2D(Collider2D collision)
    {
        _player = GameObject.Find("Character");
        Vector2 direction = (_player.transform.position - transform.position);
        if (collision.gameObject.tag == "Damage")
        {
            var bul1 = GameObject.Find("DamageAmmo1(Clone)");
            var bul2 = GameObject.Find("BOOOM(Clone)");
            var bul3 = GameObject.Find("Spike(Clone)");
            if (bul1 != null)
            {
                Destroy(bul1);
                _healh -= Damage;
            }
            else if (bul2 != null)
            {
                Destroy(bul2);//взрыв
                _healh -= DamageBoom;
                _bullClone = Instantiate(_boom, new Vector2(transform.position.x, transform.position.y), Quaternion.LookRotation(direction));
                Destroy(_bullClone, 0.2f);
                _healh -= DamageBoom / 2;
            }
            else if (bul3 != null)
            {
                _healh -= DamageAll;
                Destroy(bul3, 2f);//проход насквозь
            }
        }
    }

    private void Update()
    {
        if (_healh <= 0)
        {
            score += 1;
            Destroy(gameObject);
        }
    }
}
