using System.Collections;
using UnityEngine;

public class Player : Entity
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Start()
    {
        //yield return new WaitUntil();
        while (true)
        {
            yield return new WaitForSeconds(1.2f);
            GenerateAction();
            Action();
        }
    }

    private void Action()
    {
        Vector2 direction = new Vector2(CurrentState.X, CurrentState.Y);
        if (direction.x == direction.y && direction.x == 0)
            direction.x = 1;

        Move(direction, _rb);
    }

    private void GenerateAction()
    {
        CurrentState.X = Mathf.FloorToInt(Random.Range(-1, 1.1f));
        CurrentState.Y = Mathf.FloorToInt(Random.Range(0, 1.1f));
        //CurrentState.Attack = ;
    }

}
