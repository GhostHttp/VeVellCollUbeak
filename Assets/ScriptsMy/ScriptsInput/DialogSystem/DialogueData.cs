using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    public class DialogueNode
    {
        [TextArea(3, 10)]
        public string question;
        public string[] answers;
        public int rightAnswerIndex;
    }

    public Sprite DialogeBackground;
    public GameObject DialogePesona;

    public DialogueNode[] dialogueNodes;
}