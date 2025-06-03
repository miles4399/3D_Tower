using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private GameObject _target;
    [SerializeField] private bool _autoPossess = true;

    private Camera _mainCamera;
    private CharacterMovement _characterMovement;
    private PlaceObject _placeObject;

    private Vector2 _moveInput;
    private bool _possessed = false;

    void Start()
    {
        _mainCamera = Camera.main;
        if (_autoPossess && _target != null) Possess(_target);
        if(_target.TryGetComponent(out PlaceObject placeObject))
        {
            _placeObject = placeObject;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} Get component failed");
        }
    }

    public void Possess(GameObject target)
    {
        if (_target.TryGetComponent(out CharacterMovement characterMovement))
        {
            _characterMovement = characterMovement;
            _possessed = true;
            Debug.Log($"{gameObject.name} possessed {_target.name}", _target);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} failed to possess {_target.name}", _target);
            _characterMovement = null;
            _possessed = false;
        }
    }
    public void Depossess()
    {
        _characterMovement = null;
        _possessed = false;
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        //Debug.Log($"Move input: {_moveInput}");
    }

    public void OnPlace(InputValue value)
    {
        _placeObject?.Place();
    }

    // Update is called once per frame
    void Update()
    {
        if (_possessed)
        {
            //Vector3 up = Vector3.up;
            //Vector3 right = _mainCamera.transform.right;
            //Vector3 forward = Vector3.Cross(right, up);
            //Vector3 moveInput = forward * _moveInput.y + right * _moveInput.x;
            
            Vector3 moveInput = new Vector3(_moveInput.x, 0f, _moveInput.y);
            _characterMovement.SetMoveInput(moveInput);
            _characterMovement.SetLookDirection(moveInput);

           
        }
    }
}
