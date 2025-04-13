using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaperScripts : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabGoodPaper;
    [SerializeField] 
    private GameObject _prefabBadPaper;
    [SerializeField]
    private ECutterType _etype;

    public enum ECutterType
    {
        red,
        green,
    }

    public event Action<int> OnScoreChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PaperScripts>() == null)
        {
            if (_etype == ECutterType.red)
            {
                if (collision.tag == "BadPaper")
                {
                    OnScoreChanged?.Invoke(1);
                    Destroy(collision.gameObject);
                }
                else if (collision.tag == "GoodPaper")
                {
                    OnScoreChanged?.Invoke(-1);
                    Destroy(collision.gameObject);
                }
            }
            else 
            {
                if (collision.tag == "BadPaper")
                {
                    OnScoreChanged?.Invoke(-1);
                    Destroy(collision.gameObject);
                }
                else if (collision.tag == "GoodPaper")
                {
                    OnScoreChanged?.Invoke(1);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
