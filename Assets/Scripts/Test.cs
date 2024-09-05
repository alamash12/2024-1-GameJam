using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Test : MonoBehaviour
{
    public int min; // ������ �ּҰ�
    public int max; // ������ �ִ밪
    public int count; // ���� ������ ����
    public int wave_min; // ������ �ּҰ�
    public int wave_max; // ������ �ִ밪

    void Start()
    {
        List<int> result = SplitNumberInRange(min, max, count, wave_min, wave_max);
        foreach (var num in result)
        {
            Debug.Log(num);
        }
    }

    List<int> SplitNumberInRange(int min, int max, int count, int wave_min, int wave_max)
    {
        List<int> numbers = new List<int>();
        int requiredSum = Random.Range(min, max + 1); // ������ ����

        // �ּҰ��� �ִ밪�� ����Ͽ� ������ ����
        for (int i = 0; i < count; i++)
        {
            int num;
            if (i < count - 1)
            {
                // ������ ���ڸ� �����ϰ� ������ ������ ���� �������� ���� ����
                num = Random.Range(wave_min, wave_max + 1);
            }
            else
            {
                // ������ ���ڴ� ������ ���� �Ҵ�
                num = requiredSum - numbers.Sum();
            }

            // ��ȿ�� �˻�
            if (num >= wave_min && num <= wave_max)
            {
                numbers.Add(num);
            }
            else
            {
                // ��ȿ���� ���� ��� ��õ�
                i--;
            }
        }

        // ��� ���ڰ� wave_min�� wave_max ������ ���ԵǾ� �ִ��� Ȯ��
        if (numbers.Count != count || numbers.Any(x => x < wave_min || x > wave_max))
        {
            Debug.LogError("��ȿ�� ���ڸ� ������ �� �����ϴ�.");
            return new List<int>(); // �� ����Ʈ ��ȯ
        }

        return numbers;
    }
}