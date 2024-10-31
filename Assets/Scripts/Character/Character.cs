using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private CharacterData characterData;

    public CharacterData CharacterData => characterData;

    public IMovable MovableComponent { get; protected set; }
    public ILiveComponent LiveComponent { get; protected set; }
    public IDamageComponent DamageComponent { get; protected set; }

    public virtual void Start()
    {
        MovableComponent = new CharacterMovementComponent();
        MovableComponent.Initialize(this);
    }

    public abstract void Update();
}
