using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttackController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bull;
    [SerializeField] private GameObject _bOOM;
    [SerializeField] private GameObject _spike;
    [SerializeField] private float _speed = 0.02f;
    [SerializeField] private float _timeAnim;
    [SerializeField] private Image ManaImage;
    private float _timeLeft = 0f;
    [SerializeField] private float _maxMana = 100;
    private float _manacost;
    private float _manalost;
    private bool _isright = true;
    private bool _isfire = false;
    private bool _timerOn = true;
    private bool _booom = false;
    private bool _allHit = false;
    private bool _standart = false;
    public GameObject _bullClone;

    private Moving _moving;
    private Animator anim;

    private void Start()
    {
        _moving = transform.GetComponent<Moving>();
        anim = GetComponent<Animator>();
        _timeLeft = _timeAnim;
        _manalost = _maxMana;
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && (_moving.Grounded() == true) && _timeLeft == _timeAnim && _manalost >= 10)
        {
            _standart = true;
            _manacost = 10.0f;
            StartCoroutine(StartTimer());
        }
        if (Input.GetKeyDown(KeyCode.K) && (_moving.Grounded() == true) && _timeLeft == _timeAnim && _manalost >= 50)
        {
            _booom = true;
            _manacost = 50.0f;
            StartCoroutine(StartTimer());
        }
        if (Input.GetKeyDown(KeyCode.L) && (_moving.Grounded() == true) && _timeLeft == _timeAnim && _manalost >= 10)
        {
            _allHit = true;
            _manacost = 15.0f;
            StartCoroutine(StartTimer());
        }

        Fire();
        if(_bullClone != null)
            Bullupdate();
        anim.SetBool("isfire", _isfire);

        if(_manalost < _maxMana)
        {
            _manalost += 0.13f;
            var normalizedValue = Mathf.Clamp(_manalost / _maxMana, 0.0f, 1.0f);
            ManaImage.fillAmount = normalizedValue;
        }
    }

    void Bullupdate()
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
            _timerOn = true;
            _isfire = false;
            _timeLeft = _timeAnim;
            Destroy(_bullClone);

            if (_moving.FacingRight() == true)
                _isright = true;
            else if (_moving.FacingRight() == false)
                _isright = false;

            Vector2 direction = (_player.transform.position - transform.position);

            if(_standart == true)
            {
                _bullClone = Instantiate(_bull, transform.position, Quaternion.LookRotation(direction));
                _bullClone.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            }
            if(_booom == true)
            {
                _bullClone = Instantiate(_bOOM, transform.position, Quaternion.LookRotation(direction));
            }
            if (_allHit == true)
            {
                _bullClone = Instantiate(_spike, transform.position, Quaternion.LookRotation(direction));
            }

            if (_isright)
            {
                Vector3 newScale = _bullClone.transform.localScale;
                newScale.x *= -1;
                _bullClone.transform.localScale = newScale;
            }

            _standart = false;
            _booom = false;
            _allHit = false;

            _manalost -= _manacost;
            var normalizedValue = Mathf.Clamp(_manalost / _maxMana, 0.0f, 1.0f);
            ManaImage.fillAmount = normalizedValue;
        }

    }
}
