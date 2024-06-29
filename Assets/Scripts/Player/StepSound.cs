using UnityEngine;

public class StepSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _sounds;

    private SoundContainer _soundContainer;
    private SoundContainer SoundContainer
    {
        get
        {
            if (_soundContainer == null)
            {
                _soundContainer = ServiceLoacator.Instance.Get<SoundContainer>();
            }

            return _soundContainer;
        }
    }


    public void PlayStepSound()
    {
        if (_sounds.Length == 0)
        {
            return;
        }

        int rand = Random.Range(0, _sounds.Length);
        SoundContainer?.Play(_sounds[rand], conf =>
        {
            conf.loop = false;
            conf.volume = 0.1f;
        });
    }
}
