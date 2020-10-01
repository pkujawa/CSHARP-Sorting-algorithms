using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;

namespace Sortowanie2
{

    class QuickSort
    {


        public static void QuickSortIterative(ref int[] data)
        {
            int startIndex = 0;
            int endIndex = data.Length - 1;
            int top = -1;
            int[] stack = new int[data.Length];

            stack[++top] = startIndex;
            stack[++top] = endIndex;

            while (top >= 0)
            {
                endIndex = stack[top--];
                startIndex = stack[top--];

                int p = PartitionIterative(ref data, startIndex, endIndex);

                if (p - 1 > startIndex)
                {
                    stack[++top] = startIndex;
                    stack[++top] = p - 1;
                }

                if (p + 1 < endIndex)
                {
                    stack[++top] = p + 1;
                    stack[++top] = endIndex;
                }
            }
        }

        private static int PartitionIterative(ref int[] data, int left, int right)
        {
            int x = data[right];
            int i = (left - 1);

            for (int j = left; j <= right - 1; ++j)
            {
                if (data[j] <= x)
                {
                    ++i;
                    Swap(ref data[i], ref data[j]);
                }
            }

            Swap(ref data[i + 1], ref data[right]);

            return (i + 1);
        }







        private static void QuickSortRecursively(int[] arr, int left, int right, string pivot_place)
        {
            if (left < right)
            {
                int pivot = PartitionRecursively(arr, left, right, pivot_place);

                if (pivot > 1)
                {
                    QuickSortRecursively(arr, left, pivot - 1, pivot_place);
                }
                if (pivot + 1 < right)
                {
                    QuickSortRecursively(arr, pivot + 1, right, pivot_place);
                }
            }

        }

        private static int PartitionRecursively(int[] arr, int left, int right, string pivot_place)
        {

            int pivot;
            if (pivot_place == "right")
            {
                pivot = arr[right];
            }
            else if (pivot_place == "left")
            {
                pivot = arr[left];
            }
            else if (pivot_place == "random")
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                pivot = arr[rnd.Next(left, right)];
            }
            else
            {
                pivot = arr[(int)(right + left) / 2];
            }
            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    Swap(ref arr[left], ref arr[right]);
                    if (arr[left] == arr[right])
                        left++;
                }
                else
                {
                    return right;
                }
            }
        }
        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        public static void PrintIntegerArray(int[] array)
        {
            foreach (int i in array)
            {
                Console.Write(i.ToString() + "  ");
            }
            Console.WriteLine();
        }
        private static int[] CreateAShapeList(int elem) {
            // A-shaped list
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int[] aShapeListInc = new int[elem];
            int[] aShapeListDec = new int[elem]; // final A-shaped list
            for (int i = 0; i < aShapeListInc.Length / 2; i++)
            {
                aShapeListInc[i] = rnd.Next(500000);
            }
            QuickSortIterative(ref aShapeListInc);
            for (int i = 0; i < aShapeListDec.Length / 2; i++)
            {
                aShapeListDec[i] = rnd.Next(500000);
            }
            QuickSortIterative(ref aShapeListDec);
            Array.Reverse(aShapeListDec);
            for (int i = aShapeListInc.Length / 2; i < aShapeListInc.Length; i++)
            {
                aShapeListDec[i] = aShapeListInc[i];
            }
            return aShapeListDec;
        }
        private static void CountTimeIterative(int [] listToSort) {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            QuickSortIterative(ref listToSort);
            sw.Stop();
            var elapsedMsSel = sw.ElapsedMilliseconds;
            Console.WriteLine();
            Console.Write($"Iterative quick sort took {(elapsedMsSel).ToString()} milliseconds");
            Console.WriteLine();
            Console.WriteLine();
        }
        private static void CountTimeRecursively(int[] listToSort, string typeOfKey)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            QuickSortRecursively(listToSort, 0, listToSort.Length - 1, typeOfKey);
            sw.Stop();
            var elapsedMsSel = sw.ElapsedMilliseconds;
            Console.WriteLine();
            Console.Write($"Recursively quick sort took {(elapsedMsSel).ToString()} milliseconds");
            Console.WriteLine();
            Console.WriteLine();
        }
        private static void CountTimeForNElem(int elem)
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Time for {(elem).ToString()} elements");
            Console.WriteLine("--------------------------------------");
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int[] generatedList = new int[elem];
            int[] generatedList2 = new int[elem];
            for (int i = 0; i < generatedList.Length; i++)
            {
                generatedList[i] = rnd.Next(500000);
            }
            for (int i = 0; i < generatedList.Length; i++)
            {
                generatedList2[i] = generatedList[i];
            }

            CountTimeIterative(generatedList);
            CountTimeRecursively(generatedList2, "left");
        }
        private static void CountTimeForNElemForAShape(int[] aShapeList, string typeOfKey) {
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine($"Time for {(aShapeList.Length).ToString()} elements for {typeOfKey} key");
            Console.WriteLine("-----------------------------------------------------------");
            CountTimeRecursively(aShapeList, typeOfKey);
        }
        static void Main(string[] args)
        {
            CreateAShapeList(50000);
            Console.WriteLine("======================================");
            Console.WriteLine("Recursively and Iterative comparision");
            Console.WriteLine("======================================");
            for (int len = 50000; len <= 200000; len += 10000) {
                CountTimeForNElem(len);
            }
            Console.WriteLine("======================================");
            Console.WriteLine("Recursively type of key comparision");
            Console.WriteLine("======================================");
            int[] aShapeList;
            int[] secondList;
            for (int len = 50000; len <= 200000; len += 10000)
            {
                aShapeList = CreateAShapeList(len);
                secondList = new int[aShapeList.Length];
                for (int i = 0; i < aShapeList.Length; i++)
                {
                    secondList[i] = aShapeList[i];
                }
                CountTimeForNElemForAShape(secondList, "right");
                secondList = new int[aShapeList.Length];
                for (int i = 0; i < aShapeList.Length; i++)
                {
                    secondList[i] = aShapeList[i];
                }
                CountTimeForNElemForAShape(secondList, "random");
                secondList = new int[aShapeList.Length];
                for (int i = 0; i < aShapeList.Length; i++)
                {
                    secondList[i] = aShapeList[i];
                }
                CountTimeForNElemForAShape(secondList, "middle");
            }
            //int[] aShapedList = CreateAShapeList(elem);
            /*            Console.WriteLine("Original array : ");
                        PrintIntegerArray(generatedList);
                        QuickSortRecursively(generatedList, 0, generatedList.Length - 1, "random");
                        Console.WriteLine();
                        Console.WriteLine("Sorted array : ");
                        PrintIntegerArray(generatedList);
                        Console.WriteLine("------------------------------------------------------------------------");
                        Console.WriteLine("Quick Sort Iterative ");
                        Console.WriteLine("------------------------------------------------------------------------");
                        Console.WriteLine("Original array : ");
                        PrintIntegerArray(generatedList2);
                        QuickSortIterative(ref generatedList2);
                        Console.WriteLine();
                        Console.WriteLine("Sorted array : ");
                        PrintIntegerArray(generatedList2);
            */
        }
    }
}
