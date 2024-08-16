using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private GameObject _slotUIPrefab;
    public Slot[,] studentSlotList;


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
        
    }

    public void Init()
    {
       
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

                cloneslot.name = $"Inventory Slot [{x}],[{y}]"; //�̸�����
                studentSlotList[x, y].slotPositionX = x;
                studentSlotList[x, y].slotPositionY = y;

            }
        }

        studentSlots.SetActive(true); // ���߿� ��ư�� ���� InventorySetActive�� ���� False
        studentSlots.GetComponent<CanvasGroup>().alpha = 0;
        studentSlots.GetComponent<CanvasGroup>().blocksRaycasts = false; // �κ��丮 �������� ����


        GameObject CloneSlot() // Ŭ�� ����
        {
            GameObject cloneSlotPrefab = Instantiate(_slotUIPrefab, createPoint, Quaternion.identity, GameObject.Find("InventorySlots").transform); // ĵ���������� ����

            return cloneSlotPrefab;
        }
    }
}
