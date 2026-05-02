using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameMaster.instance.SetChechPoint(transform.position);

            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
