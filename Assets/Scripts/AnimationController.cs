using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private float _dampTime = 0.1f;

    private Animator _animator;
    private CharacterMovement _characterMovement;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _characterMovement = GetComponentInParent<CharacterMovement>();

    }

    // Update is called once per frame
    private void Update()
    {
        float speed = Mathf.Min(_characterMovement.MoveInput.magnitude, _characterMovement.Velocity.Flatten().magnitude / _characterMovement.MovementAttributes.Speed);
        _animator.SetFloat("Speed", speed, _dampTime, Time.deltaTime);
    }
}
