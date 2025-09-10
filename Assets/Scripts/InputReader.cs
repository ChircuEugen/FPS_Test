using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputReader : MonoBehaviour
{
    private PlayerInput playerInput;
    [HideInInspector] public InputAction moveInput;
    [HideInInspector] public InputAction jumpAction;
    [HideInInspector] public InputAction runAction;
    [HideInInspector] public InputAction lookAction;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveInput = playerInput.actions.FindAction("Movement");
        jumpAction = playerInput.actions.FindAction("Jump");
        runAction = playerInput.actions.FindAction("Run");
        lookAction = playerInput.actions.FindAction("Look");
    }

}
