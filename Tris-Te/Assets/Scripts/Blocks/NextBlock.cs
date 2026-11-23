using System.Collections.Generic;
using UnityEngine;

public class NextBlock : MonoBehaviour
{
    [SerializeField] private BlockGroup nextBlockGroup;
    [SerializeField] private List<BlockGroup> availableBlockGroups = new List<BlockGroup>();

    private void Start()
    {
        PickNextBlockGroup();
    }

    public BlockGroup GetNextBlockGroup()
    {
        BlockGroup nB = Instantiate(nextBlockGroup);
        nB.gameObject.SetActive(false);
        PickNextBlockGroup();

        return nB;
    }

    private void PickNextBlockGroup()
    {
        nextBlockGroup = availableBlockGroups[Random.Range(0, availableBlockGroups.Count - 1)];
    }
}
