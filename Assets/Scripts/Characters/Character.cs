using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private CharacterType characterType;

    [SerializeField]
    protected CharacterData characterData;

    public virtual Character CharacterTarget { get; }
    public CharacterType CharacterType => characterType;
    public CharacterData CharacterData => characterData;
    public IMovable MovableComponent { get; protected set; }
    public ILiveComponent LiveComponent { get; protected set; }
    public IDamageComponent DamageComponent { get; protected set; }

    public virtual void Initialize()
    {
        MovableComponent = new CharacterMovementComponent();
        MovableComponent.Initialize(this);
    }

    public abstract void Update();

    public void Enqueue(Character character)
    {
        // ƒополнительна€ логика дл€ сброса состо€ни€ персонажа перед возвращением в пул.
    }
}
