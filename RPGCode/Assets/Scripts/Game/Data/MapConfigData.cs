using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfigData
{
    //Tip: �½�
    public int Charpter;
    //Tip: �ؿ�
    public int Level;
    //Tip: ��ͼ��NPC����
    public List<MapNpcData> mapnpcdata;
    //Tip: ��ͼ��ģ������
    public List<MapModelData> mapmodeldatas;
    //Tip: ��ͼ����������
    public List<MapParticleData> mapparticledatas;
    //Tip: ��ͼ�ĶԻ�������
    public List<MapDialogData> mapdialogdatas;
    //Tip: ��ͼ�ĵ�������
    public MapLandData maplanddata;
    //Tip: ��ͼ�ı�����Ч
    public string aduioPath;
}
