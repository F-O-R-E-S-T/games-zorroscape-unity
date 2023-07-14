using System.Collections;
using UnityEngine;

public class Player : Entity
{
    private bool canJump = false;
    private Rigidbody2D _rb;
    private ApiData _previousData = new ApiData();
    public Requests _requests;
    public float Life = 100;
    [SerializeField] private AnimationManager _animationManager;
    [SerializeField] private GameObject _bulletPrefab;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            canJump = false;
        }
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.2f);
            _previousData = _requests.PreviousData;
            GenerateAction();
            Action();
        }
    }

    private void Update()
    {
        if(_rb.velocity.y != 0)
        {
            _animationManager.CurrentState = 3;
        }

        if(_rb.velocity.x > 0)
        {
            _animationManager.CurrentState = 1;
        }else if(_rb.velocity.x < 0)
        {
            _animationManager.CurrentState = 2;
        }
        else if (_rb.velocity.x == 0)
        {
            _animationManager.CurrentState = 0;
        }

        if(Life <= 0)
        {
            _animationManager.CurrentState = 5;
        }

        if(Time.timeScale == 0 && _animationManager.CurrentState != 4)
        {
            _animationManager.CurrentState = 4;
        }
    }

    private void Action()
    {
        Vector2 direction = new Vector2(_previousData.x, _previousData.y);

        if (!canJump)
        {
            direction.y = 0;
        }
        if (_previousData.jumping)
        {
            _animationManager.CurrentState = 3;
        }

        if(_previousData.attacking != 0)
            Move(direction, _rb, _previousData.jumping, _previousData.attacking, _bulletPrefab);
        else
            Move(direction, _rb, _previousData.jumping, _previousData.attacking);
    }

    private void GenerateAction()
    {
        CurrentState.X = Mathf.FloorToInt(Random.Range(-1, 1.1f));
        CurrentState.Y = Mathf.FloorToInt(Random.Range(0, 1.1f));
    }

}
