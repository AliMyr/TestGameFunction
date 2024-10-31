using UnityEngine;

public interface IMovable
{
    float Speed { get; set; }
    void Initialize(CharacterData characterData);
    void Move(Vector3 direction);
    void Rotation(Vector3 direction);
}
