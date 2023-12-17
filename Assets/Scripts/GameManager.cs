using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Character[] availableCharacters; // Danh sách nhân vật có sẵn để spawn
    public GameObject characterPrefab; // Prefab của nhân vật để spawn
    public Transform[] spawnPoints; // Mảng các điểm spawn
    public List<GameObject> charactersInGame = new List<GameObject>(); // Danh sách nhân vật trong game
    private int maxCharacters = 6; // Số lượng tối đa nhân vật
    private HashSet<Transform> usedSpawnPoints = new HashSet<Transform>();

    // Hàm để spawn nhân vật ngẫu nhiên từ danh sách
    public void SpawnCharacter()
    {
        if (charactersInGame.Count >= maxCharacters)
        {
            Debug.Log("Đã đạt tối đa số lượng nhân vật");
            return;
        }

        int randomIndex = Random.Range(0, availableCharacters.Length);
        Character characterToSpawn = availableCharacters[randomIndex];

        Transform chosenSpawnPoint = ChooseSpawnPoint();
        if (chosenSpawnPoint == null)
        {
            Debug.Log("Không còn điểm spawn nào");
            return;
        }

        GameObject newCharacter = Instantiate(characterPrefab, chosenSpawnPoint.position, Quaternion.identity);
        newCharacter.GetComponent<CharacterBehaviour>().Setup(characterToSpawn);
        charactersInGame.Add(newCharacter);
        usedSpawnPoints.Add(chosenSpawnPoint);
    }
    public void ResetUsedSpawnPoints()
    {
        usedSpawnPoints.Clear();
    }
    private Transform ChooseSpawnPoint()
    {
        var availableSpawnPoints = spawnPoints.Except(usedSpawnPoints).ToArray();
        if (availableSpawnPoints.Length == 0) return null;

        return availableSpawnPoints[Random.Range(0, availableSpawnPoints.Length)];
    }

    public void MergeCharacters()
    {
        var charactersByTypeAndLevel = new Dictionary<string, List<GameObject>>();

        // Phân loại nhân vật theo tên và cấp độ
        foreach (var character in charactersInGame)
        {
            var characterData = character.GetComponent<CharacterBehaviour>().CharacterData;
            string key = characterData.characterName + "_" + characterData.level;

            if (!charactersByTypeAndLevel.ContainsKey(key))
            {
                charactersByTypeAndLevel[key] = new List<GameObject>();
            }

            charactersByTypeAndLevel[key].Add(character);
        }

        // Duyệt qua từng nhóm và thực hiện merge nếu đủ điều kiện
        foreach (var pair in charactersByTypeAndLevel)
        {
            var charactersToMerge = pair.Value;

            // Chỉ merge khi có ít nhất 2 nhân vật cùng loại và cùng cấp độ
            while (charactersToMerge.Count >= 2)
            {
                // Lấy ra 2 nhân vật đầu tiên trong danh sách
                var char1 = charactersToMerge[0];
                var char2 = charactersToMerge[1];

                // Tạo dữ liệu nhân vật mới với các thuộc tính cộng dồn
                var mergedCharacterData = new Character
                {
                    characterName = char1.GetComponent<CharacterBehaviour>().CharacterData.characterName,
                    level = char1.GetComponent<CharacterBehaviour>().CharacterData.level + 1,
                    maxHealth = char1.GetComponent<CharacterBehaviour>().CharacterData.maxHealth + char2.GetComponent<CharacterBehaviour>().CharacterData.maxHealth,
                    maxMana = char1.GetComponent<CharacterBehaviour>().CharacterData.maxMana + char2.GetComponent<CharacterBehaviour>().CharacterData.maxMana,
                    defense = char1.GetComponent<CharacterBehaviour>().CharacterData.defense + char2.GetComponent<CharacterBehaviour>().CharacterData.defense,
                    attack = char1.GetComponent<CharacterBehaviour>().CharacterData.attack + char2.GetComponent<CharacterBehaviour>().CharacterData.attack,
                    characterSprite = char1.GetComponent<CharacterBehaviour>().CharacterData.characterSprite // Giữ nguyên sprite
                };

                // Xóa nhân vật cũ khỏi danh sách và scene
                charactersToMerge.Remove(char1);
                charactersInGame.Remove(char1);
                Destroy(char1);

                charactersToMerge.Remove(char2);
                charactersInGame.Remove(char2);
                Destroy(char2);

                // Tạo nhân vật mới và thêm vào danh sách
                Transform chosenSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject newCharacter = Instantiate(characterPrefab, chosenSpawnPoint.position, Quaternion.identity);
                newCharacter.GetComponent<CharacterBehaviour>().Setup(mergedCharacterData);
                charactersInGame.Add(newCharacter);
                charactersToMerge.Add(newCharacter);
            }
        }
    }
}
