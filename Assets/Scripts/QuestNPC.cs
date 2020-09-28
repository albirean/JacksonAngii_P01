using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestNPC : MonoBehaviour
{
    [SerializeField] Health hp = null;
    public bool _isHealed = false;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.FullyHealed == true)
        {
            _isHealed = true;
        }
    }
}
