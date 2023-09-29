using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClearCheck : MonoBehaviour
{
    /// <summary>
    /// 判定内にプレイヤーがいる
    /// </summary>
    [HideInInspector] public bool isOn2 = false;

    private string playerTag = "Player";

    #region//接触判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            isOn2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            isOn2 = false;
        }
    }
    #endregion
}