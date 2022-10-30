using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _bull;
    [SerializeField] private float _timeAnim;
    [SerializeField] private float _speed = 0.02f;
    private float _timeLeft = 0;
    private bool _timerOn = true;
    private bool _isfire = false;
    private bool _isright = true;
    private GameObject _bullClone;

    private MovingEnemy _moving;
    void Start()
    {
        _moving = transform.GetComponent<MovingEnemy>();
        _timeLeft = _timeAnim;
    }

    void Update()
    {
        if (_timeLeft == _timeAnim && _moving.Speed() == true)
            StartCoroutine(StartTimer());

        Fire();
        if (_bullClone != null)
            Bullupdate();
    }

    private void Bullupdate()
    {
        if (_isright == true)
        {
            _bullClone.transform.position = new Vector2(_bullClone.transform.position.x + _speed, _bullClone.transform.position.y);
            _isright = true;
        }
        else
        {
            _bullClone.transform.position = new Vector2(_bullClone.transform.position.x - _speed, _bullClone.transform.position.y);
            _isright = false;
        }
        Destroy(_bullClone, 4f);
    }

    private void Fire()
    {
        if (_timerOn == false && _isfire == true)
        {
            _timeLeft = _timeAnim;
            _timerOn = true;
            _isfire = false;
            Destroy(_bullClone);

            if (_moving.FacingRight() == true)
                _isright = true;
            else if (_moving.FacingRight() == false)
                _isright = false;

            Vector2 direction = (_enemy.transform.position - transform.position);
            _bullClone = Instantiate(_bull, transform.position, Quaternion.LookRotation(direction));


            if (_isright)
            {
                Vector3 newScale = _bullClone.transform.localScale;
                newScale.x *= -1;
                _bullClone.transform.localScale = newScale;
            }
        }
    }

    private IEnumerator StartTimer()
    {
        _isfire = true;
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            yield return null;
        }
        _timerOn = false;
    }
}
