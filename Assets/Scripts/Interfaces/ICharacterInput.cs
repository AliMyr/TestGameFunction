using UnityEngine;

public interface ICharacterInput
{
    Vector3 GetMovementInput();
    bool IsAttackInput();
}
