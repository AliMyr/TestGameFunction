using UnityEngine;

public class PlayerCharacter : Character, ICharacterInput
{
    public override void Start()
    {
        base.Start();
        LiveComponent = new CharacterLiveComponent();
    }

    public override void Update()
    {
        Vector3 movementVector = GetMovementInput();
        MovableComponent.Move(movementVector);
        MovableComponent.Rotation(movementVector);

        if (IsAttackInput())
        {
            // Логика для атаки игрока
        }
    }

    public Vector3 GetMovementInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        return new Vector3(moveHorizontal, 0, moveVertical).normalized;
    }

    public bool IsAttackInput()
    {
        return Input.GetKeyDown(KeyCode.Space); // Например, атака на кнопку "Пробел"
    }
}
