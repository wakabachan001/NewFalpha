using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�}�b�v�����p�N���X
//�J�����ɃA�^�b�`
public class CreateMap : MonoBehaviour
{
    public bool onNext = true;  //���}�b�v�ɐi�񂾂Ƃ�true
    private int mapCount = 0;   //���}�b�v�ڂ��ǂ���
    private int shopRand;       //���l�o�������p
    private int mapWidth = 5;   //�}�b�v�̕�
    public int mapHeight = 22;
    public float cameraPosX = 2;    //�J�����̏����ʒu
    public float cameraPosY = 2;
    private float cameraPosZ = -10;
    public int mapEnemyMinY = 4;  //�G�����������͈�
    public int mapEnemyMaxY = 19;

    public GameObject floorTiles;
    public GameObject[] enemyObj = new GameObject[2];//��������G�I�u�W�F�N�g
    public int[] enemyCount = new int[2]; //��������G�̐�

    //�I�u�W�F�N�g�̈ʒu����ۑ�����ϐ�
    private Transform boardHolder;

    private int start;
    private int end;
    Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        shopRand = Random.RandomRange(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (onNext)
        {
            //onNext���I���ɂȂ����Ƃ��A�N���[���S�폜
            mapCount++;
            if(mapCount < 5)
            {
                //�}�b�v����
                MapCreate();
            }
            else
            {
                //�{�X�}�b�v����
            }
            onNext = false;
        }
    }

    //�}�b�v�����֐�
    private void MapCreate()
    {
        BoardSetup();

        start = mapEnemyMinY * mapWidth;
        end = mapEnemyMaxY * (mapWidth + 1);

        List<int> mapNum = new List<int>();

        for (int i = start; i < end; i++)
            mapNum.Add(i);

        for (int i = 0; i < enemyCount.Length; i++)
        {
            while (enemyCount[i]-- > 0)
            {
                int index = Random.RandomRange(0, mapNum.Count);

                int rand = mapNum[index];
                //������mapNum[index]���폜����

                pos.x = rand % 5;
                pos.y = rand / 5;

                //�G�N���[������ pos
                Instantiate(enemyObj[i], pos, Quaternion.identity);
            }

        }

        //���X����

        //�J�������ړ�
        transform.position = new Vector3(cameraPosX, cameraPosY, cameraPosZ);
    }

    //����z�u
    void BoardSetup()
    {
        // Board�Ƃ����I�u�W�F�N�g���쐬���Atransform����boardHolder�ɕۑ�
        boardHolder = new GameObject("Board").transform;

        // ��5�}�X
        for (int x = 0; x < mapWidth; x++)
        {
            //�@�����P�`�Q�O�����[�v
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject toInstantiate = floorTiles;


                //���𐶐����Ainstance�ϐ��Ɋi�[
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f),
                            Quaternion.identity) as GameObject;

                //��������instance��Board�I�u�W�F�N�g�̎q�I�u�W�F�N�g�Ƃ���
                instance.transform.SetParent(boardHolder);
            }
        }
    }
}
