using UnityEngine;

public class CharacterLiveComponent : ILiveComponent
{
    public float MaxHealth { get; set; } = 50;
    public float Health { get; set; }

    public event System.Action<Character> OnCharacterDeath;

    private Renderer characterRenderer; // Добавляем рендерер для визуального эффекта
    private Color originalColor;

    public CharacterLiveComponent(Renderer renderer)
    {
        Health = MaxHealth;
        characterRenderer = renderer;
        if (characterRenderer != null)
        {
            originalColor = characterRenderer.material.color;
        }
    }

    public void SetDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            SetDeath();
        }
        else
        {
            ShowDamageEffect(); // Вызов визуального эффекта урона
        }
    }

    private void SetDeath()
    {
        Debug.Log("Character is dead");
        OnCharacterDeath?.Invoke(null);
    }

    private void ShowDamageEffect()
    {
        if (characterRenderer == null) return;

        characterRenderer.material.color = Color.red; // Меняем цвет на красный
        // Сбрасываем цвет через корутину
        characterRenderer.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(ResetColor());
    }

    private System.Collections.IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.2f); // Через 0.2 секунды вернуть цвет
        if (characterRenderer != null)
        {
            characterRenderer.material.color = originalColor;
        }
    }
}
