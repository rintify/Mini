using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClearCheck : MonoBehaviour
{
    /// <summary>
    /// ”»’è“à‚ÉƒvƒŒƒCƒ„[‚ª‚¢‚é
    /// </summary>
    [HideInInspector] public bool isOn2 = false;

    private string playerTag = "Player";

    #region//ÚG”»’è
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