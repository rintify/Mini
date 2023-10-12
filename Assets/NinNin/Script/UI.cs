using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinNin{
public class UI : MonoBehaviour
{
    public Block[] blocks;
    public GameObject gameOverUI;
    public GameObject gameClearUI;
    public GameObject destroy_ball;
    private bool isGameClear = false;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
          if( isGameClear != true)
        {
            if( DestroyAllblocks())
            {
                //ゲームクリア
                Debug.Log("ゲームクリア");
                isGameClear = true;
                Destroy(destroy_ball);
            }
        }
    }

    //ブロック全部消えた？ 
    private bool DestroyAllblocks()
    {
         foreach(Block b in blocks)
        {
            if( b != null )
            {
                 return false;
            }
        }
        return true;
    }


}
}
