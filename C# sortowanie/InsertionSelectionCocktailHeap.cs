using System;
using System.Collections.Generic;
using System.Diagnostics;

class Sort2
{
    static void Main(string[] args)
    {
        // generating random list of elem elements
        int elem = 200000;
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        int[] generatedList = new int[elem];
        int[] generatedList2 = new int[elem];
/*        for (int i = 0; i < generatedList.Length; i++)
        {
            generatedList[i] = rnd.Next(500000);
        }*/

/*        // sorting the list 
        generatedList = HeapSort(generatedList);

        // reversing the list
        Array.Reverse(generatedList);*/

/*        // constant value
        int value = rnd.Next(50);
        for (int i = 0; i < generatedList.Length; i++)
        {
            generatedList[i] = value;
        }*/


        // V-shaped 
        for (int i = 0; i < generatedList.Length/2; i++)
        {
            generatedList[i] = rnd.Next(500000);
        }
        generatedList = HeapSort(generatedList);
        Array.Reverse(generatedList);

        for (int i = 0; i < generatedList.Length/2; i++)
        {
            generatedList2[i] = rnd.Next(500000);
        }
        generatedList2 = HeapSort(generatedList2);

        for (int i = generatedList.Length/2; i < generatedList.Length; i++)
        {
            generatedList[i] = generatedList2[i];
        }


        // selection sort
        int[] numbers = new int[generatedList.Length];
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = generatedList[i];
        }
/*        Console.WriteLine();
        Console.WriteLine("Selection sort");
        Console.Write("Initial numbers are: ");
        PrintIntegerArray(numbers);
        Console.WriteLine();
        Console.Write("Sorted numbers are: ");*/
        Stopwatch sw = new Stopwatch();
        sw.Start();
        SelectionSort(numbers);
/*        PrintIntegerArray(SelectionSort(numbers));*/
        sw.Stop();
        var elapsedMsSel = sw.ElapsedMilliseconds;
        Console.WriteLine();
        Console.Write($"Selection sort took {(elapsedMsSel).ToString()} milliseconds");
        Console.WriteLine();
        Console.WriteLine();


        // insertion sort
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = generatedList[i];
        }
/*        Console.WriteLine("Insertion sort");
        Console.WriteLine();
        Console.WriteLine("Sorted numbers are :");*/
        sw = Stopwatch.StartNew();
        InsertionSort(numbers);
/*        PrintIntegerArray(InsertionSort(numbers));*/
        sw.Stop();
        var elapsedMsIns = sw.ElapsedMilliseconds;
        Console.WriteLine();
        Console.Write($"Insertion sort took {(elapsedMsIns).ToString()} milliseconds");
        Console.WriteLine();
        Console.WriteLine();



        // heap sort
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = generatedList[i];
        }
/*        Console.WriteLine("Heap Sort");
        Console.Write("Sorted numbers are: ");
        Console.WriteLine();*/
        sw = Stopwatch.StartNew();
        HeapSort(numbers);
/*        PrintIntegerArray(HeapSort(numbers));*/
        sw.Stop();
        var elapsedMsHeap = sw.ElapsedMilliseconds;
        Console.WriteLine();
        Console.Write($"Heap sort took {(elapsedMsHeap).ToString()} milliseconds");
        Console.WriteLine();
        Console.WriteLine();


        //cocktail sort
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = generatedList[i];
        }
/*        Console.WriteLine("Cocktail sort");
        Console.WriteLine();
        Console.WriteLine("Sorted numbers are :");*/
        sw = Stopwatch.StartNew();
        CocktailSort(numbers);
        /*PrintIntegerArray(CocktailSort(numbers));*/
        sw.Stop();
        var elapsedMsCocktail = sw.ElapsedMilliseconds;
        Console.WriteLine();
        Console.Write($"Cocktail sort took {(elapsedMsCocktail).ToString()} milliseconds");
        Console.WriteLine();


    }
    // selection sort
    static int[] SelectionSort(int[] inputArray)
    {
        for (int i = 0; i < inputArray.Length - 1; i++)
        {
            int smallest = i;
            for (int j = i + 1; j < inputArray.Length; j++)
            {
                if (inputArray[j] < inputArray[smallest])
                {
                    smallest = j;
                }
            }
            int temp = inputArray[smallest];
            inputArray[smallest] = inputArray[i];
            inputArray[i] = temp;
        }
        return inputArray;
    }

    //insertion sort
    static int[] InsertionSort(int[] inputArray)
    {
        for (int i = 0; i < inputArray.Length - 1; i++)
        {
            for (int j = i + 1; j > 0; j--)
            {
                if (inputArray[j - 1] > inputArray[j])
                {
                    int temp = inputArray[j - 1];
                    inputArray[j - 1] = inputArray[j];
                    inputArray[j] = temp;
                }
            }
        }
        return inputArray;
    }

    // heap sort
    static int[] HeapSort(int[] inputArray)
    {
        for (int i = inputArray.Length / 2 - 1; i >= 0; i--)
            heapify(inputArray, inputArray.Length, i);
        for (int i = inputArray.Length - 1; i >= 0; i--)
        {
            int temp = inputArray[0];
            inputArray[0] = inputArray[i];
            inputArray[i] = temp;
            heapify(inputArray, i, 0);
        }
        return inputArray;
    }
    static void heapify(int[] inputArray, int n, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        if (left < n && inputArray[left] > inputArray[largest])
            largest = left;
        if (right < n && inputArray[right] > inputArray[largest])
            largest = right;
        if (largest != i)
        {
            int swap = inputArray[i];
            inputArray[i] = inputArray[largest];
            inputArray[largest] = swap;
            heapify(inputArray, n, largest);
        }
    }

    // cocktail sort
    static int[] CocktailSort(int[] inputArray)
    {
        bool swapped = true;
        int start = 0;
        int end = inputArray.Length;

        while (swapped == true)
        {
            swapped = false;
            for (int i = start; i < end - 1; ++i)
            {
                if (inputArray[i] > inputArray[i + 1])
                {
                    int temp = inputArray[i];
                    inputArray[i] = inputArray[i + 1];
                    inputArray[i + 1] = temp;
                    swapped = true;
                }
            }
            if (swapped == false)
                break;
            swapped = false;
            end = end - 1;
            for (int i = end - 1; i >= start; i--)
            {
                if (inputArray[i] > inputArray[i + 1])
                {
                    int temp = inputArray[i];
                    inputArray[i] = inputArray[i + 1];
                    inputArray[i + 1] = temp;
                    swapped = true;
                }
            }
            start = start + 1;
        }
        return inputArray;
    }

    public static void PrintIntegerArray(int[] array)
    {
        foreach (int i in array)
        {
            Console.Write(i.ToString() + "  ");
        }
    }

}