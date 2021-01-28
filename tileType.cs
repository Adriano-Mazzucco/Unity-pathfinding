using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tileType : MonoBehaviour
{
    public bool set = false, isStart = false, isEnd = false, isObstacle = false, isChecked = false;
    private GameObject[][] grid;
    public float weight, weight1 = 0, weight2;
    public GameObject next; 

   
    void Start()
    {
        //adds this tile to the grid array based on its location
        GameObject.Find("Spawner").GetComponent<Spawn>().grid[(int)this.transform.position.x][(int)this.transform.position.y] = this.gameObject;
   
    }

    /*----
     * sets the outer tiles as obstacles to set the outer walls
     * distance from tile to the destination, this is used as heuristic cost
     * if not obstacle determines total cost, otherwise sets cost to infinite
     * sets color of tile depending on if it is start/end/wall/walkable
     */
    void Update()
    {

        int width = GameObject.Find("Spawner").GetComponent<Spawn>().widthIn;
        int height = GameObject.Find("Spawner").GetComponent<Spawn>().heightIn;


        if (this.transform.position.x == 0 || this.transform.position.y == 0 || this.transform.position.x == width-1 || this.transform.position.y == height-1)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
            isObstacle = true;
            set = true;
        }

        weight2 = Vector3.Distance(this.transform.position, GameObject.Find("Pathfinder").GetComponent<pathfinder>().endLocation);

        if (isObstacle == false)
            weight = weight1 + weight2;
        else
            weight = Mathf.Infinity;

        if (Input.GetMouseButtonDown(0) && isObstacle == false)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
            isChecked = false;
            weight1 = 0;
        }

       if(isEnd)
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0);

        if (isStart)
            this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);

    }
    /*----
     * sets tiles type 
     * set to start if first tile clicked
     * set to end if second tile clicked
     * all other tiles are set to walkable/obstacle if clicked after start and end are set
     */
    private void setTile()
    {
        
        if (GameObject.Find("Spawner").GetComponent<Spawn>().startSelect == false && set == false)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
            GameObject.Find("Spawner").GetComponent<Spawn>().startSelect = true;
            GameObject.Find("Text1").GetComponent<Text>().text = "Choose End Position";
            GameObject.Find("Pathfinder").GetComponent<pathfinder>().startLocation = this.transform.position;
            isStart = true;
            set = true;
        }
        else if (GameObject.Find("Spawner").GetComponent<Spawn>().endSelect == false && set == false && isStart == false)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
            GameObject.Find("Spawner").GetComponent<Spawn>().endSelect = true;
            GameObject.Find("Text1").GetComponent<Text>().text = "Choose obstacle locations";
            GameObject.Find("Pathfinder").GetComponent<pathfinder>().endLocation = this.transform.position;
            isEnd = true;
            set = true;
        }
        else if (set == false && isStart == false && isEnd == false)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
            isObstacle = true;
            set = true;   
        }else if(isStart == false && isEnd == false)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
            isObstacle = false;
            set = false;
        }
        
    }

    private void OnMouseDown()
    {
        setTile();
    }

    /*
     * changes color highlighting path 
     * used to trace back once path is found 
     */
    public void Path()
    {
        this.gameObject.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0);
        if(isStart == false)
            next.GetComponent<tileType>().Path();
    }

    private void OnMouseEnter()
    {
        if(Input.GetMouseButton(0))
            setTile();
    }
    
        
     


}
