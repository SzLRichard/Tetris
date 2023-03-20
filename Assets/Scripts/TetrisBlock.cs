using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int width=10;
    public static int height=20;
    private static Transform[,] grid = new Transform[width, height];

    int score;
  

/*    
    public void Setup(int score)
    {

        gameObject.SetActive(true);
        pointsText.text = score.ToString();
    }
*/
    void QuickDrop() {
        while (ValidMove()) {
            transform.position += new Vector3(0, -1, 0);
        }
        transform.position += new Vector3(0, 1, 0);
        AddToGrid();
        CheckForLines();
        this.enabled = false;
        if (!IsOver())
            FindObjectOfType<SpawnPiece>().NewTetromino(score);
        else FindObjectOfType<SpawnPiece>().Over(score);
        //Sometimes creates duplicate tetrominoes
    }

    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove()) transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove()) transform.position -= new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {

            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if (!ValidMove())
            {   transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                {
                    transform.position += new Vector3(-2, 0, 0);
                    if (!ValidMove()) {
                        transform.position += new Vector3(1, 1, 0);
                        if (!ValidMove())
                        {
                            transform.position += new Vector3(0, -1, 0);
                            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                        }

                        
                    }

                }
                
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            QuickDrop();
        }
        else fallTime = 0.8f;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            fallTime = 0.08f;
        }
        else fallTime = 0.8f;
        
        if (Time.time - previousTime >= fallTime) {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                if (!IsOver())
                    FindObjectOfType<SpawnPiece>().NewTetromino(score);
                else FindObjectOfType<SpawnPiece>().Over(score);

            }
            previousTime = Time.time;
        }
    }
    bool IsOver() {

        for (int x = 0; x < width; x++) {
            if (grid[x, 18] != null)
                return true;
           
        }
        return false;
    }
    void CheckForLines() {

        for (int i = height-1;  i >= 0;i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    
    }

    bool HasLine(int i) { 
    for(int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;

        }
        return true;

    }

    void DeleteLine(int i) {

        for (int j = 0; j < width; j++)
        {

            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
        score = 1;
    }
    void RowDown(int i) {

        for (int y = i; y < height; y++) {

            for (int j = 0; j < width; j++) {

                if (grid[j, y] != null) {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }  
        }
    }




    void AddToGrid() {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool ValidMove() {
        foreach (Transform children in transform) {

            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height){
                return false;
            }

            if (grid[roundedX, roundedY] != null)
                return false;
        }

        return true;
    }
    

}
