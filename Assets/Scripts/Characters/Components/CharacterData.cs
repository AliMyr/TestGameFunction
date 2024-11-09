using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private int scoreCost = 10;
    [SerializeField] private float timeBetweenAttacks = 1.0f;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;

    public float DefaultSpeed => speed;
    public int ScoreCost => scoreCost;
    public float TimeBetweenAttacks => timeBetweenAttacks;
    public Transform CharacterTransform => characterTransform;
    public CharacterController CharacterController => characterController;

    private void Awake()
    {
        if (characterTransform == null) characterTransform = transform;
        if (characterController == null) characterController = GetComponent<CharacterController>();
    }
}
