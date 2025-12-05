using System;

/// <summary> 体力管理インターフェース </summary>
public interface IHealth
{
    /// <summary> 死亡したときのイベント </summary>
    public event Action OnDeath
    {
        add => OnDeath += value;
        remove => OnDeath -= value;
    }
    /// <summary> 体力が変化したときのイベント (現在の体力, 最大体力) </summary>
    public event Action<int, int> OnHealthChanged
    {
        add => OnHealthChanged += value;
        remove => OnHealthChanged -= value;
    }
    /// <summary> 現在の体力 </summary>
    public int CurrentHealth { get; }
    /// <summary> 最大体力 </summary>
    public int MaxHealth { get; }
    /// <summary> 死亡しているか </summary>
    public bool IsDead { get; }
    /// <summary> ダメージを受ける </summary>
    public void TakeDamage(int amount);
    /// <summary> 回復する </summary>
    public void Heal(int amount);
}