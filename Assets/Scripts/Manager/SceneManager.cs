using UnityEngine.SceneManagement;

public static class SceneManagement
{
    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
