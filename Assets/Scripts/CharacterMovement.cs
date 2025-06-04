using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;



public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private MovementAttributes _movementAttributes;
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _raycastTransform;


    public MovementAttributes MovementAttributes => _movementAttributes;
    public Vector3 GroundNormal { get; private set; } = Vector3.up;
    public Vector3 MoveInput { get; private set; }
    public float ForcedMovement { get; set; } = 0f;
    public Vector3 LocalMoveInput { get; private set; }
    public Vector3 LookDirection { get; private set; }
    public float MoveSpeedMultiplier { get; set; } = 1f;
    public float TurnSpeedMultiplier { get; set; } = 1f;
    public bool HasMoveInput => MoveInput.magnitude > 0.2f;
    public Vector2 RawMoveInput;

    private Rigidbody _rigidbody;
    private bool _didMoveInput = false;

    public Vector3 Velocity
    {
        get => _rigidbody.velocity;
        private set => _rigidbody.velocity = value;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.useGravity = false;

        LookDirection = transform.forward;

    }

    public void SetMoveInput(Vector3 input)
    {
        Vector3 flattend = input.Flatten();
        MoveInput = Vector3.ClampMagnitude(flattend, 1f);
        LocalMoveInput = transform.InverseTransformDirection(MoveInput);
    }

    public void SetLookDirection(Vector3 direction)
    {
        if (direction.magnitude < 0.05f) return;
        LookDirection = direction.Flatten().normalized;
    }


    private void FixedUpdate()
    {
        Vector3 input = MoveInput;
        if (ForcedMovement > 0f) input = transform.forward * ForcedMovement;
        Vector3 right = Vector3.Cross(transform.up, input);
        Vector3 forward = Vector3.Cross(right, GroundNormal);
        Vector3 targetVelocity = forward * (MovementAttributes.Speed * MoveSpeedMultiplier);
        Vector3 velocityDiff = targetVelocity.Flatten() - Velocity.Flatten();
        Vector3 acceleration = velocityDiff * (MovementAttributes.Acceleration);
        _rigidbody.AddForce(acceleration);

        Quaternion targetRotation = Quaternion.LookRotation(LookDirection);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * MovementAttributes.TurnSpped * TurnSpeedMultiplier);
        _rigidbody.MoveRotation(rotation);

      
    }

    private void OnDrawGizmosSelected()
    {
        float lineLength = 10f;
        Gizmos.color = Color.red;
        Vector3 start = _raycastTransform.position;
        Vector3 end = start + transform.forward * lineLength;
        
        Gizmos.DrawLine(start, end);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
