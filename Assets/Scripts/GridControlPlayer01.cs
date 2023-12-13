using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridControlPlayer01 : MonoBehaviour
{
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] GridMap _gridMap;
    [SerializeField] GameObject[] prefabsToSpawn;
    private List<Vector3Int> spawnedPrefabPositions = new List<Vector3Int>();
    // List để lưu trữ thông tin về prefab đã spawn
    private List<SpawnedPrefabInfo> spawnedPrefabInfos = new List<SpawnedPrefabInfo>();
    private int attemptCount = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector3Int clickPosition = targetTilemap.WorldToCell(worldPoint);
        }
    }
    public void SpawnRandomPrefab()
    {
        if (prefabsToSpawn.Length == 0)
        {
            Debug.LogError("No prefabs to spawn. Please assign prefabs to the array.");
            return;
        }

        int randomX, randomY;
        Vector3Int randomCellPosition;

        int maxAttempts = 100; // Giới hạn số lần thử tìm vị trí
        attemptCount = 0;
        // Lặp để chọn một vị trí chưa spawn prefab
        do
        {
            randomX = Random.Range(0, _gridMap.length);
            randomY = Random.Range(0, _gridMap.height);
            randomCellPosition = new Vector3Int(randomX, randomY, 0);

            attemptCount++;
            if (attemptCount > maxAttempts)
            {
                Debug.LogError("Failed to find a free position for the prefab after " + maxAttempts + " attempts.");
                return;
            }
        } while (spawnedPrefabPositions.Contains(randomCellPosition));

        // Kiểm tra xem vị trí đã có trong danh sách chưa
        if (!spawnedPrefabPositions.Contains(randomCellPosition))
        {
            Vector3 spawnPosition = targetTilemap.GetCellCenterWorld(randomCellPosition);

            // Random một prefab từ mảng và spawn tại vị trí ngẫu nhiên trên tilemap
            GameObject randomPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
            GameObject instantiatedPrefab =  Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

            // Lưu thông tin về prefab đã spawn
            SpawnedPrefabInfo prefabInfo = new SpawnedPrefabInfo
            {
                prefab = instantiatedPrefab,
                position = randomCellPosition
            };
            spawnedPrefabInfos.Add(prefabInfo);

            // Lưu trữ vị trí đã spawn prefab
            spawnedPrefabPositions.Add(randomCellPosition);

        }
        else
        {
            Debug.Log("Prefab already spawned at this position.");
        }
    }

    // Hàm để kiểm tra và thực hiện merge nhân vật
    public void MergeCharacters()
    {
        Dictionary<string, List<GameObject>> prefabDict = new Dictionary<string, List<GameObject>>();

        foreach (var prefabInfo in spawnedPrefabInfos)
        {
            GameObject spawnedPrefab = prefabInfo.prefab;
            if (spawnedPrefab != null)
            {
                string prefabType = spawnedPrefab.tag;
                int prefabLevel = spawnedPrefab.GetComponent<Character>().level;
                string key = prefabType + "_" + prefabLevel;

                if (!prefabDict.ContainsKey(key))
                {
                    prefabDict[key] = new List<GameObject>();
                }

                prefabDict[key].Add(spawnedPrefab);
            }
            else
            {
                Debug.LogError("Spawned prefab is null. Check the position and tilemap settings.");
            }
        }

        // Duyệt qua từng loại prefab và level
        foreach (var key in prefabDict.Keys.ToList())
        {
            List<GameObject> prefabsOfSameTypeAndLevel = prefabDict[key];
            int level = prefabsOfSameTypeAndLevel[0].GetComponent<Character>().level;

            while (prefabsOfSameTypeAndLevel.Count >= 2 && level < 3)
            {
                for (int i = 0; i < prefabsOfSameTypeAndLevel.Count - 1; i += 2)
                {
                    if (i + 1 >= prefabsOfSameTypeAndLevel.Count) break;
                    // Tạo prefab mới ở level cao hơn
                    GameObject newPrefab = Instantiate(prefabsOfSameTypeAndLevel[i], prefabsOfSameTypeAndLevel[i + 1].transform.position, Quaternion.identity);
                    newPrefab.GetComponent<Character>().level = level + 1;
                    newPrefab.GetComponent<Character>().TMP.text = "Level 0" + newPrefab.GetComponent<Character>().level.ToString();

                    // Thêm prefab mới vào danh sách spawnedPrefabInfos
                    spawnedPrefabInfos.Add(new SpawnedPrefabInfo { prefab = newPrefab, position = Vector3Int.RoundToInt(newPrefab.transform.position) });

                    // Xác định vị trí của prefab cần xóa
                    Vector3Int positionToRemove1 = spawnedPrefabInfos.Find(info => info.prefab == prefabsOfSameTypeAndLevel[i]).position;

                    // Xóa prefab cũ
                    Destroy(prefabsOfSameTypeAndLevel[i]);
                    Destroy(prefabsOfSameTypeAndLevel[i + 1]);

                    // Xóa vị trí của prefab cũ khỏi spawnedPrefabPositions
                    spawnedPrefabPositions.Remove(positionToRemove1);

                    // Xóa vị trí prefab trên tilemap
                    spawnedPrefabInfos.RemoveAll(info => info.prefab == prefabsOfSameTypeAndLevel[i] || info.prefab == prefabsOfSameTypeAndLevel[i + 1]);


                    // Cập nhật Dictionary với prefab mới
                    string newKey = newPrefab.tag + "_" + (level + 1);
                    if (!prefabDict.ContainsKey(newKey))
                    {
                        prefabDict[newKey] = new List<GameObject>();
                    }
                    prefabDict[newKey].Add(newPrefab);

                    // Xóa prefab cũ khỏi Dictionary
                    prefabsOfSameTypeAndLevel.RemoveAt(i + 1);
                    prefabsOfSameTypeAndLevel.RemoveAt(i);

                    // Đảm bảo cập nhật danh sách prefabsOfSameTypeAndLevel sau mỗi lần merge
                    if (prefabDict.ContainsKey(newKey))
                    {
                        prefabsOfSameTypeAndLevel = prefabDict[newKey];
                    }
                }

                // Cập nhật level cho vòng lặp tiếp theo
                level++;
            }
        }


    }

    // Class để lưu thông tin về prefab đã spawn
    private class SpawnedPrefabInfo
    {
        public GameObject prefab;
        public Vector3Int position;
    }
}
