#nullable enable
using System;
using static System.Console;
using MediCal;

namespace Bme121
{
    partial class LinkedList
    {
        // Method used to indicate a target Drug object when searching.
        
        public static bool IsTarget( Drug data ) 
        { 
            return data.Name.StartsWith( "FOSAMAX", StringComparison.OrdinalIgnoreCase ); 
        }
        
        // Method used to compare two Drug objects when sorting.
        // Return is -1/0/+1 for a<b/a=b/a>b, respectively.
        
        public static int Compare( Drug a, Drug b )
        {
            return string.Compare( a.Name, b.Name, StringComparison.OrdinalIgnoreCase );
        }
        
        // Method used to add a new Drug object to the linked list in sorted order.
        
        public void InsertInOrder( Drug newData )
        {
            //TO DO
            if (newData == null) throw new ArgumentNullException( nameof( newData ) );
            
            Node newNode = new Node(newData);
            
            // the list is empty
            if (Head == null) 
            { 
                AddFirst(newNode.Data);
                return;
            }
        
            // the new element is smaller than the head
            if (Compare(newNode.Data, Head!.Data) < 0)
            {
                AddFirst(newNode.Data);
                return;
            }
            else // the new element is bigger than the head
            {
                Node? targetNode = Head;
                Node? nextNode = targetNode.Next;
                
                while (nextNode != null)
                {
                    if (Compare(newData, nextNode.Data) < 0)
                    {
                        targetNode.Next = newNode;
                        newNode.Next = nextNode;
                        Count++;
                        return;
                    }
                    targetNode = nextNode;
                    nextNode = nextNode.Next;
                }
                
                // if the new element is bigger than the oldTail
                AddLast(newData);
            }
        }
    }
    
    static class Program
    {
        // Method to test operation of the linked list.
        
        static void Main( )
        {
            Drug[ ] drugArray = Drug.ArrayFromFile( "RXQT1503-100.txt" );
            
            LinkedList drugList = new LinkedList( );
            foreach( Drug d in drugArray ) drugList.InsertInOrder( d );
            
            WriteLine( "drugList.Count = {0}", drugList.Count );
            foreach( Drug d in drugList.ToArray( ) ) WriteLine( d );
        }
    }
}
