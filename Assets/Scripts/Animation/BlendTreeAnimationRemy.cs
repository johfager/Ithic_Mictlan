using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator)),
 RequireComponent(typeof(PlayerMovementRemy)),
 DisallowMultipleComponent]
public class BlendTreeAnimationRemy : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovementRemy _playerMovementRemy;

    // Hashes the animator parameters for performance
    private static readonly int _ForwardWalkSpeed = Animator.StringToHash("ForwardWalkSpeed");
    private static readonly int _StrafeSpeed = Animator.StringToHash("StrafeSpeed");

    private void OnEnable()
    {
        _playerMovementRemy = GetComponent<PlayerMovementRemy>();
        _animator = GetComponent<Animator>();

        //Subscribes to the events from the PlayerMovementBlendTree2DSimple script
        _playerMovementRemy.OnJump += Jump;
        _playerMovementRemy.OnDance += Dance;
        _playerMovementRemy.OnLaugh += Laugh;
    }

    private void OnDisable()
    {
        //Unsubscribes from the events from the PlayerMovementBlendTree2DSimple script
        _playerMovementRemy.OnJump -= Jump;
        _playerMovementRemy.OnDance -= Dance;
        _playerMovementRemy.OnLaugh -= Laugh;
    }

    // Update is called once per frame
    void Update()
    {
        //Plays forward or backward animation based on the player's movement
        _animator.SetFloat(_ForwardWalkSpeed, _playerMovementRemy.WalkSpeed);
        _animator.SetFloat(_StrafeSpeed, _playerMovementRemy.StrafeSpeed);
    }

    private void Jump()
    {
        //Plays the jump animation
        _animator.SetTrigger("Jump");
    }
    private void Dance()
    {
        //Plays the dance animation
        _animator.SetTrigger("Dance");
    }
    private void Laugh()
    {
        //Plays the laugh animation
        _animator.SetTrigger("Laugh");
    }
}
