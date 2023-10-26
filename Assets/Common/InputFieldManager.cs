using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class InputFieldManager : MonoBehaviour
{
    //出力用のテキスト
    public TMP_InputField Field;
    public new string name =" ";

    private void Start()
    {
        GetComponent<TMP_Text>().text = "ユーザー名:ゲスト";
    }

    //inputFieldのOnEndEditに設定する用の関数
    public void OnValueChanged()
    {
        string input = Field.GetComponent<TMP_InputField>().text;
        GetComponent<TMP_Text>().text = "ユーザー名:"+input;
        name = input;
    }
}
