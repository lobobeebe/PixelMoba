using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Health PlayerHealth;

    private void Start()
    {
        if (PlayerHealth == null)
        {
            Debug.LogError("PlayerHealth null. Cannot change scenes.");
            return;
        }

        PlayerHealth.OnDeath += OnPlayerDeath;
    }
    
    void OnPlayerDeath()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
}
