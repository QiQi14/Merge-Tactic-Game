using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Button spawnButton;
    public GameObject characterPrefab;
    public Camera mainCamera; 
    public float spawnAreaWidth = 5.0f; 
    public float spawnAreaHeight = 5.0f; 
    public List<CharacterData> characterTemplates = new List<CharacterData>();
    public int maxCharacters = 6; 

    private List<Character> spawnedCharacters = new List<Character>();
    private int characterCount = 0; 

    private void Start()
    {
        spawnButton.onClick.AddListener(SpawnCharacter);
    }

    private void SpawnCharacter()
    {
        if (characterCount < maxCharacters)
        {
            
            CharacterData randomCharacterData = characterTemplates[Random.Range(0, characterTemplates.Count)];

            
            Character newCharacter = CreateCharacterFromData(randomCharacterData);

           
            float randomX = Random.Range(Screen.width * 0.5f - spawnAreaWidth * 0.5f, Screen.width * 0.5f + spawnAreaWidth * 0.5f);
            float randomY = Random.Range(Screen.height * 0.5f - spawnAreaHeight * 0.5f, Screen.height * 0.5f + spawnAreaHeight * 0.5f);

            Vector3 randomScreenPosition = new Vector3(randomX, randomY, 0);
            Vector3 spawnPosition = mainCamera.ScreenToWorldPoint(randomScreenPosition);
            spawnPosition.z = 0; 

            
            GameObject spawnedObject = Instantiate(characterPrefab, spawnPosition, Quaternion.identity);

          
            CharacterDis characterDisplay = spawnedObject.GetComponent<CharacterDis>();
            if (characterDisplay != null)
            {
                characterDisplay.SetCharacterStats(randomCharacterData);
            }

           
            spawnedCharacters.Add(newCharacter);
            characterCount++;
        }
    }

    private Character CreateCharacterFromData(CharacterData characterData)
    {
        
        Character newCharacter = new Character(characterData.characterName, characterData.level);

        
        newCharacter.health = characterData.baseHealth;
        newCharacter.mana = characterData.baseMana;
        newCharacter.defense = characterData.baseDefense;
        newCharacter.attack = characterData.baseAttack;
        newCharacter.skills = new SkillSet(
            new Skill(characterData.normalAttack.name, characterData.normalAttack.damage),
            new Skill(characterData.passiveSkill.name, characterData.passiveSkill.damage),
            new Skill(characterData.activeSkill.name, characterData.activeSkill.damage)
        );

        
        return newCharacter;
    }
}
