using System.Collections;
using UnityEngine;

public class JumpEnemy : MonoBehaviour
{
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float _timeAnim;
    private float _timeLeft = 0f;
    private bool isGrounded = false;

    private Rigidbody2D _rb;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _timeLeft = _timeAnim;
    }

    private void FixedUpdate()
    {
        if (_timeLeft == _timeAnim && isGrounded == true && ((_rb.velocity.x > 0) || (_rb.velocity.x < 0)))
            StartCoroutine(StartTimer());

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", isGrounded);

        if (!isGrounded)
            return;

        if(_rb.velocity.x > 0)
            anim.SetFloat("Speed", _rb.velocity.x);
        else
            anim.SetFloat("Speed", -_rb.velocity.x);
        
    }

    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            yield return null;
        }
        _rb.AddForce(new Vector2(0, 1500));
        _timeLeft = _timeAnim;
    }
}
