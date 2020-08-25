using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private Health _PlayerHealth;

    [SerializeField]
    private Health _PlayerBaseHealth;

    [SerializeField]
    private Health _EnemyBaseHealth;

    private void Start()
    {
        if (_PlayerHealth == null)
        {
            Debug.LogError("PlayerHealth null. Cannot change scenes.");
            return;
        }

        if (_PlayerBaseHealth == null)
        {
            Debug.LogError("PlayerBaseHealth null. Cannot change scenes.");
            return;
        }

        if (_EnemyBaseHealth == null)
        {
            Debug.LogError("EnemyBaseHealth null. Cannot change scenes.");
            return;
        }

        _PlayerHealth.OnDeath += LoadStartMenu;
        _PlayerBaseHealth.OnDeath += LoadStartMenu;
        _EnemyBaseHealth.OnDeath += LoadStartMenu;
    }
    
    void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
}
