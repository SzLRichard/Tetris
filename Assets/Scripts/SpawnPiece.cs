using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SpawnPiece : MonoBehaviour
{
    private GameObject TempPiece;
    public GameObject[] Tetrominoes;

    public GameObject GameOverScreen;

    public Text pointsText;

    int points;
    void Shuffle7bag()
    {
        
        for (int x = 0; x <= 6; x++)
        {

            for (int y = 0; y <= 6; y++)
            {

                if (Random.Range(0, 2) == 0)
                {

                    TempPiece = Tetrominoes[x];
                    Tetrominoes[x] = Tetrominoes[y];
                    Tetrominoes[y] = TempPiece;
                    
                    
                }
            }

        }


    }

    
    
    int i = 0;
    void Start()
    {
        Shuffle7bag();
        NewTetromino(0);
        
    }

    public void NewTetromino(int score)
    {
        points += score;
        pointsText.text = points.ToString();
        if (i > 6)
        {
            Shuffle7bag();
            i = 1;
            Instantiate(Tetrominoes[0], transform.position, Quaternion.identity);
        }
        else { 
        Instantiate(Tetrominoes[i], transform.position, Quaternion.identity);
        i += 1; }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitButton() {

        Application.Quit();
    
    }


    public void Over(int score) {
       
        GameOverScreen.SetActive(true);
        
    
    
    }


}

