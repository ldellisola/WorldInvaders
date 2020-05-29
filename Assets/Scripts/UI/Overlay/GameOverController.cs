using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public BasePanel gameOverPanel;
    public EnemyGenerator EnemyGenerator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (EnemyGenerator.KiledAllEnemies())
        {
            gameOverPanel.OpenPanel();
        }
    }


    public void ButtonClick_TryAgain()
    {
        gameOverPanel.ClosePanel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }

    public void ButtonClick_MainMenu()
    {
        SceneManager.LoadScene("MenuGUI",LoadSceneMode.Single);

    }
}
