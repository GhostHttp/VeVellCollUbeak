using UnityEngine;
using UnityEngine.Audio;

public class ClickSound : MonoBehaviour
{
    public AudioSource audioSource;
    [Tooltip("Minimum possible pitch value")]
    [SerializeField] private float minPitch = 0.9f;
    [Tooltip("Maximum possible pitch value")]
    [SerializeField] private float maxPitch = 1.1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayWithRandomPitch();
        }
    }

    private void PlayWithRandomPitch()
    {
        if (audioSource.clip != null)
        {
            // Устанавливаем случайный pitch в заданном диапазоне
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();
        }
    }

    // Альтернативная версия для клика по объекту
    private void OnMouseDown()
    {
        PlayWithRandomPitch();
    }
}
