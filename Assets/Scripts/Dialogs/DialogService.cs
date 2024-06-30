using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogService : OrderedMonobeh, IService
{
    [SerializeField] private Animator _captainAnimator;
    [SerializeField] private Animator _cadetAnimator;

    private DialogState _dialogState;
    private AudioSource _audioSource;
    private Queue<IEnumerator> _actionQueue;
    private float _elapsedTIme = 0;
    private float _timeOut = 0.5f;

    public bool IsPlaying => _audioSource.isPlaying || _actionQueue.Count > 0;

    public override void OrderedAwake()
    {
        _audioSource = GetComponent<AudioSource>();
        _actionQueue = new Queue<IEnumerator>();
    }

    private void FixedUpdate()
    {
        if (_audioSource.isPlaying == false && _actionQueue.Count > 0)
        {
            _elapsedTIme += Time.fixedDeltaTime;

            while (_elapsedTIme > _timeOut)
            {
                _elapsedTIme = 0;
                var currentAction = _actionQueue.Dequeue();

                StartCoroutine(currentAction);
            }
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
            if (_dialogState == DialogState.InterruptRequested)
            {
                _audioSource.Stop();
                break;
            }

            animator.SetBool(Constants.AnimationParams.Talk, true);
            yield return null;
        }

        animator.SetBool(Constants.AnimationParams.Talk, false);
        animator.gameObject.SetActive(false);
        _dialogState = DialogState.None;
    }
}

public enum DialogState
{
    None,
    Playing,
    InterruptRequested
}