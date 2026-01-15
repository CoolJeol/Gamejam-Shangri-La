using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMenus : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    public GameObject CreditsContainer;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CreditsContainer.SetActive(false);
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        CreditsContainer.SetActive(true);
    }
}
