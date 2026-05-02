using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] TMP_Text resultText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.DisableMovement();



            _loadingScreen.SetActive(true); 
            int collected = GameMaster.instance.foodCollected;
            int total = GameMaster.instance.totalFoodInLevel;

            resultText.text = $"Вы собрали: {collected} из {total} очков!";

            Invoke("LoadNextLevel", 3f); // Через 3 секунды загружается следующий уровень
        }
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
