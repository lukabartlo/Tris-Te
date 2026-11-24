using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float blockSpeedModifier;
    [SerializeField] private NextBlock nextBlock;
    private BlockGroup currentBlock;
    private int score;

    private void Start()
    {
        currentBlock = nextBlock.GetNextBlockGroup();
        BlockGroup.GroupPlacedEvent += OnBlockPlaced;
    }

    private void IncrementScore(int amount)
    {
        score += amount;
    }

    private void OnBlockPlaced(BlockGroup newBlock)
    {
        currentBlock = nextBlock.GetNextBlockGroup();
    }

    public void MoveBlock(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 inputValue = context.ReadValue<Vector2>();
            currentBlock.Move(inputValue.x > 0);
        }
    }

    public void RotateBlock(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 inputValue = context.ReadValue<Vector2>();
            currentBlock.Rotate(inputValue.y > 0);
        }
    }

    public void AccelerateFall(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentBlock.SetFallingSpeedModifier(blockSpeedModifier);
        }
        if (context.canceled)
        {
            currentBlock.SetFallingSpeedModifier(1);
        }
    }

    public void OpenPauseMenu(InputAction.CallbackContext context)
    {
        Debug.LogWarning("This function has not yet been implemented.");
        if (context.started)
        {

        }
    }
}
