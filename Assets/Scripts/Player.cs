using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AbilityLoadout _abilityLoadout;
    [SerializeField] Ability _startingAbility;

    private Transform _self = null;
    [SerializeField] GameObject _healShield = null;

    private void Awake()
    {
        _healShield.SetActive(false);
        _self = GetComponent<Transform>();

        if(_startingAbility != null)
        {
            _abilityLoadout?.EquipAbility(_startingAbility);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _abilityLoadout.UseEquippedAbility(_self);
            _healShield.SetActive(true);
        }
        else
        {
            _healShield.SetActive(false);
        }
    }
}
