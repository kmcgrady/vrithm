using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    void Start ()
    {
    }
    
    void Update ()
    {
    }

    public void LoadScene()
    {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MathRoom");
    }
}