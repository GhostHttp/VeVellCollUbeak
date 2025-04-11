using UnityEngine;

[CreateAssetMenu(fileName = "New Vacancy", menuName = "Job System/Vacancy")]
public class VacancyData : ScriptableObject
{
    public string jobTitle;
    [TextArea(3, 5)] public string jobDescription;
    public Sprite companyLogo;
    [Range(0f, 1f)] public float successChance = 0.5f;
    public int minDelaySeconds = 5;
    public int maxDelaySeconds = 15;
}