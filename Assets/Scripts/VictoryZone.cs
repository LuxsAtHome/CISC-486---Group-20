using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryZone : MonoBehaviour
{
    [SerializeField] private string victorySceneName = "VictoryScene";

    private void OnTriggerEnter(Collider other)
    {
        // Only trigger when the Player enters
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(victorySceneName);
        }
    }
}
