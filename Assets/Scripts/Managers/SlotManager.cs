using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager _slot { get; private set; }

    [SerializeField] private GameObject _slotUIPrefab;
    public Slot[,] studentSlotList;
    public Slot tmpSlot;
    public List<Vector2Int> availableSlots = new List<Vector2Int>();

    [Header("Student Acting")]
    [SerializeField] private float studentDelay_minimum = 1.0f;
    [SerializeField] private float studentDelay_max = 2.0f;
    [SerializeField] private int studentsNumber_minimum = 1;
    [SerializeField] private int studentsNumber_max = 3;
    private float studentDelay;
    private int studentNumber;



    [Header("Slots")]
    [SerializeField] private float first_slot_x; //슬롯이 시작되는 영역
    [SerializeField] private float first_slot_y; // 슬롯이 시작되는 영역
    [SerializeField] public float slotwidth = 0; // 하나의 슬롯의 가로 크기
    [SerializeField] public float slotheight = 0; // 하나의 슬롯의 세로 크기
    [SerializeField] private int slotwidthSize=6;
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
        Debug.Log(slotwidthSize);
        InitSlots(slotwidthSize,slotheightSize);

        _slot = this;
        // 2D 배열의 모든 좌표를 리스트에 추가
        for (int y = 0; y < slotheightSize; y++)
        {
            for (int x = 0; x < slotwidthSize; x++)
            {
                availableSlots.Add(new Vector2Int(x, y));
            }
        }
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
        studentNumber = Random.Range(studentsNumber_minimum, studentsNumber_max);
        for(int i =0;i<studentNumber;i++)
        {
            if (availableSlots.Count == 0)
            {
                Managers.Game.PlayerDied(); break;
            }
            int randomIndex = Random.Range(0, availableSlots.Count);
            Debug.Log($"DeleteAvailabe 전의 List의 Count {availableSlots.Count}");
            Vector2Int selectedSlotposition = availableSlots[randomIndex];
            Slot slot = studentSlotList[selectedSlotposition.x,selectedSlotposition.y];
            if (!slot.isActing)
            {
                slot.SetActingtype(Random.Range(0, 3));
                availableSlots.RemoveAt(randomIndex); // 중복 방지를 위해 선택한 슬롯을 제거
            }
        }

       


        studentDelay = Random.Range(studentDelay_minimum, studentDelay_max);
        yield return new WaitForSeconds(studentDelay);
        StartCoroutine(RandomStudentAct());
        yield return null;
    }
    
    public void DeleteAvailable(int x,int y)
    {
        Debug.Log($"DeleteAvailabe 후 List의 Count {availableSlots.Count}");
        availableSlots.Add(new Vector2Int(x, y));
    }
}
