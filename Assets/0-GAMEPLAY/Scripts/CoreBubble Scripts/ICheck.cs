using UnityEngine;
using System.Collections;
using System;

public class ICheck :I_Ainterface,I_Binterface{
 
	public static void Main(string[] args)
    {
        ICheck obj = new ICheck();
        I_Ainterface obj1 = new ICheck();
        obj1.Method();
        Debug.Log("yes making");
      
    }
    void I_Ainterface.Method()
    {
        Console.WriteLine("A");
    }
    void I_Binterface.Method()
    {
        Console.WriteLine("B");
    }
    
}


interface I_Ainterface
{

    void Method();
}

interface I_Binterface
{

    void Method();
}
