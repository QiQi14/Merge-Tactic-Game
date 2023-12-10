using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridControlPlayer01 : MonoBehaviour
{
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] GridMap _gridMap;
    [SerializeField] GameObject[] prefabsToSpawn;
    private List<Vector3Int> spawnedPrefabPositions = new List<Vector3Int>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickPosition = targetTilemap.WorldToCell(worldPoint);
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


        // Lặp để chọn một vị trí chưa spawn prefab
        do
        {
             randomX = Random.Range(0, _gridMap.length);
             randomY = Random.Range(0, _gridMap.height);
            randomCellPosition = new Vector3Int(randomX, randomY, 0);
        } while (spawnedPrefabPositions.Contains(randomCellPosition));

        // Kiểm tra xem vị trí đã có trong danh sách chưa
        if (!spawnedPrefabPositions.Contains(randomCellPosition))
        {
            Vector3 spawnPosition = targetTilemap.GetCellCenterWorld(randomCellPosition);

            // Random một prefab từ mảng và spawn tại vị trí ngẫu nhiên trên tilemap
            GameObject randomPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
            Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

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
        // Tạo một Dictionary để lưu trữ danh sách prefab theo loại và level
        Dictionary<string, List<GameObject>> prefabDict = new Dictionary<string, List<GameObject>>();

        // Duyệt qua danh sách prefab đã spawn
        foreach (var spawnedPrefabPosition in spawnedPrefabPositions)
        {
            Vector3Int cellPosition = targetTilemap.WorldToCell(spawnedPrefabPosition);
            GameObject spawnedPrefab = targetTilemap.GetInstantiatedObject(cellPosition);

            // Lấy prefab tại vị trí hiện tại
            //GameObject spawnedPrefab = targetTilemap.GetInstantiatedObject(spawnedPrefabPosition);
            Debug.Log("Checking position: " + spawnedPrefabPosition);
            Debug.Log("Tilemap bounds: " + targetTilemap.cellBounds);

            if (spawnedPrefab != null)
            {
                // Lấy hoặc tạo danh sách prefab theo loại và level
                string prefabType = spawnedPrefab.tag;
                int prefabLevel = spawnedPrefab.GetComponent<Character>().level;

                string key = prefabType + "_" + prefabLevel;

                if (!prefabDict.ContainsKey(key))
                {
                    prefabDict[key] = new List<GameObject>();
                }

                // Thêm prefab vào danh sách
                prefabDict[key].Add(spawnedPrefab);
            }
            else
            {
                Debug.LogError("Spawned prefab is null. Check the position and tilemap settings.");
            }
        }

        // Duyệt qua từng loại prefab và level
        foreach (var prefabList in prefabDict.Values)
        {
            // Kiểm tra tổng số prefab của từng loại và level
            int totalPrefabs = prefabList.Count;

            // Duyệt qua danh sách prefab của từng loại và level
            foreach (var spawnedPrefab in prefabList)
            {
                // Kiểm tra xem prefab có thể merge với các prefab khác trên tilemap hay không
                if (CanMerge(prefabList, spawnedPrefab))
                {
                    // Thực hiện merge bằng cách tăng level và cập nhật text
                    spawnedPrefab.GetComponent<Character>().level++;
                    spawnedPrefab.GetComponent<Character>().TMP.text = spawnedPrefab.GetComponent<Character>().level.ToString();

                    // Xóa prefab khác tại vị trí hiện tại
                    targetTilemap.SetTile(targetTilemap.WorldToCell(spawnedPrefab.transform.position), null);
                }
            }

            // Kiểm tra và merge level 1 thành level 2
            if (totalPrefabs >= 2 && prefabList[0].GetComponent<Character>().level == 1)
            {
                // Merge level 1 thành level 2
                prefabList[0].GetComponent<Character>().level++;
                prefabList[1].GetComponent<Character>().level++;
                prefabList[0].GetComponent<Character>().TMP.text = prefabList[0].GetComponent<Character>().level.ToString();
                prefabList[1].GetComponent<Character>().TMP.text = prefabList[1].GetComponent<Character>().level.ToString();
            }

            // Kiểm tra và merge level 2 thành level 3
            if (totalPrefabs >= 2 && prefabList[0].GetComponent<Character>().level == 2)
            {
                // Merge level 2 thành level 3
                prefabList[0].GetComponent<Character>().level++;
                prefabList[1].GetComponent<Character>().level++;
                prefabList[0].GetComponent<Character>().TMP.text = prefabList[0].GetComponent<Character>().level.ToString();
                prefabList[1].GetComponent<Character>().TMP.text = prefabList[1].GetComponent<Character>().level.ToString();
            }
        }
    }

    // Hàm kiểm tra xem prefab có thể merge với các prefab khác trên tilemap hay không
    private bool CanMerge(List<GameObject> prefabList, GameObject currentPrefab)
    {
        Character currentCharacterController = currentPrefab.GetComponent<Character>();

        foreach (var otherPrefab in prefabList)
        {
            if (currentPrefab != otherPrefab)
            {
                Character otherCharacterController = otherPrefab.GetComponent<Character>();

                // Kiểm tra nếu level của hai prefab là giống nhau và level là 1 hoặc 2 thì có thể merge
                if (currentCharacterController.level == otherCharacterController.level
                    && (currentCharacterController.level == 1 || currentCharacterController.level == 2))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
