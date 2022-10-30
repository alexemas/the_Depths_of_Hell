using System.Collections;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float groundRadius;
    [SerializeField] private float jump;
    [SerializeField] private GameObject player;
    private bool isFacingRight = true;
    private bool isGrounded = false;
    private bool _doublejump = false;
    private float _timeLeft;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    private Rigidbody2D _rb;
    private Animator anim;

	private void Start()
    {
        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", isGrounded);
        anim.SetFloat("vSpeed", _rb.velocity.y);
        if (!isGrounded)
            return;

        float move = Input.GetAxis("Horizontal");

        if(move > 0)
            anim.SetFloat("Speed", move);
        else
            anim.SetFloat("Speed", -move);

        _rb.velocity = new Vector2(move * maxSpeed, _rb.velocity.y);

        if (move > 0 && !isFacingRight)
            Flip();
        else if (move < 0 && isFacingRight)
            Flip();
    }

    private void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            _rb.AddForce(new Vector2(0, jump));
            _doublejump = true;
        }

        if(!isGrounded && _doublejump && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jump / 180);
            _doublejump = false;
        }

        if (!isGrounded)
        {
            StartCoroutine(StartTimer());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private IEnumerator StartTimer()
    {
        _timeLeft = 20;
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            yield return null;
        }
        player.transform.position = new Vector2(player.transform.position.x + 2, player.transform.position.y + 1);
        StopAllCoroutines();
    }

    public bool FacingRight()
    {
        return isFacingRight;
    }

    public bool Grounded()
    {
        return isGrounded;
    }
}
