using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneChanger : MonoBehaviour
{
    public void OnStartClicked()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
