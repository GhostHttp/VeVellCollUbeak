using TMPro;
using UnityEngine;

public class MinigameTryView : MonoBehaviour
{
    [SerializeField] private JobBoardManager JobBoardManager;
    [SerializeField] private TMP_Text _callsText;
    [SerializeField] private GameObject _CallsObject;
    [SerializeField] private int counter = 0;

    private void Awake()
    {
        DisableAlerts();
        JobBoardManager.OnMinigameCreated += AddCall;
    }

    private void Update()
    {
        if (counter <= 0)
        {
            DisableAlerts();
        }
    }

    private void AddCall()
    {
        EnableAlerts();
        counter++;
        _callsText.text = counter.ToString();
        if (counter <= 0)
        {
            DisableAlerts();
        }
    }

    public void RemoveCall()
    {
        if (counter > 0)
        {
            counter--;
            _callsText.text = counter.ToString();
        }
        else
        {
            DisableAlerts();
        }
    }

    private void DisableAlerts()
    {
        _callsText.gameObject.SetActive(false);
        _CallsObject.gameObject.SetActive(false);
    }

    private void EnableAlerts()
    {
        _callsText.gameObject.SetActive(true);
        _CallsObject.gameObject.SetActive(true);
    }
}
