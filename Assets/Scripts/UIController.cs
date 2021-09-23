using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public BlockManager BlockManager;

    void Start(){
        BlockManager = GameObject.Find("Block Manager").GetComponent<BlockManager>();
    }

    public void Next(){
        BlockManager.newNumber();
    }

    public void Reset(){
        BlockManager.ResetBlocks();
    }


    public void ExitButton(){
        Application.Quit();
    }
}
