using UnityEngine;

public class Entity : MonoBehaviour
{
    private Vector2 target;
    private Vector2 _moveDirection /*{ get => _moveDirection; set => _moveDirection = value; }*/;
    private bool _isAttacking /*{ get => _isAttacking; set => _isAttacking = value; }*/;

    public States CurrentState = new States();


    public void Move(Vector2 direction, Rigidbody2D objectToMove, bool jump)
    {
        if (direction == Vector2.zero) return;

        _moveDirection = direction;
        MoveToTarget(_moveDirection, objectToMove, jump);
        //objectToMove.transform.position = (Vector2)objectToMove.transform.position + direction;
    }

    private void MoveToTarget(Vector2 targetPosition, Rigidbody2D objectToMove, bool jump)
    {
        if ((Vector2)objectToMove.transform.position != targetPosition)
        {
            Vector2 direction = (targetPosition - (Vector2)objectToMove.transform.position).normalized;
            if(jump)
                objectToMove.velocity = targetPosition.normalized * 7;
            else
                objectToMove.velocity = targetPosition.normalized * 3;
        }
    }


    public void Attack(Vector2 direction, Rigidbody2D objectToMove, GameObject objectToDestroy)
    {
        Move(direction, objectToMove, false);
        objectToDestroy.SetActive(false);
    }
}

public class States
{
    public float X;
    public float Y;
    public bool Attack;
}
