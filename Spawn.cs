using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{

    private GameObject tile;
    public GameObject[][] grid;
    public int widthIn, heightIn;
    public bool startSelect = false, endSelect = false;
    // Start is called before the first frame update
    void Start()
    {
        //find the tile prefab from storage
        tile = GameObject.Find("storage").GetComponent<Storage>().tile;
    }

    //instantiates a new tile at location x,y
    void newTile(int x, int y)
    {
        Instantiate(tile, new Vector2(x, y), tile.transform.rotation);
    }

    /*---
     *activates when button is clicked
     * takes values from width and height input boxes
     * creates a array called Grid makes it the correct size based on the inputs
     * cycles through width and height and instantiates the tiles creating the grid
     * removes the text input boxes and the start button
     */
    public void button()
    {
        widthIn = int.Parse(GameObject.Find("width").GetComponent<InputField>().text) + 2;
        heightIn = int.Parse(GameObject.Find("height").GetComponent<InputField>().text) + 2;

        grid = new GameObject[widthIn][];

        for (int z = 0; z < widthIn; z++)
        {
            grid[z] = new GameObject[heightIn];
            for(int y =0; y< heightIn; y++)
            {
                grid[z][y] = null;
            }
        }

            for (int i = 0; i < widthIn; i++)
            for (int j = 0; j < heightIn; j++)
            {
                newTile(i, j);
            }

        
        GameObject.Find("Text1").GetComponent<Text>().text = "Choose Start Position";
        Destroy(GameObject.Find("width"));
        Destroy(GameObject.Find("height"));
        Destroy(GameObject.Find("Button"));
    }

    
}
