using UnityEngine;

public class TimerAnim : MonoBehaviour
{
    [SerializeField] private float time;
    private float _timeLeft = 0f;
    private bool _timerOn = false;

    private void Start()
    {
        _timeLeft = time;
    }

    private void Update()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
            }
            else
            {
                _timeLeft = time;
                _timerOn = false;
            }
        }
    }



}
