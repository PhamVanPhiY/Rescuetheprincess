using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    // Sliders 
    private Slider _healthSlider;
    private Slider _manaSlider;
    // Player Stats
    [SerializeField] private float _attack1Mana = 10;
    [SerializeField] private float _attack2Mana = 20;
    [SerializeField] private float _sprintMana = 0.05f;
    [SerializeField] private float _manaRegen;
    [SerializeField] private float _jumpMana = 20f;
    [SerializeField] private float DEFAULTMANAREGEN = 0.05f;
    // Damage Modifiers
    [SerializeField] private float DEFAULTDAMAGEMULTIPLIER = 1;
    private float _damageMultiplier;
    // Player scripts
    private PlayerAudio _playerAudio;
    private PlayerUI _playerUI;
    private Inventory _inventory;
    // Components
    private Animator _animator;
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    void Start()
    {
        // Components
        _animator = GetComponent<Animator>();
        _healthSlider = GameObject.Find("PlayerHealthBar").GetComponent<Slider>();
        _manaSlider = GameObject.Find("PlayerManaBar").GetComponent<Slider>();
        _collider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        // Player scripts
        _playerAudio = GetComponent<PlayerAudio>();
        _playerUI = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();
        // Damage Modifiers
        _damageMultiplier = DEFAULTDAMAGEMULTIPLIER;
        // Mana
        _manaRegen = DEFAULTMANAREGEN;
    }
    private void FixedUpdate()
    {
        _manaSlider.value += _manaRegen;
    }
    ////////////////////////////////////////////////////////////////////////////////
    // Getters - public
    ////////////////////////////////////////////////////////////////////////////////
    public float GetMana()
    {
        return _manaSlider.value;
    }
    public float GetHealth()
    {
        return _healthSlider.value;
    }
    public float GetAttack1Mana()
    {
        return _attack1Mana;
    }
    public float GetAttack2Mana()
    {
        return _attack2Mana;
    }
    public float GetSprintMana()
    {
        return _sprintMana;
    }
    public float GetJumpMana()
    {
        return _jumpMana;
    }
    ////////////////////////////////////////////////////////////////////////////////
    // Setters - public
    ////////////////////////////////////////////////////////////////////////////////
    public void AdjustHitPoints(float amount)
    {
        _healthSlider.value += amount;
    }
    public void AdjustMana(float amount)
    {
        _manaSlider.value += amount;
    }
    public void IncreaseMaxMana(float amount)
    {
        _manaSlider.maxValue += amount;
    }
    public void IncreaseMaxHealth(float amount)
    {
        _healthSlider.maxValue += amount;
    }
    public void IncreaseManaRegen(float amount)
    {
        _manaRegen *= amount;
    }
    ////////////////////////////////////////////////////////////////////////////////
    // Damage Multiplier - public
    ////////////////////////////////////////////////////////////////////////////////
    public void AdjustDamageMultiplier(float damage)
    {
        _damageMultiplier = damage;
    }
    public void ResetDamageMultiplier()
    {
        _damageMultiplier = DEFAULTDAMAGEMULTIPLIER;
    }
    public float GetDamageMultiplier()
    {
        return _damageMultiplier;
    }
    ////////////////////////////////////////////////////////////////////////////////
    // Damage Player - public
    ////////////////////////////////////////////////////////////////////////////////
    public IEnumerator DamagePlayer(int damage, float interval)
    {
        while (true)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            _playerAudio.Play("hurt");
            _animator.SetTrigger("hurt");
            _healthSlider.value -= damage;
            Invoke(nameof(ResetColor), 0.167f);
            if (_healthSlider.value <= float.Epsilon)
            {
                KillPlayer();
                break;
            }
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }
    ////////////////////////////////////////////////////////////////////////////////
    // Utility - private
    ////////////////////////////////////////////////////////////////////////////////
    public void ResetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void KillPlayer()
    {
        _playerAudio.Play("death");
        _animator.SetBool("alive", false);
        _playerUI.GameOverScreen(true);
        _collider.enabled = false;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Invoke(nameof(StopTime), 0.75f * 2);
        Debug.Log("Dead");
    }
    private void StopTime()
    {
        Time.timeScale = 0;
    }
}
