using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureAoE : Ability
{
    private Collider[] _colliders;

    [SerializeField] int _healAmount = 5;
    [SerializeField] float _healTime = 0f;
    [SerializeField] float _timeBetweenHeals = 5f;
    [SerializeField] float _healDistance = 3f;

    [SerializeField] GameObject _healParticles = null;
    [SerializeField] AudioSource _healSound = null;


    private void FixedUpdate()
    {
        Use();
    }
    public override void Use()
    {
        Debug.Log("Button Pressed");
        _colliders = Physics.OverlapSphere(transform.position, _healDistance);
        for (int i=0; i<_colliders.Length; i++)
        {
            if(_colliders[i].tag == "Allies")
            {
                if (_colliders[i].gameObject != gameObject)
                {
                    _colliders[i].gameObject.GetComponent<Health>().isHealed = true;
                    HealAoE(_colliders[i].gameObject);
                }
            }
        }
    }

    public void HealAoE (GameObject UnitToHeal)
    {
        _healTime += Time.deltaTime;
        if(_healTime >= _timeBetweenHeals)
        {
            Debug.Log("Healing: " + UnitToHeal.name);
            UnitToHeal.GetComponent<Health>().Heal(_healAmount);
            _healSound.Play();
            Instantiate(_healParticles, UnitToHeal.transform);
            UnitToHeal.GetComponent<Health>().isHealed = false;
            _healTime = 0;
        }
    }
}
