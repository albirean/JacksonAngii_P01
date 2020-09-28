using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField] QuestNPC questNPC = null;
    [SerializeField] GameObject questMarker = null;
    [SerializeField] Text QuestObjective = null;
    [SerializeField] string questText = "";

    // Start is called before the first frame update
    void Start()
    {
        QuestObjective.gameObject.SetActive(true);
        questMarker.SetActive(true);
        QuestObjective.text = questText;
        
    }

    public void QuestComplete()
    {
        if(questNPC._isHealed == true)
        {
            Debug.Log("Quest Complete.");
            QuestObjective.gameObject.SetActive(false);
            questMarker.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        QuestComplete();
    }
}
