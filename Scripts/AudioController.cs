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
        GameManager.OnPlayerJump.AddListener(HandleOnPlayerJump);
        // Kill.OnPlayerCollision.AddListener(HandleOnPlayerCollision);
        GameManager.OnPlayerKill.AddListener(HandleOnPlayerKill);
        GameManager.OnPlayerScore.AddListener(HandleOnPlayerScore);
        // ScoreTrigger.OnPlayerClear.AddListener(HandleOnPlayerClear);
    }

    void HandleOnPlayerJump()
    {
        StartCoroutine(PlayMultipleClips(new AudioClip[] {jump, glide}));
    }

    void HandleOnPlayerKill()
    {
        StopCoroutine("PlayMultipleClips");
        StartCoroutine(PlayMultipleClips(new AudioClip[] {hit, death}));
    }

    void HandleOnPlayerScore(int _)
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
