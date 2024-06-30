using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour, IService
{
    [SerializeField] private Image _interactImage;

    private const float InteractImageAnimationTime = 0.5f;
    private TweenerCore<Vector3, Vector3, VectorOptions> _interactScaleAnimation;

    public void ShowInteractButton()
    {
        _interactImage.gameObject.SetActive(true);

        _interactImage.transform.localScale = Vector3.zero;
        
        if(_interactScaleAnimation != null)
        {
            _interactScaleAnimation.Kill();
        }

        _interactScaleAnimation = _interactImage.transform.DOScale(1, InteractImageAnimationTime);
    }

    public void HideInteractButton()
    {
        if (_interactScaleAnimation != null)
        {
            _interactScaleAnimation.Kill();
        }

        _interactScaleAnimation = _interactImage.transform.DOScale(0, InteractImageAnimationTime).OnComplete(() =>
        {
            _interactImage.gameObject.SetActive(false);
        });
    }
}
