using System.Collections.Generic;
using UnityEngine;

public class NextBlock : MonoBehaviour
{
    private BlockGroup nextBlockGroup;
    [SerializeField] private List<BlockGroup> availableBlockGroups = new List<BlockGroup>();

    private void Start()
    {
        PickNextBlockGroup();
    }

    public BlockGroup GetNextBlockGroup()
    {
        Vector2 spawnPos = new Vector2(0,4);   
        BlockGroup nB = Instantiate(nextBlockGroup, spawnPos, Quaternion.identity);
        PickNextBlockGroup();

        return nB;
    }

    private void PickNextBlockGroup()
    {
        nextBlockGroup = availableBlockGroups[Random.Range(0, availableBlockGroups.Count - 1)];
    }
}