using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Node
{
    //data to store
    public int data;
    //reference to next node
    public Node next;

    public Node(int data)
    {
        this.data = data;
        next = null;
    }
}
