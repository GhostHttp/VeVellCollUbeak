using UnityEngine;
using UnityEngine.UI;

public class SimpleOpenClose : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private Button _button;
    private bool _isActive = false;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeState);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ChangeState);
    }
    private void ChangeState()
    {
        _isActive = !_isActive;
        _gameObject.SetActive(_isActive);
    }
}
