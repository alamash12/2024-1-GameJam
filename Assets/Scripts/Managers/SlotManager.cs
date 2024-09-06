using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public static SlotManager _slot { get; private set; }

    public Slider slider;
    public float timer;
    public float maxTimer;
    public int clearScore;

    [Header("Balance Time")]
    [SerializeField] private float studentDelay;
    [SerializeField] private int studentsNumber_minimum;
    [SerializeField] private int studentsNumber_max;
    [SerializeField] private int studentWave_minimum;
    [SerializeField] private int studentWave_max;
    
    [Space]
    [SerializeField] private float To_10_Delay;
    [SerializeField] private int To_10_Student_minimum;
    [SerializeField] private int To_10_Student_max;
    [SerializeField] private int To_10_Student_Wave_minimum;
    [SerializeField] private int To_10_Student_Wave_max;
    [Space]
    [SerializeField] private float To_20_Delay;
    [SerializeField] private int To_20_Student_minimum;
    [SerializeField] private int To_20_Student_max;
    [SerializeField] private int To_20_Student_Wave_minimum;
    [SerializeField] private int To_20_Student_Wave_max;

    [Space]
    [SerializeField] private float To_35_Delay;
    [SerializeField] private int To_35_Student_minimum;
    [SerializeField] private int To_35_Student_max;
    [SerializeField] private int To_35_Student_Wave_minimum;
    [SerializeField] private int To_35_Student_Wave_max;

    [Space]
    [SerializeField] private float To_50_Delay;
    [SerializeField] private int To_50_Student_minimum;
    [SerializeField] private int To_50_Student_max;
    [SerializeField] private int To_50_Student_Wave_minimum;
    [SerializeField] private int To_50_Student_Wave_max;


    [SerializeField] private GameObject _slotUIPrefab;
    public Slot[,] studentSlotList;
    public Slot tmpSlot;
    public List<Vector2Int> availableSlots = new List<Vector2Int>();




    [Header("Slots")]
    [SerializeField] private float first_slot_x; //슬롯이 시작되는 영역
    [SerializeField] private float first_slot_y; // 슬롯이 시작되는 영역
    [SerializeField] public float slotwidth = 0; // 하나의 슬롯의 가로 크기
    [SerializeField] public float slotheight = 0; // 하나의 슬롯의 세로 크기
    [SerializeField] private int slotwidthSize=6;
    [SerializeField] private int slotheightSize;// 가로세로 슬롯개수


    [Header("Connected Objects")]
    [SerializeField] public GameObject studentSlots;

    List<int> studentCountOrder = new List<int>();

    int studentOrder;
    int waveCount = 5;

    bool is10;
    bool is20;
    bool is35;
    bool is50;
    private void Start()
    {
        Init();
        ResetValue();
        StartCoroutine(RandomStudentAct());
        studentOrder = 0;
        is10 = false; is20 = false; is35 = false; is50 = false;
    }
    private void Update()
    {
        if (timer == 10 && !is10)
        {
            studentDelay = To_10_Delay;
            studentsNumber_minimum = To_10_Student_minimum;
            studentsNumber_max = To_10_Student_max;
            studentWave_max = To_10_Student_Wave_max;
            studentWave_minimum = To_10_Student_Wave_minimum;
            waveCount = (int)Mathf.Ceil(10f / To_10_Delay);
            Debug.Log($"waveCount : {waveCount}");
            studentOrder = 0;
            ResetValue();
            is10 = true;
        }
        else if (timer == 20 && !is20)
        {
            studentDelay = To_20_Delay;
            studentsNumber_minimum = To_20_Student_minimum;
            studentsNumber_max = To_20_Student_max;
            studentWave_max = To_20_Student_Wave_max;
            studentWave_minimum = To_20_Student_Wave_minimum;
            waveCount = (int)Mathf.Ceil(10f / To_20_Delay);
            Debug.Log($"waveCount : {waveCount}");
            studentOrder = 0;
            ResetValue();
            is20 = true;
        }
        else if (timer == 35 && !is35)
        {
            studentDelay = To_35_Delay;
            studentsNumber_minimum = To_35_Student_minimum;
            studentsNumber_max = To_35_Student_max;
            studentWave_max = To_35_Student_Wave_max;
            studentWave_minimum = To_35_Student_Wave_minimum;
            waveCount = (int)Mathf.Ceil(10f / To_35_Delay);
            Debug.Log($"waveCount : {waveCount}");
            studentOrder = 0;
            ResetValue();
            is35 = true;
        }
        else if (timer == 50 && !is50)
        {
            studentDelay = To_50_Delay;
            studentsNumber_minimum = To_50_Student_minimum;
            studentsNumber_max = To_50_Student_max;
            studentWave_max = To_50_Student_Wave_max;
            studentWave_minimum = To_50_Student_Wave_minimum;
            waveCount = (int)Mathf.Ceil(10f / To_50_Delay);
            Debug.Log($"waveCount : {waveCount}");
            studentOrder = 0;
            ResetValue();
            is50 = true;
        }
    }

    public void Init()
    {
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
        while (true)
        {
            Debug.Log($"웨이브 : {studentOrder}");
            for (int i = 0; i < studentCountOrder[studentOrder]; i++)
            {
                if (availableSlots.Count <= studentCountOrder[studentOrder])
                {
                    Managers.Game.PlayerDied(0); yield break;
                }
                int randomIndex = Random.Range(0, availableSlots.Count);
                //Debug.Log($"DeleteAvailabe 전의 List의 Count {availableSlots.Count}");
                Vector2Int selectedSlotposition = availableSlots[randomIndex];
                Slot slot = studentSlotList[selectedSlotposition.x, selectedSlotposition.y];
                if (!slot.isActing)
                {
                    slot.SetActingtype(Random.Range(0, 3));
                    availableSlots.RemoveAt(randomIndex); // 중복 방지를 위해 선택한 슬롯을 제거
                }
            }
            studentOrder++;
            yield return new WaitForSeconds(studentDelay);
        }
    }

    public void DeleteAvailable(int x,int y)
    {
        //Debug.Log($"DeleteAvailabe 후 List의 Count {availableSlots.Count}");
        availableSlots.Add(new Vector2Int(x, y));
    }

    public void TimerAt(float time)
    {
        timer = (int)time;
    }

    public void SliderConnect()
    {
        StopAllCoroutines();
        slider.GetComponent<TimeSlider>().isStop = true;
    }

    List<int> SplitNumberInRange(int min, int max, int count, int wave_min, int wave_max)
    {
        List<List<int>> combination = new List<List<int>>();
        List<int> numbers = new List<int>();
        int studentCount = Random.Range(min, max + 1); // 학생의 총 합
        int remainSum = studentCount;
        int currentCount = count;

        // numbers 리스트에 min부터 max까지의 숫자 추가
        //for (int i = wave_min; i <= wave_max; i++)
        //{
        //    numbers.Add(i);
        //    remainSum -= i;
        //    currentCount--;
        //}
        Debug.Log($"나와야하는 딴짓 수 : {studentCount}");
        combination = FindCombinations(wave_min, wave_max, remainSum, currentCount);

        if (combination.Count == 0)
        {
            Debug.Log("조합 없음");
            return new List<int>(); // 조합이 없으면 빈 리스트 반환
        }

        List<int> tempList = GetRandomCombination(combination);
        numbers.AddRange(tempList);
        return numbers;
    }

    List<List<int>> FindCombinations(int min, int max, int targetSum, int count)
    {
        // Dictionary로 메모리 사용 최적화
        Dictionary<(int, int), List<List<int>>> dp = new Dictionary<(int, int), List<List<int>>>();

        // 조합 초기화: 0개로 0을 만드는 방법
        dp[(0, 0)] = new List<List<int>> { new List<int>() };

        // 다이나믹 프로그래밍 테이블 채우기
        for (int num = min; num <= max; num++)
        {
            for (int i = 0; i < count; i++) // 개수를 증가시키며
            {
                for (int j = num; j <= targetSum; j++) // 합계를 증가시키며
                {
                    if (!dp.ContainsKey((i, j - num)))
                        continue;

                    foreach (var combination in dp[(i, j - num)])
                    {
                        List<int> newCombination = new List<int>(combination);
                        newCombination.Add(num);

                        if (!dp.ContainsKey((i + 1, j)))
                            dp[(i + 1, j)] = new List<List<int>>();

                        dp[(i + 1, j)].Add(newCombination);
                    }
                }
            }
        }

        return dp.ContainsKey((count, targetSum)) ? dp[(count, targetSum)] : new List<List<int>>();
    }

    List<int> GetRandomCombination(List<List<int>> combinations)
    {
        System.Random random = new System.Random();
        int randomIndex = random.Next(combinations.Count);
        return combinations[randomIndex];
    }

    void ResetValue()
    {
        List<int> tempList = SplitNumberInRange(studentsNumber_minimum, studentsNumber_max, waveCount, studentWave_minimum, studentWave_max);
        tempList.Sort();
        studentCountOrder = tempList;
        int c = 0;
        foreach (int i in tempList)
        {
            Debug.Log($" {c++}번째 웨이브 출현 딴짓 : {i}");
        }
    }
}
