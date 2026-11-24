using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockGroup : MonoBehaviour
{
    [SerializeField] public List<SingleBlock> blocks;

    [SerializeField] private float fallingTime = 1;
    private bool  isPlaced = false;

    private float fallingTimeModifier = 1;
    private float timeKeeper = 0;

    public static Action<BlockGroup> GroupPlacedEvent;

    #region DEBUG ONLY
    public bool DEBUGSpeedUp = false;

    private void OnValidate()
    {
        if (DEBUGSpeedUp) fallingTimeModifier = 0.1f;
        else              fallingTimeModifier = 1;
    }
    #endregion

    public void SetFallingSpeedModifier(float v) { fallingTimeModifier = v; }

    private void CheckIfPlaced()
    {
        foreach (SingleBlock b in blocks)
            if (b.GetIsPlaced())
            {
                if (!isPlaced) GroupPlacedEvent?.Invoke(this);
                isPlaced = true;
                foreach (SingleBlock block in blocks) block.SetIsPlaced(isPlaced);
            }
    }
    private void Update()
    {
        if (isPlaced) return;

        timeKeeper += Time.deltaTime;
        if (timeKeeper >= fallingTime * fallingTimeModifier)
        {
            timeKeeper -= timeKeeper;
            foreach (SingleBlock b in blocks)  b.Fall();
        }

        CheckIfPlaced();
    }

    public void Rotate(bool isClockwise)
    {
        if (isClockwise) transform.Rotate(0, 0, 90);
        else             transform.Rotate(0, 0, -90);
    }

    public void Move(bool isRight)
    {
        foreach (SingleBlock b in blocks) b.Move(isRight);
    }
}
