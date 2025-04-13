using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PaperObject", menuName = "PaperSO")]
public class PaperSO : ScriptableObject
{ 
    public EtypePaper etypePaper;
    public GameObject prefab;
}
    public enum EtypePaper
    {
        Bad = 0,
        Good = 1
    }
