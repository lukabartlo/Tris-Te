using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Block currentBlock;
    private int score;

    private void IncrementScore(int amount)
    {
        score += amount;
    }

    private void OnBlockPlaced(Block newBlock)
    {
        currentBlock = newBlock;
    }

    public void MoveBlock(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 inputValue = context.ReadValue<Vector2>();
        }
    }

    public void RotateBlock(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 inputValue = context.ReadValue<Vector2>();
        }
    }

    public void AccelerateFall(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
    }

    public void OpenPauseMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
    }
}
