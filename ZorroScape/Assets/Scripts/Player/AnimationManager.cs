using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    private int _currentState;

    public int CurrentState { get => _currentState; set => _currentState = value; }

    public Animator ObjectAnimator { get => _anim; }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

}
