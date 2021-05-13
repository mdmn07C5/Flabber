using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource mainAudio;
    [SerializeField] AudioSource secondaryAudio;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip score;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip glide;
    [SerializeField] AudioClip hit;

    void Awake()
    {
        GameManager.OnJumpKeyPressed.AddListener(HandleOnJumpKeyPressed);
        Kill.OnPlayerCollision.AddListener(HandleOnPlayerCollision);
        ScoreTrigger.OnPlayerClear.AddListener(HandleOnPlayerClear);
    }

    void HandleOnJumpKeyPressed()
    {
        StartCoroutine(PlayMultipleClips(new AudioClip[] {jump, glide}));
    }

    void HandleOnPlayerCollision()
    {
        StopCoroutine("PlayMultipleClips");
        StartCoroutine(PlayMultipleClips(new AudioClip[] {hit, death}));
    }

    void HandleOnPlayerClear(int _)
    {
        secondaryAudio.clip = score;
        secondaryAudio.Play();
    }

    IEnumerator PlayFullClip(AudioClip audioClip)
    {
        mainAudio.clip = audioClip;
        mainAudio.Play();
        yield return new WaitForSecondsRealtime(audioClip.length);
    }

    IEnumerator PlayMultipleClips(AudioClip[] clips)
    {
        foreach (AudioClip audioClip in clips)
        {
            yield return StartCoroutine(PlayFullClip(audioClip));
        }
    }
}
