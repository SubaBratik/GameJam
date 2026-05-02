using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public Vector3 lastCheckPointPos;

    public int foodCollected;
    public int totalFoodInLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");
        totalFoodInLevel = foods.Length;
        foodCollected = 0;
    }
    

    public void ResetLevelData()
    {
        foodCollected = 0;
        totalFoodInLevel = GameObject.FindGameObjectsWithTag("Food").Length;
    }

    public void SetChechPoint(Vector3 pos)
    {
        lastCheckPointPos = pos;
    }

}
