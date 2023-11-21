using UnityEngine;
using System;
//List���g������
using System.Collections.Generic;
//Random���g������
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

    //�J�E���g�p�̃N���X��ݒ�
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    // 20*5�̃Q�[���{�[�h�����̂ŏc�̒i���Q�O���̗���T
    public int columns = 5;
    public int rows = 20;
    public float adjustment = 0.2f;//�I�u�W�F�N�g�ʒu�����p

    public Count enemyCount = new Count(14, 20);

    public GameObject   floorTiles;
    public GameObject   BossTiles;
    public GameObject[] enemyTiles;

    //�I�u�W�F�N�g�̈ʒu����ۑ�����ϐ�
    private Transform boardHolder;

        //�I�u�W�F�N�g��z�u�ł���͈͂�\�����X�g
        //�k�������͉ό^�̔z��
        private List<Vector3> gridPositions = new List<Vector3>();

        //�G�L������z�u�ł���͈͂�����
        void InitialiseList()
        {
            //gridPosition���N���A
            gridPositions.Clear();
            //gridPosition�ɃI�u�W�F�N�gw�z�u�\�͈͂��w��
            //�@�����P�`�T�����[�v
            for (int x = 1; x <= columns; x++) 
            {
                // �����T�`�P�W�����[�v
                for (int y = 5; y < rows - 1; y++) 
                {
                    //�@�T*�P�W�͈̔͂�gridPosition�Ɏw��
                    gridPositions.Add(new Vector3(x, y + adjustment, 0f));
                }
            }

        }

    //����z�u
    void BoardSetup()
    {
        // Board�Ƃ����I�u�W�F�N�g���쐬���Atransform����boardHolder�ɕۑ�
        boardHolder = new GameObject("Board").transform;

        // �����P�`�W�����[�v
        for (int x = 1; x < columns + 1; x++)
        {
            //�@�����P�`�Q�O�����[�v
            for (int y = 1; y < rows + 1; y++)
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

    Vector3 RandomPosition()
    {
        // �O�`�R�U�`�����_���łP���肵�A�ʒu�����m��
        int randomIndex = Random.Range(0, gridPositions.Count);

        Vector3 randomPosition = gridPositions[randomIndex];
        //�����_���Ō��肵�����l�͍폜
        gridPositions.RemoveAt(randomIndex);
        //�m�肵���ʒu����Ԃ�
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray,int minimum,int maximum)
    {
        //�Œ�l�`�ő�l�{�P�̃����_���񐔕��������[�v
        int objectCount = Random.Range(minimum, maximum + 1);
        for (int i = 0; i < objectCount; i++)
        {
            //gridPosition����ʒu������擾
            Vector3 randomPosition = RandomPosition();
            //����tileArray���烉���_���ň�I��
            GameObject tileChoise = tileArray[Random.Range(0, tileArray.Length)];
            //�����_���Ō��肵����ށE�ʒu�ŃI�u�W�F�N�g�𐶐�
            Instantiate(tileChoise, randomPosition, Quaternion.identity);
        }
    }

    //�I�u�W�F�N�g��z�u���Ă������\�b�h
    //���̃N���X���B���public���\�b�h���𐶐�����^�C�~���O��GameManager����Ă΂��
    public void SetupScene()
    {

        //����z�u���A
        BoardSetup();
        //�G�L������z�u�ł���ʒu�����肵�A
        InitialiseList();

        //Instantiate(����������GameObject, �ʒu, );
        Instantiate(BossTiles, new Vector2(3, rows + adjustment), Quaternion.identity);

        //�G�L�����������_���Ŕz�u���A
        LayoutObjectAtRandom(enemyTiles, enemyCount.minimum, enemyCount.maximum);

    }


}
