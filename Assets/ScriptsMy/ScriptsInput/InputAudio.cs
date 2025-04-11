using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class InputAudio : MonoBehaviour
{
    public Slider mySlider;
    public TextMeshProUGUI valueText;

    void Start()
    {
        if (mySlider == null)
        {
            mySlider = GetComponent<Slider>();
            if (mySlider == null)
            {
                Debug.LogError("Слайдер не найден!");
                enabled = false;
                return;
            }
        }

        mySlider.onValueChanged.AddListener(OnSliderValueChanged);

        if (valueText != null)
        {
            valueText.text = mySlider.value.ToString();
        }
    }

    public void OnSliderValueChanged(float value)
    {
        Debug.Log("Значение слайдера изменилось: " + value);

        AudioListener.volume = value;
        if (valueText != null)
        {
            float valueT = Mathf.Round(value*100);
            valueText.text = valueT.ToString();
        }
    }
}
