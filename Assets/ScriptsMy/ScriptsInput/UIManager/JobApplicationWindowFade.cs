using DG.Tweening;
using UnityEngine;

public class JobApplicationWindowFade : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 1f;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private JobBoardManager _jobBoardManager;

    private void Start()
    {
        _jobBoardManager.OnVacancyCreated += ItemsAnimation;
    }

    public void PanelFadeIn()
    {
        _canvasGroup.alpha = 0f;
        _rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        _rectTransform.transform.localScale = new Vector3(0f, 0, 0f);
        _rectTransform.DOAnchorPos(new Vector2(0f, 0f),_fadeTime,false).SetEase(Ease.OutElastic);

        _rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutSine);
        _rectTransform.DOScale(new Vector3(1f, 1f, 1f), _fadeTime).SetEase(Ease.InFlash);
        _canvasGroup.DOFade(1, _fadeTime);
    }

    public void PanelFadeOut()
    {
        _canvasGroup.alpha = 1f;
        _rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        _rectTransform.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.InOutElastic);
        _rectTransform.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.InOutSine);
        _rectTransform.DOScale(new Vector3(0f, 0f, 0f), _fadeTime).SetEase(Ease.InOutFlash);
        _canvasGroup.DOFade(0, _fadeTime);
    }

    private void ItemsAnimation(GameObject item)
    {
        item.transform.localScale = Vector3.zero;
        item.transform.DOScale(1f, _fadeTime).SetEase(Ease.OutBounce);
    }
}
