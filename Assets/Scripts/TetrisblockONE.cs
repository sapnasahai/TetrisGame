using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TetrisblockONE : MonoBehaviour
{
     


    public Vector3 rotationPoint;
    private float previousTime;      // global variable for vertical movement 
    public float fallTime = 0.8f;   //global variable for vertical movement

    public static int height = 20;    // static means same variable value for everyone
    public static int width = 10;

    private static Transform[,] grid = new Transform[width, height];





    
    

    // Start is called before the first frame update
    void Start()
    {

        if (!ValidMove())
        {

            Debug.Log("Game Over");
            

            Destroy(gameObject);
            
        }

    }








    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))              // for horizontal left and right movement 
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);       // for horizontal left and right movement
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }







        // falltime is the time before trteomino move down , Time.time is current time
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<SpawnerTetro>().NewTetromino();
               

            }
            previousTime = Time.time;

        }





        

    }

    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }
    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }





    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }



    bool ValidMove()
    {
        foreach (Transform children in transform)     // browsw all the childern of tetrominos(4)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);   // check the position of x and y tetromino and theor childern(1)
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)  //if one is bigger than size of grid (2)
            {
                return false;       /// return false(3)
            }
            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;    // return true(5) if none is bigger than size of grid
    }

    
        


         


}
