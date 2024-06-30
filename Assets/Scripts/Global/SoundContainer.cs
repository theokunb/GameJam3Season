using System;
using System.Linq;
using UnityEngine;

public class SoundContainer : OrderedMonobeh, IService
{
    private AudioSource[] _audiSources;

    public override void OrderedAwake()
    {
        _audiSources = GetComponentsInChildren<AudioSource>();
    }

    private void Start()
    {
        var mainTheme = Resources.Load(Constants.Sounds.MiamiNight) as AudioClip;

        Play(mainTheme, conf =>
        {
            conf.loop = true;
            conf.volume = 0.04f;
        });
    }

    public void Play(AudioClip clip, Action<AudioSource> configureAudiSource = null)
    {
        var freeAudioSource = _audiSources.Where(element => element.isPlaying == false).FirstOrDefault();

        if (freeAudioSource == null)
        {
            return;
        }

        configureAudiSource?.Invoke(freeAudioSource);

        freeAudioSource.clip = clip;
        freeAudioSource.Play();
    }
}
