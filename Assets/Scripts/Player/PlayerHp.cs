using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public PlayerData playerData;
    [Header("Health Settings")]
    // [SerializeField] protected int _maxHealth = 30;
    protected float _healthDecreaseInterval = 1f;
    protected int _healthDecreaseAmount = 1;
    protected float _healthIncreaseInterval = 0.05f;
    protected int _healthIncreaseAmount = 1;

    [Header("UI References")]
    [SerializeField] protected Image _healthImage;

    
    protected float _timeSinceLastHpDecrease;
    protected float _timeSinceLastHpIncrease;
    protected bool _isDead;
    protected bool _isHealing;

    public int CurrentHealth => playerData.currentHealth;
    public int MaxHealth => playerData ? playerData.maxHealth : 0;
    public float HealthPercentage => (float)playerData.currentHealth / MaxHealth;

    protected virtual void Start()
    {
        InitializeHealth();
    }

    protected virtual void Update()
    {
        if (_isDead) return;
        HandleHealthDecrease();
        HandleHealing();
        CheckDeath();
    }

    protected virtual void InitializeHealth()
    {
        playerData.currentHealth = MaxHealth; 
        _timeSinceLastHpDecrease = 0f;
        _timeSinceLastHpIncrease = 0f;
        _isDead = false;
        _isHealing = false;
        UpdateHealthUI();
    }

    protected virtual void HandleHealthDecrease()
    {
        // Skip health decrease if Fever Time is active
        if (GameManager.Instance != null && GameManager.Instance.isFeverTime)
            return;

        _timeSinceLastHpDecrease += Time.deltaTime;
        if (_timeSinceLastHpDecrease >= _healthDecreaseInterval)
        {
            ModifyHealth(-_healthDecreaseAmount);
            _timeSinceLastHpDecrease = 0f;
        }
    }

    protected virtual void HandleHealing()
    {
        if (!_isHealing) return;

        _timeSinceLastHpIncrease += Time.deltaTime;
        if (_timeSinceLastHpIncrease >= _healthIncreaseInterval)
        {
            ModifyHealth(_healthIncreaseAmount);
            _timeSinceLastHpIncrease = 0f;
        }
    }

    protected virtual void CheckDeath()
    {
        if (playerData.currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void ModifyHealth(int amount)
    {
        if (_isDead) return;
        playerData.currentHealth = Mathf.Clamp(playerData.currentHealth + amount, 0, MaxHealth);
        UpdateHealthUI();
    }

    public void DecreaseHealth(int amount) => ModifyHealth(-amount);
    public void IncreaseHealth(int amount) => ModifyHealth(amount);

    protected virtual void UpdateHealthUI()
    {
        if (_healthImage != null)
        {
            _healthImage.fillAmount = HealthPercentage;
        }
    }

    protected virtual void Die()
    {
        _isDead = true;
        playerData.currentHealth = 0;
        UpdateHealthUI();
        GameManager.Instance.GameOver();
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            Debug.Log($"[{gameObject.name} HP] Healing started");
            _isHealing = true;
        }
    }

    protected virtual void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            _isHealing = false;
            _timeSinceLastHpIncrease = 0f;
        }
    }
}
