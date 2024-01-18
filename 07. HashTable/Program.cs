﻿using System.Collections;
using System.Collections.Immutable;

namespace _07._HashTable
{
    internal class Program
    {
        /* 해시테이블 (HashTable)
         * 
         * 키 값을 해시함수로 해싱하여 해시테이블의 특정 위치로 직접 엑세스하도록 만든 방식
         * 해시 : 임의의 길이를 가진 데이터를 고정된 길이를 가진 데이터로 매핑
         */

        // <해시테이블 구현>
        // 데이터를 담을 테이블을 이미 크게 확보해 놓은 후
        // 입력받은 키를 해싱하여 테이블 고유한 index를 계산하고 데이터를 담아 보관
        // 
        //           해싱
        //          ┌────┐
        //     27 ─→│    │─→  27
        //    385 ─→│해시│─→ 192
        //    581 ─→│함수│─→   2
        //   1031 ─→│    │─→  66
        //          └────┘
        //
        //   [0]   [1]   [2]       [27]       [66]       [191] [192] [193]
        // ┌─────┬─────┬─────┬─  ─┬────┬─  ─┬──────┬─  ─┬─────┬─────┬─────┐
        // │     │     │ 582 │....│ 27 │....│ 1031 │....│     │ 385 │     │
        // └─────┴─────┴─────┴─  ─┴────┴─  ─┴──────┴─  ─┴─────┴─────┴─────┘

        // <해시함수>
        // 키값을 해싱하여 고유한 index를 만드는 함수
        // 하나의 키값을 해싱하는 경우 반드시 항상 같은 결과여야 함 (바뀌지 않은 값으로 해야됨)
        // 효율 : 해시의 결과가 분산적일수록 효율이 좋다.
        //
        // 나눗셈법 : 데이터 % 데이블크기
        // 나눗셈법 예시
        // 데이블 크기 : 1000
        // 201 -> 201
        // 1121 -> 121
        // 338746 -> 746
        // 자리수접기 : 데이터의 각 자리수의 합
        // SHA -2     : 미국 국가안보구이 설계한 암호화 해시 함수
        // 자리수접기 예시
        // "hash" -> h(104) + a(97) + s(115) + h(104) = 420



        // <해시테이블 주의점 - 충돌>
        // 해시함수가 서로 다른 입력 값에 대해 동일한 해시테이블 주소를 반환하는 것
        // 모든 입력 값에 대해 고유한 해시 값을 만드는 것은 불가능하며 충돌은 피할 수 없음
        //
        //           해싱
        //          ┌────┐
        //    274 ─→│해시│─→  81
        //    660 ─→│함수│─→  81
        //          └────┘
        //
        //   [0]   [1]   [2]         [81]         [191] [192] [193]
        // ┌─────┬─────┬─────┬─  ─┬─────────┬─  ─┬─────┬─────┬─────┐
        // │     │     │     │....│ 274 660 │....│     │     │     │
        // └─────┴─────┴─────┴─  ─┴─────────┴─  ─┴─────┴─────┴─────┘

        // <충돌해결방안 - 체이닝>
        // 해시 충돌이 발생하면 연결리스트로 데이터들을 연결하는 방식
        // 장점 : 해시테이블에 자료가 많아지더라도 성능저하가 적음
        // 단점 : 해시테이블 외 추가적인 저장공간이 필요 (왠만하면 충돌 안나가게 해야된다.)
        //
        //           해싱
        //          ┌────┐
        //    274 ─→│해시│─→  81
        //    660 ─→│함수│─→  81
        //          └────┘
        //
        //   [0]   [1]   [2]        [81]      [191] [192] [193]
        // ┌─────┬─────┬─────┬─  ─┬─────┬─  ─┬─────┬─────┬─────┐
        // │     │     │     │....│  │  │....│     │     │     │
        // └─────┴─────┴─────┴─  ─┴──│──┴─  ─┴─────┴─────┴─────┘
        //                           ↓
        //                        ┌─────┬─┐ ┌─────┬─┐
        //                        │ 274 │──→│ 660 │ │
        //                        └─────┴─┘ └─────┴─┘


        // <충돌해결방안 - 개방주소법>
        // 해시 충돌이 발생하면 다른 빈 공간에 데이터를 삽입하는 방식
        // 해시 충돌시 선형탐색, 제곱탐색, 이중해시 등을 통해 다른 빈 공간을 선정
        // 장점 : 추가적인 저장공간이 필요하지 않음, 삽입삭제시 오버헤드가 적음
        // 단점 : 해시테이블에 자료가 많아질수록 성능저하가 많음
        //
        // 개방주소법 해시테이블의 공간 사용률이 높을 경우(통계적으로 70% 이상) 급격한 성능저하가 발생
        // 이런 경우 재해싱을 통해 공간 사용률을 낮추어 다시 효율을 확보함
        // 재해싱 : 해시테이블의 크기를 늘리고 테이블 내의 모든 데이터를 다시 해싱하여 보관
        //
        //           해싱
        //          ┌────┐
        //    274 ─→│해시│─→  81
        //    660 ─→│함수│─→  81
        //          └────┘
        //                          
        //   [0]   [1]   [2]        [81]  [82]      [191] [192] [193]
        // ┌─────┬─────┬─────┬─  ─┬─────┬─────┬─  ─┬─────┬─────┬─────┐
        // │     │     │     │....│ 274 │     │....│     │     │     │
        // └─────┴─────┴─────┴─  ─┴─────┴─────┴─  ─┴─────┴─────┴─────┘
        //                          ↑660(충돌)
        //
        //   [0]   [1]   [2]        [81]  [82]      [191] [192] [193]
        // ┌─────┬─────┬─────┬─  ─┬─────┬─────┬─  ─┬─────┬─────┬─────┐
        // │     │     │     │....│ 274 │ 660 │....│     │     │     │
        // └─────┴─────┴─────┴─  ─┴─────┴─────┴─  ─┴─────┴─────┴─────┘
        //                                ↑(다음위치에 저장)


        // <해시테이블의 시간복잡도> (삽입과 삭제는 달라 질수 있다.)
        // 접근       탐색       삽입       삭제
        //  X         O(1)       O(1)       O(1)

        // 해시를 보안용도로 사용되기도 한다.


        static void Main(string[] args)
        {   
            ArrayList arrayList = new ArrayList();
            arrayList.Add("asd");
            arrayList.Add(12313);
            arrayList.Add(0.15f);
                       
            // 일반화 되어 있지 않은 Hashtable 자료구조
            Hashtable ht = new Hashtable();   
            
            ht.Add("a", 1);
            ht.Add(123,"ads");   
            // 얘네는 느려짐

            // 해시테이블 기반의 HashSet 자료구조
            // 중복이 없는 해시기반의 저장소
            HashSet<string> set = new HashSet<string>();
            
            // 삽입 (중복 추가 무시)
            set.Add("D");
            set.Add("B");
            set.Add("B");
            set.Add("C");
            set.Add("A");
            set.Add("A");
            set.Add("A");
            set.Add("E");

            // 삭제
            set.Remove("B");

            // 탐색
            set.Contains("A");

            // 순서대로 출력시 포함된 순서대로 결과 확인
            foreach (string s in set)
            {
                Console.Write(s);  // output : DCAE
            }
            Console.WriteLine();

            // 해시테이블 기반의 Dictionary 자료구조
            // 중복을 허용하지 않는 key를 기준으로 해시기반의 value 저장소
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            // 삽입            
            dictionary.Add(2, "A");
            dictionary.Add(1, "B");
            dictionary.Add(4, "C");
            dictionary.Add(3, "D");
            dictionary.Add(5, "E");
            // dictionary.Add(4, "F");    // error : Dictionary는 중복을 허용하지 않음
            dictionary.TryAdd(4, "F");    // 이런식으로 쓰면 오류는 안난다.

            // 삭제
            dictionary.Remove(5);

            // 탐색
            dictionary.ContainsKey(3);                        // 포함 확인
            dictionary.TryGetValue(3, out string dicValue);   // 탐색 시도

            // 순서대로 출력시 정렬된 결과 확인
            foreach (string value in dictionary.Values)
            {
                Console.Write(value);     // output : ABCD
            }
            Console.WriteLine();

        }
    }
}
