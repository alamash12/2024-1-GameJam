using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private GameObject _slotUIPrefab;
    public Slot[,] studentSlotList;
    public Slot tmpSlot;

    [Header("Student Acting")]
    [SerializeField] private float studentDelay_minimum = 1.0f;
    [SerializeField] private float studentDelay_max = 2.0f;
    [SerializeField] private int studentsNumber_minimum = 1;
    [SerializeField] private int studentsNumber_max = 3;
    private float studentDelay;
    private int studentNumber;



    [Header("Slots")]
    [SerializeField] private float first_slot_x; //������ ���۵Ǵ� ����
    [SerializeField] private float first_slot_y; // ������ ���۵Ǵ� ����
    [SerializeField] public float slotwidth = 0; // �ϳ��� ������ ���� ũ��
    [SerializeField] public float slotheight = 0; // �ϳ��� ������ ���� ũ��
    [SerializeField] private int slotwidthSize;
    [SerializeField] private int slotheightSize;// ���μ��� ���԰���


    [Header("Connected Objects")]
    [SerializeField] public GameObject studentSlots;
    private void Start()
    {
        Init();
        StartCoroutine(RandomStudentAct());
    }

    public void Init()
    {
        InitSlots(slotwidthSize,slotheightSize);
    }

    private void InitSlots(int slotWidthSize, int slotHeightSize)
    {
        studentSlotList = new Slot[slotWidthSize, slotHeightSize];
        // �ӽ� ���� ��ġ�� Ŭ����ġ�κ��� ����ϴ��ڵ�
        RectTransform setXYrt = _slotUIPrefab.GetComponent<RectTransform>();
        first_slot_x = setXYrt.anchoredPosition.x;
        first_slot_y = setXYrt.anchoredPosition.y;
        Vector2 createPoint = new Vector2(first_slot_x, first_slot_y);
        // ---------------------------------------------------------

        for (int y = 0; y < slotHeightSize; y++)   // ���Լ���ŭ �ݺ�
        {
            for (int x = 0; x < slotWidthSize; x++)
            {
                var cloneslot = CloneSlot();
                RectTransform clonert = cloneslot.GetComponent<RectTransform>();

                clonert.anchoredPosition = new Vector2(first_slot_x + slotwidth * x, first_slot_y - slotheight * y); // ��ġ����
                cloneslot.SetActive(true); //����



                studentSlotList[x, y] = cloneslot.GetComponent<Slot>();
                studentSlotList[x, y].position = clonert; // ���Կ���ġ����.

                cloneslot.name = $"Student Slot [{x}],[{y}]"; //�̸�����
                studentSlotList[x, y].slotPositionX = x;
                studentSlotList[x, y].slotPositionY = y;

            }
        }

        studentSlots.SetActive(true);
        studentSlots.GetComponent<CanvasGroup>().alpha = 1;

        GameObject CloneSlot() // Ŭ�� ����
        {
            GameObject cloneSlotPrefab = Instantiate(_slotUIPrefab, createPoint, Quaternion.identity, GameObject.Find("StudentSlots").transform); // ĵ���������� ����

            return cloneSlotPrefab;
        }
    }

    IEnumerator RandomStudentAct()
    {
        studentNumber = Random.Range(studentsNumber_minimum, studentsNumber_max);
        for(int i =0;i<studentNumber;i++)
        {
            Slot slot = studentSlotList[Random.Range(0, slotheightSize - 1), Random.Range(0, slotheightSize - 1)];
            slot.SetActingtype(Random.Range(0, 3));
        }


        studentDelay = Random.Range(studentDelay_minimum, studentDelay_max);
        yield return new WaitForSeconds(studentDelay);
        StartCoroutine(RandomStudentAct());
        yield return null;
    }

}
