using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QueueDynamic : MonoBehaviour
{
    
    private Node front;
    private Node rear;

    //Queue for int
    public QueueDynamic()
    {
        front = null;
        rear = null;
    }
    //Add an element to the back of the queue
    public void Enqueue(int data)
    {
        Node newNode = new Node(data);
        if (rear == null)
        {
            front = newNode;
            rear = newNode;
        }
        else
        {
            rear.next = newNode;
            rear = newNode;
        }
    }

    // Delete and return the last element in queue
    public int Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty");
        }

        int data = front.data;
        front = front.next;

        if (front == null)
        {
            rear = null;
        }

        return data;
    }

    // See the element without deleting
    public int Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty");
        }

        return front.data;
    }

    // Verify if queue is empty
    public bool IsEmpty()
    {
        return front == null;
    }

    // Print all elements in Queue
    public void PrintQueue()
    {
        Node temp = front;
        while (temp != null)
        {
            Console.Write(temp.data + " -> ");
            temp = temp.next;
        }
        Console.WriteLine("NULL");
    }
}


