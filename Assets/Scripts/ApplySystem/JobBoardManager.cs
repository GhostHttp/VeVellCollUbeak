using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class JobBoardManager : MonoBehaviour
{
    [SerializeField] private VacancyData[] allVacancies;
    [SerializeField] private Transform vacanciesContainer;
    [SerializeField] private GameObject vacancyPrefab;
    [SerializeField] private float checkNewVacancyInterval = 10f;

    private List<VacancyData> availableVacancies = new List<VacancyData>();
    private List<VacancyData> activeVacancies = new List<VacancyData>();

    public delegate void VacancyResponseEvent(bool isSuccess, VacancyData vacancy);
    public event VacancyResponseEvent OnVacancyResponded;

    private void Start()
    {
        // Копируем все вакансии в доступные
        availableVacancies.AddRange(allVacancies);

        // Запускаем корутину для появления новых вакансий
        StartCoroutine(NewVacanciesRoutine());
    }

    private IEnumerator NewVacanciesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkNewVacancyInterval);

            if (availableVacancies.Count > 0 && Random.value > 0.3f) // 30% шанс появления новой вакансии
            {
                AddRandomVacancy();
            }
        }
    }

    private void AddRandomVacancy()
    {
        if (availableVacancies.Count == 0) return;

        int randomIndex = Random.Range(0, availableVacancies.Count);
        VacancyData vacancy = availableVacancies[randomIndex];
        // Создаем объект вакансии на доске
        GameObject vacancyObj = Instantiate(vacancyPrefab, vacanciesContainer);
        SetupVacancyUI(vacancyObj, vacancy);

        activeVacancies.Add(vacancy);
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

        // Настраиваем UI элементы
        vacancyUI.jobTitle.text = vacancy.jobTitle;
        vacancyUI.jobDescription.text = vacancy.jobDescription;
        vacancyUI.companyLogo.sprite = vacancy.companyLogo;

        // Настраиваем кнопку отклика
        vacancyUI.respondButton.onClick.RemoveAllListeners();
        vacancyUI.respondButton.onClick.AddListener(() => RespondToVacancy(vacancy, vacancyObj));
    }

    private void RespondToVacancy(VacancyData vacancy, GameObject vacancyObj)
    {
        // Удаляем вакансию с доски
        activeVacancies.Remove(vacancy);
        Destroy(vacancyObj);

        // Определяем успешность отклика
        bool isSuccess = Random.value <= vacancy.successChance;

        // Вызываем событие отклика
        OnVacancyResponded?.Invoke(isSuccess, vacancy);

        Debug.Log($"Responded to {vacancy.jobTitle}. Success: {isSuccess}");
    }

    public void ResetJobBoard()
    {
        // Очищаем доску и сбрасываем доступные вакансии
        foreach (Transform child in vacanciesContainer)
        {
            Destroy(child.gameObject);
        }

        availableVacancies.Clear();
        availableVacancies.AddRange(allVacancies);
        activeVacancies.Clear();
    }
}