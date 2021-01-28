using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinder : MonoBehaviour
{

    public Vector2 startLocation;
    public Vector2 endLocation;
    private int[] next = new int[2];
    private GameObject[] surrounding = new GameObject[8];
    public List<GameObject> checkedTiles = new List<GameObject>();
    private bool start = false, end = true; 
    Vector2 nextTile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /* on mouse click resets checkedtiles list
     * on mouse click runs Astar function setting the start tile as first tile
     */
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            checkedTiles = new List<GameObject>();
            start = false;
        }

        if (Input.GetMouseButtonUp(0) && GameObject.Find("Spawner").GetComponent<Spawn>().endSelect == true)
        {
            nextTile = startLocation;
            Astar();
            start = true;
            end = false;   
        }
    }

    
    /*----
     * Runs the Astar algorithm 
     * checks surroudings tiles of the current tile
     * puts them in an array 
     * updates cost of tiles if lower cost is found
     * 
     */
    void Astar()
    {
        int x = (int)nextTile.x;
        int y = (int)nextTile.y;
        
        GameObject.Find("Spawner").GetComponent<Spawn>().grid[x][y].GetComponent<tileType>().isChecked = true;

        surrounding[0] = GameObject.Find("Spawner").GetComponent<Spawn>().grid[x+1][y];
        surrounding[1] = GameObject.Find("Spawner").GetComponent<Spawn>().grid[x-1][y];
        surrounding[2] = GameObject.Find("Spawner").GetComponent<Spawn>().grid[x][y+1];
        surrounding[3] = GameObject.Find("Spawner").GetComponent<Spawn>().grid[x][y-1];
        surrounding[4] = GameObject.Find("Spawner").GetComponent<Spawn>().grid[x+1][y-1];
        surrounding[5] = GameObject.Find("Spawner").GetComponent<Spawn>().grid[x+1][y+1];
        surrounding[6] = GameObject.Find("Spawner").GetComponent<Spawn>().grid[x-1][y-1];
        surrounding[7] = GameObject.Find("Spawner").GetComponent<Spawn>().grid[x-1][y+1];

        for(int i = 0; i < 8; i++)
        {
            float temp1 = GameObject.Find("Spawner").GetComponent<Spawn>().grid[x][y].GetComponent<tileType>().weight1
                    + Vector3.Distance(surrounding[i].transform.position, GameObject.Find("Spawner").GetComponent<Spawn>().grid[x][y].transform.position);
            if (surrounding[i].GetComponent<tileType>().isObstacle == false && surrounding[i].GetComponent<tileType>().isChecked == false) {
                
                surrounding[i].GetComponent<tileType>().weight1 = temp1;
                surrounding[i].GetComponent<tileType>().next = GameObject.Find("Spawner").GetComponent<Spawn>().grid[x][y];

                surrounding[i].GetComponent<Renderer>().material.color = new Color(0, 0, 0.5f);
                surrounding[i].GetComponent<tileType>().isChecked = true;
                
                checkedTiles.Add(surrounding[i]);
            }

            if (temp1 < surrounding[i].GetComponent<tileType>().weight1)
            {
                surrounding[i].GetComponent<tileType>().weight1 = temp1;
                surrounding[i].GetComponent<tileType>().next = GameObject.Find("Spawner").GetComponent<Spawn>().grid[x][y];
            }
        }
        
        Invoke("sort", 0.01f);

    }

    /*----
     * goes through checked tiles to the one with the lowest cost
     * if it is not the end runs the Astar again
     * if it is the end calls function to trace the path back
     */
    void sort()
    {
        float temp = checkedTiles[0].GetComponent<tileType>().weight;
        float temp2 = checkedTiles[0].GetComponent<tileType>().weight2;
        int next = 0;

        for (int i = 0; i < checkedTiles.Count; i++)
        {
            if (checkedTiles[i].GetComponent<tileType>().weight < temp || (checkedTiles[i].GetComponent<tileType>().weight == temp && checkedTiles[i].GetComponent<tileType>().weight2 < temp2))
            {
                next = i;
                temp = checkedTiles[i].GetComponent<tileType>().weight;
                temp2 = checkedTiles[i].GetComponent<tileType>().weight2;
            }
        }

        checkedTiles[next].GetComponent<Renderer>().material.color = new Color(0, 0.5f, 0.5f);
        int nextX = (int)checkedTiles[next].GetComponent<tileType>().transform.position.x;
        int nextY = (int)checkedTiles[next].GetComponent<tileType>().transform.position.y;

        checkedTiles.Remove(checkedTiles[next]);
        nextTile = new Vector2(nextX, nextY);

        if (nextTile != endLocation)
            Invoke("Astar", 0.01f);
        else
        {
            GameObject.Find("Spawner").GetComponent<Spawn>().grid[(int)endLocation.x][(int)endLocation.y].GetComponent<tileType>().Path();
            end = true;
        }
    }

}
