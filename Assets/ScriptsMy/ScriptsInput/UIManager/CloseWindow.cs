using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CloseWindow : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _timeToClose;

    private Button _closeButton;

    private void Awake()
    {
        _closeButton = gameObject.GetComponent<Button>();
        _closeButton.onClick.AddListener(CloseWithDelay);
    }
    private IEnumerator BreakBeforeClosing()
    {
        yield return new WaitForSeconds(_timeToClose);
        _gameObject.SetActive(false);
    }

    public void CloseWithDelay()
    {
        StartCoroutine(BreakBeforeClosing());
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(CloseWithDelay);
    }
}
