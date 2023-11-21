using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class skill : MonoBehaviour
{
    public GameObject OriginObjct;//�I���W�i���̃I�u�W�F�N�g

    private bool skillused = false;

    private Button button;
    private Color normalColor  = Color.white; //�m�[�}���J���[
    private Color pressedlColor = Color.gray; //�v���X�h�J���[


    public float right = 2.0f;
    public float time = 5.0f;
    public float cooldownTime = 10.0f;



    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(ChangeColor);
    }

    public void OnClick()
    {

        if (!skillused)
        {
            StartCoroutine(Startskill());

            GameObject a;
            GameObject b;

            //���ݒn���擾
            Vector3 currentPositione = transform.position;


            //�N���[���쐬
            a = Instantiate(OriginObjct, OriginObjct.transform.position + (transform.right * right), Quaternion.identity);

            //�N���[���쐬
            b = Instantiate(OriginObjct, OriginObjct.transform.position + (transform.right * right * -1.0f), Quaternion.identity);


            //5�b��ɏ���
            Destroy(a, time);
            Destroy(b, time);


        }
    }

    private void ChangeColor()
    {
        //�{�^���̃J���[��ύX
        ColorBlock colors = button.colors;
        colors.selectedColor = pressedlColor;
    
        button.colors     = colors;
        EditorUtility.SetDirty(button);

        //�{�^�����ēx���̃J���[�ɖ߂����߂̃R���[�`�����Ăяo����
        StartCoroutine(ResetColor());
    }
    private IEnumerator ResetColor()
    {

       
        yield return new WaitForSeconds(10.0f);//10�b��Ɍ��̃J���[�ɖ߂�
        ColorBlock colors = button.colors;
        colors.selectedColor = normalColor;
        button.colors = colors;

        EditorUtility.SetDirty(button);




    }
    IEnumerator Startskill()
    {
        skillused = true;

        yield return new WaitForSeconds(cooldownTime);

        skillused = false;
    }
}
