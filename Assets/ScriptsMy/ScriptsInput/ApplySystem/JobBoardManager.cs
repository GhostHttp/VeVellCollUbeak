using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobBoardManager : MonoBehaviour
{
    [SerializeField] private VacancyData[] allVacancies;
    [SerializeField] private Transform vacanciesContainer;
    [SerializeField] private float checkNewVacancyInterval = 10f;
    [SerializeField] private GameObject JobBoardWindow;
    [SerializeField] private AudioSource getApplySound;
    [Space]

    private List<VacancyData> availableVacancies = new List<VacancyData>();
    public List<VacancyData> activeVacancies = new List<VacancyData>();

    public delegate void VacancyResponseEvent(bool isSuccess, VacancyData vacancy);
    public event Action<bool> OnVacancyResponded;
    public event Action<GameObject> OnVacancyCreated;
    public event Action OnMinigameCreated;

    private int AppysCounter;

    private void Start()
    {
        availableVacancies.AddRange(allVacancies);

        StartCoroutine(NewVacanciesRoutine());
    }

    public void StartNewVacanciesRoutine()
    {
        StartCoroutine(NewVacanciesRoutine());
    }

    private IEnumerator NewVacanciesRoutine()
    {
        while (JobBoardWindow.activeInHierarchy)
        {
            yield return new WaitForSeconds(checkNewVacancyInterval);

            if (availableVacancies.Count > 0 && UnityEngine.Random.value > 0.3f) // 30% шанс появления новой вакансии
            {
                AddRandomVacancy();
            }
        }
    }

    private void AddRandomVacancy()
    {
        if (availableVacancies.Count == 0) return;

        int randomIndex = UnityEngine.Random.Range(0, availableVacancies.Count);
        VacancyData vacancy = availableVacancies[randomIndex];
        GameObject vacancyObj = Instantiate(vacancy.vacancyPrefab, vacanciesContainer);
        SetupVacancyUI(vacancyObj, vacancy);

        activeVacancies.Add(vacancy);
        OnVacancyCreated?.Invoke(vacancyObj);
        //availableVacancies.RemoveAt(randomIndex);
    }

    private void SetupVacancyUI(GameObject vacancyObj, VacancyData vacancy)
    {
        VacancyUI vacancyUI = vacancyObj.GetComponent<VacancyUI>();
        if (vacancyUI == null)
        {
            Debug.LogError("Компонент VacancyUI не найден на префабе вакансии!");
            return;
        }

        vacancyUI.jobTitle.text = vacancy.jobTitle;
        vacancyUI.jobDescription.text = vacancy.jobDescription;
        vacancyUI.companyLogo.sprite = vacancy.companyLogo;

        vacancyUI.respondButton.onClick.RemoveAllListeners();
        vacancyUI.respondButton.onClick.AddListener(() => RespondToVacancy(vacancy, vacancyObj));
    }

    private void RespondToVacancy(VacancyData vacancy, GameObject vacancyObj)
    {
        activeVacancies.Remove(vacancy);
        // Destroy(vacancyObj);
        StartCoroutine(RespondAnimation(vacancyObj));
        AppysCounter++;
        bool isSuccess = UnityEngine.Random.value <= vacancy.successChance;
        if (isSuccess)
        {
            getApplySound.PlayOneShot(getApplySound.clip);
        }
        if (AppysCounter == 25 || AppysCounter == 50 || AppysCounter == 75 || AppysCounter == 100)
        {
            OnMinigameCreated?.Invoke();
        }

        OnVacancyResponded?.Invoke(isSuccess);

        Debug.Log($"Responded to {vacancy.jobTitle}. Success: {isSuccess}");
    }

    public void ResetJobBoard()
    {
        foreach (Transform child in vacanciesContainer)
        {
            Destroy(child.gameObject);
        }

        availableVacancies.Clear();
        availableVacancies.AddRange(allVacancies);
        activeVacancies.Clear();
    }

    private IEnumerator RespondAnimation(GameObject item)
    {
        var fadetime = 0.5f;
        item.transform.DOScale(0f, fadetime).SetEase(Ease.OutFlash);
        yield return new WaitForSeconds(fadetime);
        Destroy(item);
    }
}