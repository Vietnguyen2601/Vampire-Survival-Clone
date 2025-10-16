using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SrceenChange(string name)
    {
        SceneManager.LoadScene(name);
    }
}
