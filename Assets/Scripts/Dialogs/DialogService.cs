using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogService : MonoBehaviour, IService
{
    [SerializeField] private Animator _captainAnimator;
    [SerializeField] private Animator _cadetAnimator;

    private DialogState _dialogState;
    private AudioSource _audioSource;
    private Queue<IEnumerator> _actionQueue;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _actionQueue = new Queue<IEnumerator>();
    }

    private void Update()
    {
        if(_audioSource.isPlaying == false && _actionQueue.Count > 0)
        {
            var currentAction = _actionQueue.Dequeue();

            StartCoroutine(currentAction);
        }
    }

    public void CadetTalk(AudioClip audioClip, bool force = false)
    {
        if (force)
        {
            _actionQueue.Clear();
            _dialogState = DialogState.InterruptRequested;
        }

        _actionQueue.Enqueue(Talk(_cadetAnimator, audioClip));
    }

    public void CaptainTalk(AudioClip audioClip, bool force = false)
    {
        if (force)
        {
            _actionQueue.Clear();
            _dialogState = DialogState.InterruptRequested;
        }

        _actionQueue.Enqueue(Talk(_captainAnimator, audioClip));
    }

    private IEnumerator Talk(Animator animator, AudioClip audioClip)
    {
        animator.gameObject.SetActive(true);
        _dialogState = DialogState.Playing;
        _audioSource.clip = audioClip;
        _audioSource.Play();

        while (_audioSource.isPlaying)
        {
            if(_dialogState == DialogState.InterruptRequested)
            {
                _audioSource.Stop();
                break;
            }

            animator.SetBool(Constants.AnimationParams.Talk, true);
            yield return new WaitForFixedUpdate();
        }

        animator.SetBool(Constants.AnimationParams.Talk, false);
        animator.gameObject.SetActive(false);
        _dialogState =  DialogState.None;
    }
}

public enum DialogState
{
    None,
    Playing,
    InterruptRequested
}