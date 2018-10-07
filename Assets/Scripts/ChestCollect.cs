using UnityEngine;
using System.Collections;

public class ChestCollect : MonoBehaviour
{
    [SerializeField] Sprite lootedSprite;
    [SerializeField] GameObject collectable;
    [SerializeField] bool isLooted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isLooted)
        {
            OpenChest();
            isLooted = true;
            Invoke("DestroyChest", 1f);
        }
    }

    void OpenChest ()
    {
        GetComponent<SpriteRenderer>().sprite = lootedSprite;
        Instantiate (collectable, transform);
    }

    void DestroyChest()
    {
        Destroy(gameObject);
    }
}
