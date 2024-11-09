using UnityEngine;
using System.Collections.Generic;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Character playerCharacterPrefab;
    [SerializeField] private Character enemyCharacterPrefab;

    private Dictionary<CharacterType, Queue<Character>> disabledCharacters
        = new Dictionary<CharacterType, Queue<Character>>();

    private List<Character> activeCharacters = new List<Character>();

    public Character Player { get; private set; }
    public List<Character> ActiveCharacters => activeCharacters;

    public Character GetCharacter(CharacterType type)
    {
        Character character = null;
        if (disabledCharacters.ContainsKey(type) && disabledCharacters[type].Count > 0)
        {
            character = disabledCharacters[type].Dequeue();
        }
        else if (!disabledCharacters.ContainsKey(type))
        {
            disabledCharacters.Add(type, new Queue<Character>());
        }

        if (character == null)
        {
            character = InstantiateCharacter(type);
        }

        activeCharacters.Add(character);
        character.gameObject.SetActive(true);
        return character;
    }

    public void ReturnCharacter(Character character)
    {
        if (character == null) return;

        character.Enqueue();
        Queue<Character> characters = disabledCharacters[character.CharacterType];
        characters.Enqueue(character);

        activeCharacters.Remove(character);
    }

    private Character InstantiateCharacter(CharacterType type)
    {
        Character character = null;
        switch (type)
        {
            case CharacterType.Player:
                character = Instantiate(playerCharacterPrefab);
                Player = character;
                break;

            case CharacterType.DefaultEnemy:
                character = Instantiate(enemyCharacterPrefab);
                break;

            default:
                Debug.LogError("Unknown character type: " + type);
                break;
        }
        character.Initialize();
        return character;
    }
}
