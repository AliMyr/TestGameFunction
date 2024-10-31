using UnityEngine;

public class EnemyCharacter : Character, ICharacterInput
{
    [SerializeField] private AiState currentState = AiState.None;
    [SerializeField] private Character targetCharacter;

    private float timeBetweenAttackCounter = 0;

    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private float chaseDistance = 5f;

    public override void Start()
    {
        base.Start();
        LiveComponent = new ImmortalLiveComponent();
        DamageComponent = new CharacterDamageComponent();
    }

    public override void Update()
    {
        float distanceToTarget = Vector3.Distance(targetCharacter.transform.position, transform.position);

        switch (currentState)
        {
            case AiState.None:
                CheckForChase(distanceToTarget);
                break;
            case AiState.MoveToTarget:
                MoveToTarget(distanceToTarget);
                break;
            case AiState.Attack:
                AttackTarget(distanceToTarget);
                break;
        }
    }

    private void CheckForChase(float distanceToTarget)
    {
        if (distanceToTarget <= chaseDistance)
        {
            currentState = AiState.MoveToTarget;
        }
    }

    private void MoveToTarget(float distanceToTarget)
    {
        if (distanceToTarget <= attackDistance)
        {
            currentState = AiState.Attack;
            return;
        }

        Vector3 direction = GetMovementInput();
        MovableComponent.Move(direction);
        MovableComponent.Rotation(direction);

        if (distanceToTarget > chaseDistance)
        {
            currentState = AiState.None;
        }
    }

    private void AttackTarget(float distanceToTarget)
    {
        if (distanceToTarget > attackDistance)
        {
            currentState = AiState.MoveToTarget;
            return;
        }

        if (timeBetweenAttackCounter <= 0)
        {
            DamageComponent.MakeDamage(targetCharacter);
            timeBetweenAttackCounter = characterData.TimeBetweenAttacks;
        }

        if (timeBetweenAttackCounter > 0)
            timeBetweenAttackCounter -= Time.deltaTime;
    }

    public Vector3 GetMovementInput()
    {
        return (targetCharacter.transform.position - transform.position).normalized;
    }

    public bool IsAttackInput()
    {
        return Vector3.Distance(targetCharacter.transform.position, transform.position) <= attackDistance;
    }
}
