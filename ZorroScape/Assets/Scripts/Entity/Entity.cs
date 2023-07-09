using UnityEngine;

public class Entity : MonoBehaviour
{
    private Vector2 _moveDirection /*{ get => _moveDirection; set => _moveDirection = value; }*/;
    private bool _isAttacking /*{ get => _isAttacking; set => _isAttacking = value; }*/;

    public States CurrentState = new States();


    public void Move(Vector2 direction, GameObject objectToMove)
    {
        _moveDirection = direction;
        objectToMove.transform.position = (Vector2)objectToMove.transform.position + direction;
    }


    public void Attack(Vector2 direction, GameObject objectToMove, GameObject objectToDestroy)
    {
        Move(direction, objectToMove);
        objectToDestroy.SetActive(false);
    }
}

public class States
{
    public float X;
    public float Y;
    public bool Attack;
}
