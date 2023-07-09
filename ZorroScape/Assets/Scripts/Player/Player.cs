using System.Collections;
using UnityEngine;

public class Player : Entity
{
    
    private IEnumerator Start()
    {
        //yield return new WaitUntil();
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GenerateAction();
            Action();
        }
    }

    private void Action()
    {
        Vector2 direction = new Vector2(CurrentState.X, CurrentState.Y);
        Move(direction, gameObject);
    }

    private void GenerateAction()
    {
        CurrentState.X = Mathf.FloorToInt(Random.Range(-1, 1.1f));
        CurrentState.Y = Mathf.FloorToInt(Random.Range(0, 1.1f));
        //CurrentState.Attack = ;
    }

}
