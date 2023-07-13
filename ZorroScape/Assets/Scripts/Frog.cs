using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private bool _inArea;
    private Player _player;
    private bool isAttacking = false;
    public float _attackDelay = 2f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(collision.collider.TryGetComponent<Player>(out Player player) && !isAttacking)
            {
                _inArea = true;
                _player = player;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.TryGetComponent<Player>(out Player player) && !isAttacking)
            {
                _inArea = false;
                _player = null;
            }
        }
    }

    private void Start()
    {
        StartCoroutine(ReduceLifeByTime());
    }

    private IEnumerator ReduceLifeByTime()
    {
        if (!_inArea || _player == null) {
            yield return new WaitForSeconds(_attackDelay);
            StartCoroutine(ReduceLifeByTime());
        }

        
        if (_inArea)
        {
            isAttacking = true;
            if (_player.Life > 0)
            {
                _player.Life -= 10;
            }
            yield return new WaitForSeconds(_attackDelay);
            
            isAttacking = false;
            StartCoroutine(ReduceLifeByTime());
        }
        
    }
}
