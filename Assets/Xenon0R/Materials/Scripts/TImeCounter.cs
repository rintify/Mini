using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
namespace XenonAnd{
    public class SampleScene : MonoBehaviour
{
    //�J�E���g�_�E��
    public float countdown = 7.00f;
 
    //���Ԃ�\������Text�^�̕ϐ�
    public TMP_Text timeText;
 
    //�|�[�Y���Ă��邩�ǂ���
    private bool isPose = false;
 
    // Update is called once per frame
    void Update()
    {
        //�N���b�N���ꂽ�Ƃ�
        if(Input.GetMouseButtonDown(0))
        {
            //countdown��0��؂��Ă��Ȃ��Ƃ�
            if(countdown >= 0)
            {
                //�|�[�Y���ɃN���b�N���ꂽ�Ƃ�
                if(isPose)
                {
                     //�|�[�Y��Ԃ���������
                     isPose = false;
                 }
                //�i�s���ɃN���b�N���ꂽ�Ƃ�
                else    {
                     //�|�[�Y��Ԃɂ���
                     isPose = true;
                }
            }
        }
 
        //�|�[�Y�����ǂ���
        if (isPose)
        {
            //�J�E���g�_�E�����Ȃ�
            return;
        }
 
        //���Ԃ��J�E���g����
        countdown -= Time.deltaTime;
 
        //���Ԃ�\������
        timeText.text = countdown.ToString("f2");
 
        //countdown��0�ȉ��ɂȂ����Ƃ�
        if (countdown <= 0)
        {
            timeText.text = "Time Over";
        }
    }
}
}
