using UnityEngine;
using DG.Tweening;

public class SimpleWindowFade : MonoBehaviour
{
    [SerializeField] protected float _fadeTime = 1f;
    [SerializeField] protected CanvasGroup _canvasGroup;
    [SerializeField] protected RectTransform _rectTransform;

    public void PanelFadeIn()
    {
        //_canvasGroup.alpha = 0f;
        //_rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        //_rectTransform.transform.localScale = new Vector3(0f, 0, 0f);
       //_rectTransform.DOAnchorPos(new Vector2(0f, 0f),_fadeTime,false).SetEase(Ease.OutElastic);

        //_rectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.OutSine);
        //_rectTransform.DOScale(new Vector3(1f,1f,1f), _fadeTime).SetEase(Ease.InFlash);
        //_canvasGroup.DOFade(1, _fadeTime);
    }

    public void PanelFadeOut()
    {
        //_canvasGroup.alpha = 1f;
        //_rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        // _rectTransform.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.InOutElastic);
        //_rectTransform.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.InOutSine);
        //_rectTransform.DOScale(new Vector3(0f, 0f, 0f), _fadeTime).SetEase(Ease.InOutFlash);
        //_canvasGroup.DOFade(0, _fadeTime);
    }

}
