using UnityEngine;

public class CharacterMovementComponent : IMovable
{
    private Character character;
    private float speed;

    public float Speed
    {
        get => speed;
        set
        {
            if (value < 0) return;
            speed = value;
        }
    }

    public void Initialize(Character character)
    {
        this.character = character;
        speed = character.CharacterData.DefaultSpeed;
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Vector3 move = direction.normalized * Speed * Time.deltaTime;
        character.CharacterData.CharacterController.Move(move);
    }

    public void Rotation(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        character.CharacterData.CharacterTransform.rotation = Quaternion.Euler(0, targetAngle, 0);
    }
}
