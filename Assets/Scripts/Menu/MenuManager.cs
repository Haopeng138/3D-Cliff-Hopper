using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene("TestLevel");
    }

    public void Credits ()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Instructions ()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
