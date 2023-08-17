using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _bodyTransform;
    [SerializeField] private Rigidbody2D _rigidbody;


    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _turningRate = 30f;

    private Vector2 _privousMovementInput;
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        _inputReader.MoveEvent += HandleMove;
      
    }
    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;
    }
    private void Update()
    {
        if (!IsOwner) return;
        float zRotation = _privousMovementInput.x * -_turningRate * Time.deltaTime;
        _bodyTransform.Rotate(0,0,zRotation);

    }
    private void FixedUpdate()
    {
        if (!IsOwner) return;
        _rigidbody.velocity =   (Vector2) _bodyTransform.up * _privousMovementInput.y * _moveSpeed;
    }
    private void HandleMove(Vector2 movementInput)
    {
        _privousMovementInput = movementInput;
    }
}
