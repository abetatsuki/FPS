using System;
using UniRx; 
//// <summary> 体力管理クラス </summary>
public class HealthEntity : IHealth
{
    /// <summary> コンストラクタ </summary>
    public HealthEntity(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public event Action OnDeath;
    public event Action<int, int> OnHealthChanged;
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public bool IsDead => _isDead;
    public void TakeDamage(int damage)
    {
        if (_isDead) return;
        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0)
        {
            _isDead = true;
            CurrentHealth = 0;
            OnDeath?.Invoke();
        }
    }
    public void Heal(int amount)
    {
        if (_isDead) return;
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    private bool _isDead = false;



}
