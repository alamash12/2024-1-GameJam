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
    [SerializeField] private float first_slot_x; //슬롯이 시작되는 영역
    [SerializeField] private float first_slot_y; // 슬롯이 시작되는 영역
    [SerializeField] public float slotwidth = 0; // 하나의 슬롯의 가로 크기
    [SerializeField] public float slotheight = 0; // 하나의 슬롯의 세로 크기
    [SerializeField] private int slotwidthSize;
    [SerializeField] private int slotheightSize;// 가로세로 슬롯개수


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
        // 임시 현재 위치를 클론위치로부터 계산하는코드
        RectTransform setXYrt = _slotUIPrefab.GetComponent<RectTransform>();
        first_slot_x = setXYrt.anchoredPosition.x;
        first_slot_y = setXYrt.anchoredPosition.y;
        Vector2 createPoint = new Vector2(first_slot_x, first_slot_y);
        // ---------------------------------------------------------

        for (int y = 0; y < slotHeightSize; y++)   // 슬롯수만큼 반복
        {
            for (int x = 0; x < slotWidthSize; x++)
            {
                var cloneslot = CloneSlot();
                RectTransform clonert = cloneslot.GetComponent<RectTransform>();

                clonert.anchoredPosition = new Vector2(first_slot_x + slotwidth * x, first_slot_y - slotheight * y); // 위치설정
                cloneslot.SetActive(true); //생성



                studentSlotList[x, y] = cloneslot.GetComponent<Slot>();
                studentSlotList[x, y].position = clonert; // 슬롯에위치저장.

                cloneslot.name = $"Student Slot [{x}],[{y}]"; //이름설정
                studentSlotList[x, y].slotPositionX = x;
                studentSlotList[x, y].slotPositionY = y;

            }
        }

        studentSlots.SetActive(true);
        studentSlots.GetComponent<CanvasGroup>().alpha = 1;

        GameObject CloneSlot() // 클론 생성
        {
            GameObject cloneSlotPrefab = Instantiate(_slotUIPrefab, createPoint, Quaternion.identity, GameObject.Find("StudentSlots").transform); // 캔버스안으로 구현

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
