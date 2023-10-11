using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class in charge of playet animation using blend trees
/// </summary>
[RequireComponent(typeof(Animator)),
 RequireComponent(typeof(PlayerMovementYBot)),
 DisallowMultipleComponent]
public class BlendTreeAnimations2DFreeform : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovementYBot _playerMovementYBot;

    // Hashes the animator parameters for performance
    private static readonly int _ForwardWalkSpeed = Animator.StringToHash("ForwardWalkSpeed");
    private static readonly int _StrafeSpeed = Animator.StringToHash("StrafeSpeed");

    private void OnEnable()
    {
        _playerMovementYBot = GetComponent<PlayerMovementYBot>();
        _animator = GetComponent<Animator>();

        //Subscribes to the events from the PlayerMovementBlendTree2DSimple script
        _playerMovementYBot.OnJump += Jump;
        _playerMovementYBot.OnEmote1 += Emote1;
        _playerMovementYBot.OnEmote2 += Emote2;
        _playerMovementYBot.OnSlide += Slide;
        _playerMovementYBot.OnDeath += Death;
    }

    private void OnDisable()
    {
        //Unsubscribes from the events from the PlayerMovementBlendTree2DSimple script
        _playerMovementYBot.OnJump -= Jump;
        _playerMovementYBot.OnEmote1 -= Emote1;
        _playerMovementYBot.OnEmote2 -= Emote2;
        _playerMovementYBot.OnSlide -= Slide;
        _playerMovementYBot.OnDeath -= Death;
    }

    // Update is called once per frame
    void Update()
    {
        //Plays forward or backward animation based on the player's movement
        _animator.SetFloat(_ForwardWalkSpeed, _playerMovementYBot.WalkSpeed);
        _animator.SetFloat(_StrafeSpeed, _playerMovementYBot.StrafeSpeed);
    }

    private void Jump()
    {
        //Plays the jump animation
        _animator.SetTrigger("Jump");
    }

    public void Emote1()
    {
        //Plays the emote 1 animation
        _animator.SetTrigger("Emote1");
    }

    public void Emote2()
    {
        //Plays the emote 2 animation
        _animator.SetTrigger("Emote2");
    }

    public void Slide()
    {
        //Plays the slide animation
        _animator.SetTrigger("Slide");
    }

    public void Death()
    {
        //Plays the death animation
        _animator.SetTrigger("Death");
    }
}
