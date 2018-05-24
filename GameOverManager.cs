using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;


    Animator anim;
    bool isPlayable = false;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            // Set isPlayable to true
            isPlayable = true;
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R) && isPlayable)
        {
            SceneManager.LoadScene(0);
        }
    }
}
