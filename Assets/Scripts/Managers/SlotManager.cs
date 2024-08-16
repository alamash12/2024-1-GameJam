using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public static SlotManager _slot { get; private set; }

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
    [SerializeField] private float first_slot_x; //������ ���۵Ǵ� ����
    [SerializeField] private float first_slot_y; // ������ ���۵Ǵ� ����
    [SerializeField] public float slotwidth = 0; // �ϳ��� ������ ���� ũ��
    [SerializeField] public float slotheight = 0; // �ϳ��� ������ ���� ũ��
    [SerializeField] private int slotwidthSize=6;
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
        Debug.Log(slotwidthSize);
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
                    studentSlotList[x, y].GetComponent<Image>().sprite = Character_Graphic_basic;
                }
                else if (randomIndex == 1)
                {
                    studentSlotList[x, y].GetComponent<Image>().sprite = Character_Pd_basic;
                }
                else if (randomIndex == 2)
                {
                    studentSlotList[x, y].GetComponent<Image>().sprite = Character_Sound_basic;
                }
                else if (randomIndex == 3)
                {
                    studentSlotList[x, y].GetComponent<Image>().sprite = Character_Programer_basic;
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
        studentNumber = Random.Range(studentsNumber_minimum, studentsNumber_max);
        for(int i =0;i<studentNumber;i++)
        {
            if (availableSlots.Count == 0)
            {
                Managers.Game.PlayerDied(); break;
            }
            int randomIndex = Random.Range(0, availableSlots.Count);
            Debug.Log($"DeleteAvailabe ���� List�� Count {availableSlots.Count}");
            Vector2Int selectedSlotposition = availableSlots[randomIndex];
            Slot slot = studentSlotList[selectedSlotposition.x,selectedSlotposition.y];
            if (!slot.isActing)
            {
                slot.SetActingtype(Random.Range(0, 3));
                availableSlots.RemoveAt(randomIndex); // �ߺ� ������ ���� ������ ������ ����
            }
        }

       


        studentDelay = Random.Range(studentDelay_minimum, studentDelay_max);
        yield return new WaitForSeconds(studentDelay);
        StartCoroutine(RandomStudentAct());
        yield return null;
    }
    
    public void DeleteAvailable(int x,int y)
    {
        Debug.Log($"DeleteAvailabe �� List�� Count {availableSlots.Count}");
        availableSlots.Add(new Vector2Int(x, y));
    }
}
