using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static Action<int> LineClearedEvent;

    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2Int gridOrigin;

    private SingleBlock[][] blocksGrid;
    private int lineClearedScore;
    private int lineToDestroy;

    private void OnEnable() { SingleBlock.PlacedEvent += OnBlockPlaced; }
    private void OnDisable() { SingleBlock.PlacedEvent -= OnBlockPlaced; }

    private void Start()
    {
        InitGrid();
    }

    private void InitGrid()
    {
        blocksGrid = new SingleBlock[gridSize.x][];
        for (int x = 0; x < gridSize.x; x++)
        {
            blocksGrid[x] = new SingleBlock[gridSize.y];
        }
    }

    private void ClearLine()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            if (blocksGrid[x][lineToDestroy] != null)
            {
                Destroy(blocksGrid[x][lineToDestroy].gameObject);
                blocksGrid[x][lineToDestroy] = null;
            }
        }
        for (int y = lineToDestroy + 1; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                if (blocksGrid[x][y] != null)
                {
                    blocksGrid[x][y - 1] = blocksGrid[x][y];
                    blocksGrid[x][y] = null;
                    blocksGrid[x][y - 1].transform.position += Vector3.down;
                }
            }
        }
        lineClearedScore++;
        LineClearedEvent?.Invoke(lineClearedScore);
    }

    private void CheckLine()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            bool isLineFull = true;
            for (int x = 0; x < gridSize.x; x++)
            {
                if (blocksGrid[x][y] == null)
                {
                    isLineFull = false;
                    break;
                }
            }
            if (isLineFull)
            {
                lineToDestroy = y;
                ClearLine();
                return;
            }
        }
    }

    private void CheckGrid()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                if (blocksGrid[x][y] == null) continue;
                if (blocksGrid[x][y].transform.position.y >= gridSize.y)
                {
                    GameOver();
                    return;
                }
            }
        }
    }

    private void OnBlockPlaced(SingleBlock blockPlaced)
    {
        Vector2Int cellPos = WorldToCell(blockPlaced.transform.position);
        if (cellPos.x < 0 || cellPos.x >= gridSize.x || cellPos.y < 0 || cellPos.y >= gridSize.y)
        {
            GameOver();
            return;
        }
        blocksGrid[cellPos.x][cellPos.y] = blockPlaced;
        CheckLine();
        CheckGrid();
    }

    private bool CheckPlacement()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                if (blocksGrid[x][y] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void GameOver()
    {
        if (CheckPlacement())
        {
            Debug.Log("Game Over: No blocks placed yet.");
            return;
        }
        Debug.Log("Game Over: Blocks reached the top of the grid.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 cellPos = CellToWorld(new Vector2Int(x, y));
                Gizmos.DrawWireCube(cellPos + new Vector3(0.5f, 0.5f, 0), Vector3.one);
            }
        }
    }

    private void OnValidate()
    {
        if (gridSize.x < 1) gridSize.x = 1;
        if (gridSize.y < 1) gridSize.y = 1;
    }

    private Vector2Int WorldToCell(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x - gridOrigin.x),
            Mathf.FloorToInt(worldPos.y - gridOrigin.y));
    }

    private Vector3 CellToWorld(Vector2Int cellPos)
    {
        return new Vector3(
            cellPos.x + gridOrigin.x,
            cellPos.y + gridOrigin.y,
            0);
    }
}
