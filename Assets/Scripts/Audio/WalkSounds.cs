using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSounds : AudioPlayer
{
    [SerializeField] private PlayerMovementYBot _playerMovementYBot;

    public floorType floor;
    [Space]
    public AudioClip emote1Clip;
    public AudioClip emote2Clip;
    public AudioClip slideClip;
    public AudioClip DeathClip;
    [Space]
    public AudioClip jumpDesertClip;
    public List<AudioClip> walkDesertClips;
    public List<AudioClip> runDesertClips;
    [Space]
    public AudioClip jumpMetalClip;
    public List<AudioClip> walkMetalClips;
    public List<AudioClip> runMetalClips;
    [Space]
    public AudioClip jumpGrassClip;
    public List<AudioClip> walkGrassClips;
    public List<AudioClip> runGrassClips;
    [Space]
    public AudioClip jumpWaterClip;
    public List<AudioClip> walkWaterClips;
    public List<AudioClip> runWaterClips;

    private void OnEnable()
    {
        base.OnEnable();
        FootCollisions.OnFootContact += PlayAudio;
        FootCollisions.OnFootContactRun += PlayAudioRun;
        _playerMovementYBot.OnJump += PlayJumpSound;
        _playerMovementYBot.OnEmote1 += PlayEmote1Sound;
        _playerMovementYBot.OnEmote2 += PlayEmote2Sound;
        _playerMovementYBot.OnSlide += PlaySlideSound;
        _playerMovementYBot.OnDeath += PlayDeathSound;
    }

    private void OnDisable()
    {
        FootCollisions.OnFootContact -= PlayAudio;
        FootCollisions.OnFootContactRun -= PlayAudioRun;
        _playerMovementYBot.OnJump -= PlayJumpSound;
        _playerMovementYBot.OnEmote1 -= PlayEmote1Sound;
        _playerMovementYBot.OnEmote2 -= PlayEmote2Sound;
        _playerMovementYBot.OnSlide -= PlaySlideSound;
        _playerMovementYBot.OnDeath -= PlayDeathSound;
    }

    private void Start()
    {
        audioClip = walkDesertClips[0];
    }

    private void Update()
    {
        if (FootCollisions.instance.GetGroundTag() == 0)
        {
            floor = floorType.Desert;
        }
        else if (FootCollisions.instance.GetGroundTag() == 1)
        {
            floor = floorType.Metal;
        }
        else if (FootCollisions.instance.GetGroundTag() == 2)
        {
            floor = floorType.Grass;
        }
        else if (FootCollisions.instance.GetGroundTag() == 3)
        {
            floor = floorType.Water;
        }
    }

    private void PlayJumpSound()
    {
        switch (floor)
        {
            case floorType.Desert:
                audioClip = jumpDesertClip;
                break;
            case floorType.Metal:
                audioClip = jumpMetalClip;
                break;
            case floorType.Grass:
                audioClip= jumpGrassClip;
                break;
            case floorType.Water:
                audioClip = jumpWaterClip;
                break;
        }
        audioSource.PlayOneShot(audioClip);
    }

    private void PlayEmote1Sound()
    {
        audioSource.PlayOneShot(emote1Clip);
    }

    private void PlayEmote2Sound()
    {
        audioSource.PlayOneShot(emote2Clip);
    }

    private void PlaySlideSound()
    {
        audioSource.PlayOneShot(slideClip);
    }

    private void PlayDeathSound()
    {
        audioSource.PlayOneShot(DeathClip);
    }

    private void PlayAudio()
    {
        switch (floor)
        {
            case floorType.Desert:
                audioClip = walkDesertClips[Random.Range(0, walkDesertClips.Count)];
                break;
            case floorType.Metal:
                audioClip = walkMetalClips[Random.Range(0, walkMetalClips.Count)];
                break;
            case floorType.Grass:
                audioClip = walkGrassClips[Random.Range(0, walkGrassClips.Count)];
                break;
            case floorType.Water:
                audioClip = walkWaterClips[Random.Range(0, walkWaterClips.Count)];
                break;
        }
        audioSource.PlayOneShot(audioClip);
    }

    private void PlayAudioRun()
    {
        switch (floor)
        {
            case floorType.Desert:
                audioClip = runDesertClips[Random.Range(0, runDesertClips.Count)];
                break;
            case floorType.Metal:
                audioClip = runMetalClips[Random.Range(0, runMetalClips.Count)];
                break;
            case floorType.Grass:
                audioClip = runGrassClips[Random.Range(0, runGrassClips.Count)];
                break;
            case floorType.Water:
                audioClip = runWaterClips[Random.Range(0, runWaterClips.Count)];
                break;
        }
        audioSource.PlayOneShot(audioClip);
    }

    public enum floorType
    {
        Desert,
        Metal,
        Grass,
        Water
    }
}
