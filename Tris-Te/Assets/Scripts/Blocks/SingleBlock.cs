using System;
using UnityEngine;

public class SingleBlock : MonoBehaviour
{
    public static Action<SingleBlock> PlacedEvent;
    private bool isPlaced;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool GetIsPlaced() { return isPlaced; }
    public void SetIsPlaced(bool v) { isPlaced = v; }

    public void Fall()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
    }

    public void Move(bool isRight)
    {
        if (isRight) transform.position = new Vector3(transform.position.x + 1,
                                                      transform.position.y, transform.position.z);
        else transform.position = new Vector3(transform.position.x - 1,
                                                      transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall") return;

        if (collision.gameObject.TryGetComponent(out SingleBlock singleBlock))
            if (!singleBlock.isPlaced) return;

        PlacedEvent?.Invoke(this);
        isPlaced = true;

        rb.bodyType = RigidbodyType2D.Static;
    }
}
