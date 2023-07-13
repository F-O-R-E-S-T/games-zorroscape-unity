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
        //yield return new WaitUntil();
        while (true)
        {
            yield return new WaitForSeconds(1.2f);
            _previousData = _requests.PreviousData;
            GenerateAction();
            Action();
        }
    }

    private void Action()
    {
        Vector2 direction = new Vector2(_previousData.x, _previousData.y);
        //if (direction.x == direction.y && direction.x == 0)
        //    direction.x = 1;
        //    direction.x = 1;
        if (!canJump)
        {
            //_previousData.jumping = false;
            direction.y = 0;
        }
        if (_previousData.jumping)
        {
            _animationManager.CurrentState = 3;
        }
            
        Move(direction, _rb, _previousData.jumping);
    }

    private void GenerateAction()
    {
        CurrentState.X = Mathf.FloorToInt(Random.Range(-1, 1.1f));
        CurrentState.Y = Mathf.FloorToInt(Random.Range(0, 1.1f));
        //CurrentState.Attack = ;
    }

}
