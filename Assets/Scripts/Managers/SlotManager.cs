using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private GameObject _slotUIPrefab;
    public Slot[,] studentSlotList;
    public int MouseInputType; // water->0,WakeUp->1,CheerUp->2
    public Slot tmpSlot;

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
        Slot slot = studentSlotList[Random.Range(0, slotheightSize - 1), Random.Range(0, slotheightSize - 1)];
        slot.SetActingtype(Random.Range(0, 3));

        yield return new WaitForSeconds(1f);
        StartCoroutine(RandomStudentAct());
        yield return null;
    }

    public void SetMouseInputType(int type)
    {
        MouseInputType = type;
    }
}
