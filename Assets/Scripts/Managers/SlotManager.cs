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
    [SerializeField] private float first_slot_x; //������ ���۵Ǵ� ����
    [SerializeField] private float first_slot_y; // ������ ���۵Ǵ� ����
    [SerializeField] public float slotwidth = 0; // �ϳ��� ������ ���� ũ��
    [SerializeField] public float slotheight = 0; // �ϳ��� ������ ���� ũ��
    [SerializeField] private int slotwidthSize=6;
    [SerializeField] private int slotheightSize;// ���μ��� ���԰���


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
        // 2D �迭�� ��� ��ǥ�� ����Ʈ�� �߰�
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
                else Debug.Log("index����");
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
        while (true)
        {
            Debug.Log($"���̺� : {studentOrder}");
            for (int i = 0; i < studentCountOrder[studentOrder]; i++)
            {
                if (availableSlots.Count <= studentCountOrder[studentOrder])
                {
                    Managers.Game.PlayerDied(0); yield break;
                }
                int randomIndex = Random.Range(0, availableSlots.Count);
                //Debug.Log($"DeleteAvailabe ���� List�� Count {availableSlots.Count}");
                Vector2Int selectedSlotposition = availableSlots[randomIndex];
                Slot slot = studentSlotList[selectedSlotposition.x, selectedSlotposition.y];
                if (!slot.isActing)
                {
                    slot.SetActingtype(Random.Range(0, 3));
                    availableSlots.RemoveAt(randomIndex); // �ߺ� ������ ���� ������ ������ ����
                }
            }
            studentOrder++;
            yield return new WaitForSeconds(studentDelay);
        }
    }

    public void DeleteAvailable(int x,int y)
    {
        //Debug.Log($"DeleteAvailabe �� List�� Count {availableSlots.Count}");
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
        int studentCount = Random.Range(min, max + 1); // �л��� �� ��
        int remainSum = studentCount;
        int currentCount = count;

        // numbers ����Ʈ�� min���� max������ ���� �߰�
        //for (int i = wave_min; i <= wave_max; i++)
        //{
        //    numbers.Add(i);
        //    remainSum -= i;
        //    currentCount--;
        //}
        Debug.Log($"���;��ϴ� ���� �� : {studentCount}");
        combination = FindCombinations(wave_min, wave_max, remainSum, currentCount);

        if (combination.Count == 0)
        {
            Debug.Log("���� ����");
            return new List<int>(); // ������ ������ �� ����Ʈ ��ȯ
        }

        List<int> tempList = GetRandomCombination(combination);
        numbers.AddRange(tempList);
        return numbers;
    }

    List<List<int>> FindCombinations(int min, int max, int targetSum, int count)
    {
        // Dictionary�� �޸� ��� ����ȭ
        Dictionary<(int, int), List<List<int>>> dp = new Dictionary<(int, int), List<List<int>>>();

        // ���� �ʱ�ȭ: 0���� 0�� ����� ���
        dp[(0, 0)] = new List<List<int>> { new List<int>() };

        // ���̳��� ���α׷��� ���̺� ä���
        for (int num = min; num <= max; num++)
        {
            for (int i = 0; i < count; i++) // ������ ������Ű��
            {
                for (int j = num; j <= targetSum; j++) // �հ踦 ������Ű��
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
            Debug.Log($" {c++}��° ���̺� ���� ���� : {i}");
        }
    }
}
