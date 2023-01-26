using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPiece : MonoBehaviour
{
    private GameObject TempPiece;
    public GameObject[] Tetrominoes;

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
        NewTetromino();
        
    }

    public void NewTetromino()
    {

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

}

