using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Test : MonoBehaviour
{
    public int min; // 범위의 최소값
    public int max; // 범위의 최대값
    public int count; // 나눌 정수의 개수
    public int wave_min; // 정수의 최소값
    public int wave_max; // 정수의 최대값

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
        int requiredSum = Random.Range(min, max + 1); // 정해진 숫자

        // 최소값과 최대값을 고려하여 정수를 생성
        for (int i = 0; i < count; i++)
        {
            int num;
            if (i < count - 1)
            {
                // 마지막 숫자를 제외하고 범위를 나누기 위해 랜덤으로 수를 선택
                num = Random.Range(wave_min, wave_max + 1);
            }
            else
            {
                // 마지막 숫자는 나머지 값을 할당
                num = requiredSum - numbers.Sum();
            }

            // 유효성 검사
            if (num >= wave_min && num <= wave_max)
            {
                numbers.Add(num);
            }
            else
            {
                // 유효하지 않을 경우 재시도
                i--;
            }
        }

        // 모든 숫자가 wave_min과 wave_max 범위에 포함되어 있는지 확인
        if (numbers.Count != count || numbers.Any(x => x < wave_min || x > wave_max))
        {
            Debug.LogError("유효한 숫자를 생성할 수 없습니다.");
            return new List<int>(); // 빈 리스트 반환
        }

        return numbers;
    }
}