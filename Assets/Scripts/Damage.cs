using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int _damageDealt = 5;

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision");
        DealDamage(collider.gameObject, _damageDealt);
    }

    public static void DealDamage(GameObject gameObject, int _damageDealt)
    {
        //get all damageable components
        Component[] damageables = gameObject.GetComponents(typeof(IDamageable));

        if(damageables == null)
        {
            return;
        }
        foreach(IDamageable damage in damageables)
        {
            damage.TakeDamage(_damageDealt);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
