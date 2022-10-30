using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    private bool _move;
    private bool _isright = true;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        player = GameObject.Find("Character");
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasePlayer();
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
            if (rb.velocity.x > 0 && _isright == false)
                Flip();
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
            if (rb.velocity.x < 0 && _isright == true)
                Flip();
        }
    }

    private void Flip()
    {
        _isright = !_isright;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void StopChasePlayer()
    {
        rb.velocity = new Vector2(0, 0);
    }

    public bool Speed()
    {
        if (moveSpeed > 0 || moveSpeed < 0)
            _move = true;
        else
            _move = false;
        return _move;
    }

    public bool FacingRight()
    {
        return _isright;
    }
}
