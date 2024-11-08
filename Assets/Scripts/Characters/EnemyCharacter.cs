using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private AiState currentState;

    private float timeBetweenAttackCounter = 0;

    public override Character CharacterTarget =>
        GameManager.Instance.CharacterFactory.Player;

    public override void Initialize()
    {
        base.Initialize();

        // Инициализация LiveComponent без вызова Initialize
        LiveComponent = new CharacterLiveComponent();

        // Инициализация компонента урона
        DamageComponent = new CharacterDamageComponent();
    }

    public override void Update()
    {
        // Проверка состояния ИИ
        switch (currentState)
        {
            case AiState.None:
                // Никаких действий не требуется
                break;

            case AiState.MoveToTarget:
                // Проверка на наличие цели перед движением
                if (CharacterTarget == null) return;

                // Определение направления к цели
                Vector3 direction = CharacterTarget.transform.position - transform.position;
                direction.Normalize();

                // Движение и вращение к цели
                MovableComponent.Move(direction);
                MovableComponent.Rotation(direction);

                // Атака, если враг в пределах расстояния атаки и перезарядка завершена
                if (Vector3.Distance(CharacterTarget.transform.position, transform.position) < 3
                    && timeBetweenAttackCounter <= 0)
                {
                    DamageComponent.MakeDamage(CharacterTarget);
                    timeBetweenAttackCounter = characterData?.TimeBetweenAttacks ?? 1.0f; // Используйте значение по умолчанию, если characterData не задано
                }

                // Обновление счетчика времени между атаками
                if (timeBetweenAttackCounter > 0)
                    timeBetweenAttackCounter -= Time.deltaTime;

                break;
        }
    }
}
