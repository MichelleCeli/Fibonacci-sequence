using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockManager : MonoBehaviour
{
   [Header("GameObjects")]

   public GameObject blockPrefab;

   public GameObject blockContainer;

   public TextMeshProUGUI Number;
   
   public GameObject Warning;

   Vector3 scaleChange;

   int counter;

   int fn_2;

   int fn_1;

   int fn;

   int currentFibNumber;

   Vector3 StartPoint;

   Vector3 NextPoint;

   Vector3 CubeCoordinates;

   private enum Direction {North, East, South, West}

   private Direction direction = Direction.South;

   public GameObject Camera;

   

   void Start()
   {
        initializeNumbers();

       
        Warning.SetActive(false);
        Camera = GameObject.Find("Main Camera");
      
      
   }

   public void newNumber()
   {
       if (counter < 2){
           
        currentFibNumber = 1;
         
        Number.text = currentFibNumber.ToString();
        SetBlock();
           
       } else if(counter < 8)
       {
        fn_2 = fn_1;
        fn_1 = fn;
        fn = fn_1 + fn_2;
        currentFibNumber = fn; 

        Number.text = currentFibNumber.ToString();
        SetBlock();

        Camera.GetComponent<CameraMovement>().MoveCamera(fn);

       } else if(counter > 7){
           Debug.Log("no more numbers");
           Warning.SetActive(true);
       }
        
        counter++;
       
   }
   

    public void SetBlock()
    {
        Debug.Log("before GameObject instatiation");
        GameObject gameObj = Instantiate(blockPrefab) as GameObject;
        Debug.Log("after GameObject instatiation");
        Block block = gameObj.GetComponent<Block>();

        if (counter < 2){

        block.GetComponent<Block>().FibNumber = 1; 

        gameObj.transform.position = new Vector3(counter,10,0);
        gameObj.transform.parent = blockContainer.transform;

        } else if (counter < 8) {
      
        // Starting in South-Direction
        // Startpoint: -0.5, 0, -0.5

        if(direction == Direction.South)
        NextPoint = new Vector3(StartPoint.x + fn, 0, StartPoint.z - fn);
        else if (direction == Direction.East)
        NextPoint = new Vector3(StartPoint.x + fn, 0, StartPoint.z + fn);
        else if (direction == Direction.North)
        NextPoint = new Vector3(StartPoint.x - fn, 0, StartPoint.z + fn);
        else if (direction == Direction.West)
        NextPoint = new Vector3(StartPoint.x - fn, 0, StartPoint.z - fn);
        
        CubeCoordinates = (NextPoint + StartPoint)/2;



        block.FibNumber = currentFibNumber;

        gameObj.transform.position = new Vector3(CubeCoordinates.x, 10, CubeCoordinates.z);

        scaleChange = new Vector3(currentFibNumber, currentFibNumber, currentFibNumber);
        gameObj.transform.localScale = scaleChange;  

        gameObj.transform.parent = blockContainer.transform;

        Debug.Log("Before Change Direction, direction: " + direction);
        direction = changeDirection(direction);
        Debug.Log("After Change Direction: " + direction);
        StartPoint = NextPoint;

        } 

      
        
      
       
    }

    void initializeNumbers()
    {
    counter = 0;
       fn_2 = 0;
       fn_1 = 1;
       fn = 1;
       currentFibNumber = 0;

       StartPoint = new Vector3(-0.5f, 0, -0.5f);

       direction = Direction.South;

    }



    private Direction changeDirection(Direction dir){

        Debug.Log("in change direction: " + dir); 
        if(dir == Direction.North)
            dir = Direction.West;
            else if(dir == Direction.West)
            dir = Direction.South;
            else if (dir == Direction.South)
            dir = Direction.East;
            else if (dir == Direction.East) {
                dir = Direction.North;
            }

            return dir;

    }
    public void ResetBlocks(){

        Warning.SetActive(false);
        Camera.GetComponent<CameraMovement>().ResetCamera();
    
        initializeNumbers();

        Number.text = currentFibNumber.ToString();

        int childCount = blockContainer.transform.childCount;

        for (int i = childCount-1; i >= 0; i--)
        {
            DestroyImmediate(blockContainer.transform.GetChild(i).gameObject);
        }
    }

    
}
