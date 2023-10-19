using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class in charge of playet animation using blend trees
/// </summary>
[RequireComponent(typeof(Animator)),
 RequireComponent(typeof(PlayerMovementAlien)),
 DisallowMultipleComponent]
public class BlendTreeAnimationAlien : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovementAlien _playerMovementAlien;

    // Hashes the animator parameters for performance
    private static readonly int _ForwardWalkSpeed = Animator.StringToHash("ForwardWalkSpeed");
    private static readonly int _StrafeSpeed = Animator.StringToHash("StrafeSpeed");

    private void OnEnable()
    {
        _playerMovementAlien = GetComponent<PlayerMovementAlien>();
        _animator = GetComponent<Animator>();

        //Subscribes to the events from the PlayerMovementBlendTree2DSimple script
        _playerMovementAlien.OnJump += Jump;
        _playerMovementAlien.OnDance += Dance;
        _playerMovementAlien.OnDeath += Death;
    }

    private void OnDisable()
    {
        //Unsubscribes from the events from the PlayerMovementBlendTree2DSimple script
        _playerMovementAlien.OnJump -= Jump;
        _playerMovementAlien.OnDance -= Dance;
        _playerMovementAlien.OnDeath -= Death;
    }

    // Update is called once per frame
    void Update()
    {
        //Plays forward or backward animation based on the player's movement
        _animator.SetFloat(_ForwardWalkSpeed, _playerMovementAlien.WalkSpeed);
        _animator.SetFloat(_StrafeSpeed, _playerMovementAlien.StrafeSpeed);
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

    private void Death()
    {
        //Plays the death animation
        _animator.SetTrigger("Die");
    }
}
