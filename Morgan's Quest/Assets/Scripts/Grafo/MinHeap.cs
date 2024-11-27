using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MinHeap<T>
{
    private readonly List<(int priority, T item)> elements = new List<(int, T)>();

    public int Count => elements.Count;

    public void Add(int priority, T item)
    {
        elements.Add((priority, item));
        int currentIndex = elements.Count - 1;
        while (currentIndex > 0 && elements[currentIndex].priority < elements[(currentIndex - 1) / 2].priority)
        {
            (elements[currentIndex], elements[(currentIndex - 1) / 2]) =
                (elements[(currentIndex - 1) / 2], elements[currentIndex]);
            currentIndex = (currentIndex - 1) / 2;
        }
    }

    public (int priority, T item) ExtractMin()
    {
        if (elements.Count == 0) throw new InvalidOperationException("Heap is empty");

        var min = elements[0];
        elements[0] = elements[^1];
        elements.RemoveAt(elements.Count - 1);

        int currentIndex = 0;
        while (true)
        {
            int left = 2 * currentIndex + 1;
            int right = 2 * currentIndex + 2;
            int smallest = currentIndex;

            if (left < elements.Count && elements[left].priority < elements[smallest].priority)
                smallest = left;
            if (right < elements.Count && elements[right].priority < elements[smallest].priority)
                smallest = right;
            if (smallest == currentIndex) break;

            (elements[currentIndex], elements[smallest]) = (elements[smallest], elements[currentIndex]);
            currentIndex = smallest;
        }

        return min;
    }
}
