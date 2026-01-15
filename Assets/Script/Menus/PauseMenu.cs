using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject Container;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Container.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void resumeButton()
    {
        Container.SetActive(false);
        Time.timeScale = 1;
    }

    public void menuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
