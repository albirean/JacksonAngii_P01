using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    public int _maxHealth = 100;
    public int _currentHealth = 50;
    public bool isHealed = false;
    public bool FullyHealed = false;

    private bool _damageFlash = false;

    [SerializeField] HealthBar hpBar = null;
    [SerializeField] Image damagedImage = null;
    [SerializeField] AudioSource _damageAudio = null;
    void Start()
    {
        hpBar.SetMaxHealth(_maxHealth);
        damagedImage.gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        hpBar.SetHealth(_currentHealth);
        if(_currentHealth == _maxHealth)
        {
            FullyHealed = true;
        }
        if(_currentHealth == _maxHealth)
        {
            Kill();
        }
        
    }
    public void Heal(int amount)
    {
        _currentHealth += amount;
        Debug.Log(gameObject.name + " has healed " + amount);
    }

    public void TakeDamage(int damageTaken)
    {
        _currentHealth -= damageTaken;
        hpBar.SetHealth(_currentHealth);
        Debug.Log(gameObject.name + " has taken " + damageTaken + " damage.");
        _damageFlash = true;
        StartCoroutine("DamageFlash");
        _damageAudio.Play();
    }

    IEnumerator DamageFlash()
    {
        Renderer r = GetComponentInChildren<Renderer>();
        while (_damageFlash)
        {
            if (damagedImage)
            {
            _damageFlash = true;
            damagedImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            damagedImage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            r.enabled = false;
            yield return new WaitForSeconds(0.3f);
            r.enabled = true;
            yield return new WaitForSeconds(0.5f);
            r.enabled = false;
            yield return new WaitForSeconds(0.3f);
            r.enabled = true;
            yield return new WaitForSeconds(0.5f);

            _damageFlash = false;
              }
            _damageFlash = false;
        }
    }

    public void Kill()
    {
        if(_currentHealth <= 0)
        {
            if (damagedImage)
            {
                Destroy(damagedImage);
            }
            StopCoroutine("DamageFlash");
            Debug.Log(gameObject.name + " has died.");
        }
    }
}
