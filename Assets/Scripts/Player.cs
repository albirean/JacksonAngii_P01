using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AbilityLoadout _abilityLoadout;
    [SerializeField] Ability _startingAbility;
    [SerializeField] Ability _newAbilityToTest;

    [SerializeField] Transform _testTarget = null;

    public Transform CurrentTarget { get; private set; }

    private void Awake()
    {
        if(_startingAbility != null)
        {
            _abilityLoadout?.EquipAbility(_startingAbility);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        CurrentTarget = newTarget;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _abilityLoadout.UseEquippedAbility(CurrentTarget);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _abilityLoadout.EquipAbility(_newAbilityToTest);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetTarget(_testTarget);
        }
    }
}
