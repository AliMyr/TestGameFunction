using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter : Character
{
    public override Character CharacterTarget
    {
        get
        {
            Character target = null;
            float minDistance = float.MaxValue;
            List<Character> list = GameManager.Instance.CharacterFactory.ActiveCharacters;
            foreach (var enemy in list)
            {
                if (enemy.CharacterType == CharacterType.Player) continue;

                float distance = Vector3.Distance(enemy.transform.position, transform.position);
                if (distance < minDistance)
                {
                    target = enemy;
                    minDistance = distance;
                }
            }
            return target;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        LiveComponent = new CharacterLiveComponent(GetComponent<Renderer>()); // Передаем рендерер
        DamageComponent = new CharacterDamageComponent();
    }

    public override void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (MovableComponent != null)
        {
            if (CharacterTarget == null)
            {
                MovableComponent.Rotation(movementVector);
            }
            else
            {
                Vector3 rotationDirection = CharacterTarget.transform.position - transform.position;
                MovableComponent.Rotation(rotationDirection);

                if (Input.GetButtonDown("Jump") && DamageComponent != null)
                {
                    DamageComponent.MakeDamage(CharacterTarget);

                    // Визуальная индикация атаки (можно добавить звук или эффект удара)
                    Debug.Log("Player attacked the enemy!");
                }
            }

            MovableComponent.Move(movementVector);
        }
    }
}
