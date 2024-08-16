using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public static SlotManager _slot { get; private set; }

    public Slider slider;
    private int timer;

    [Header("Balance Time")]
    [SerializeField] private float To_10_Delay_minimum;
    [SerializeField] private float To_10_Delay_max;
    [SerializeField] private int To_10_Student_minimum;
    [SerializeField] private int To_10_Student_max;

    [SerializeField] private float To_30_Delay_minimum;
    [SerializeField] private float To_30_Delay_max;
    [SerializeField] private int To_30_Student_minimum;
    [SerializeField] private int To_30_Student_max;

    [SerializeField] private float To_40_Delay_minimum;
    [SerializeField] private float To_40_Delay_max;
    [SerializeField] private int To_40_Student_minimum;
    [SerializeField] private int To_40_Student_max;

    [SerializeField] private GameObject _slotUIPrefab;
    public Slot[,] studentSlotList;
    public Slot tmpSlot;
    public List<Vector2Int> availableSlots = new List<Vector2Int>();

    [Header("Prefab Images")]
    [SerializeField] private Sprite Character_Graphic_basic;
    [SerializeField] private Sprite Character_Pd_basic;
    [SerializeField] private Sprite Character_Programer_basic;
    [SerializeField] private Sprite Character_Sound_basic;

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

                int randomIndex = Random.Range(0, 4);
                if (randomIndex == 0)
                {
                    studentSlotList[x, y].GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Assets/Character/Character_graphic_basic");
                    studentSlotList[x, y].PersonType = 0;
                }
                else if (randomIndex == 1)
                {
                    studentSlotList[x, y].GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Assets/Character/Character_pd_basic");
                    studentSlotList[x, y].PersonType = 1;
                }
                else if (randomIndex == 2)
                {
                    studentSlotList[x, y].GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Assets/Character/Character_programer_basic");
                    studentSlotList[x, y].PersonType = 2;
                }
                else if (randomIndex == 3)
                {
                    studentSlotList[x, y].GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Assets/Character/Character_sound_basic");
                    studentSlotList[x, y].PersonType = 3;
                }
                else Debug.Log("index오류");
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
                Managers.Game.PlayerDied(); yield break;
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

       if(timer==10)
        {
            studentDelay_minimum = To_10_Delay_minimum;
            studentDelay_max = To_10_Delay_max;
            studentsNumber_minimum = To_10_Student_minimum;
            studentsNumber_max = To_10_Student_max;
        }
       else if(timer==30)
        {
            studentDelay_minimum = To_30_Delay_minimum;
            studentDelay_max = To_30_Delay_max;
            studentsNumber_minimum = To_30_Student_minimum;
            studentsNumber_max = To_30_Student_max;

        }
       else if(timer==40)
        {
            studentDelay_minimum = To_40_Delay_minimum;
            studentDelay_max = To_40_Delay_max;
            studentsNumber_minimum = To_40_Student_minimum;
            studentsNumber_max = To_40_Student_max;

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

    public void TimerAt(float time)
    {
        timer = (int)time;
    }
}
