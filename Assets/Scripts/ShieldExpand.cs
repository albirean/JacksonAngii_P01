using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldExpand : MonoBehaviour
{
    [SerializeField] GameObject Shield = null;
    private float maxSize = 3f;
    private float expandTime = 2f;
    private float expandRatio = 0.5f;
    private Vector3 startSize = new Vector3 (0, 0, 0);

    private void Expand()
    {
        expandTime -= Time.deltaTime;
        if(expandTime > 0)
        {
            Shield.transform.localScale = (Shield.transform.localScale * Time.deltaTime * expandRatio);
        }
        if(expandTime < 0)
        {
            Shield.transform.localScale = startSize;
        }
    }
}
