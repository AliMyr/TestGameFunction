using System.Collections;
using System.Collections.Generic;
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

        LiveComponent = new CharacterLiveComponent();
        LiveComponent.Initialize(this);

        DamageComponent = new CharacterDamgaeComponent();
    }

    public override void Update()
    {
        switch (currentState)
        {
            case AiState.None:
                break;

            case AiState.MoveToTarget:
                Vector3 direction = CharacterTarget.transform.position - transform.position;
                direction.Normalize();

                MovableComponent.Move(direction);
                MovableComponent.Rotation(direction);

                if (Vector3.Distance(CharacterTarget.transform.position, transform.position) < 3
                    && timeBetweenAttackCounter <= 0) 
                {
                    DamageComponent.MakeDamage(CharacterTarget);
                    timeBetweenAttackCounter = characterData.TimeBetweenAttacks;
                }

                if (timeBetweenAttackCounter > 0)
                    timeBetweenAttackCounter -= Time.deltaTime;

                break;
        }
    }
}
