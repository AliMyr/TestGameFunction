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

        Renderer renderer = GetComponent<Renderer>();
        LiveComponent = new CharacterLiveComponent(renderer);

        DamageComponent = new CharacterDamageComponent();
    }


    public override void Update()
    {
        switch (currentState)
        {
            case AiState.None:
                break;

            case AiState.MoveToTarget:
                if (CharacterTarget == null) return;

                Vector3 direction = CharacterTarget.transform.position - transform.position;
                MovableComponent.Move(direction);
                MovableComponent.Rotation(direction);

                if (Vector3.Distance(CharacterTarget.transform.position, transform.position) < 3 && timeBetweenAttackCounter <= 0)
                {
                    DamageComponent.MakeDamage(CharacterTarget);
                    timeBetweenAttackCounter = characterData?.TimeBetweenAttacks ?? 1.0f;
                }

                if (timeBetweenAttackCounter > 0)
                    timeBetweenAttackCounter -= Time.deltaTime;

                break;
        }
    }
}
