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
}
