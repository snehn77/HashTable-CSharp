using System;
using System.Collections.Generic;

namespace HashTable
{
    public class Hashtable<T>
    {
        // Node of a Hash Table
        public class Node
        {
            public Node Next { get; set; }
            public string Key { get; set; }
            public T Value { get; set; }
        }
        public int size;
        private int tableSize;
        private Node[] buckets;
        // Parameterized Constructer taking in the size of Hash Table
        public Hashtable(int size)
        {
            // Create a new Array of buckets of given size
            buckets = new Node[size];
            tableSize = size;
        }
        // Method checks if key is present in hash table
        protected (Node previous,Node current) GetNode(string key)
        {
            // Gets the position of new Node by calling this method
            int position = GetBucketByKey(key);
            Node hashtable = buckets[position];
            Node previous = null;

            while (null != hashtable)
            {
                if(hashtable.Key == key)
                {
                    return (previous,hashtable);
                }
                previous = hashtable;
                hashtable = hashtable.Next;
            }
            return (null, null);
        }

        // Gets the value of given key
        public T GetValueByKey(string key)
        {
            // check the hash table if key is present
            var (_, element) = GetNode(key);

            // If key not found then throw an exception
            if(element == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return element.Value;
        }

        // Inserting new Node in Hash Table
        public void Insert(string key,T value)
        {
            var (_, element) = GetNode(key);

            // Checks if Hash Table is Full
            if (size == tableSize)
            {
                Console.WriteLine("\nHash Table Overflow");
                return;
            }
            // Checks if the entered key is in the hash table already or not
            if (element != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError: Key is already in the hash table! You cannot use same key multiple times");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            // Create a instance of new Node
            Node newNode = new Node
            {
                Key = key,
                Value = value,
                Next = null
            };
            // Get position in hash table were you want to insert
            int positon = GetBucketByKey(key);
            Node hashtable = buckets[positon];

            // If node is empty at given position
            if(null == hashtable)
            {
                buckets[positon] = newNode;
                //Increment Size
                size++;
                OperationSuccess();
                return;
            }
            // If node already has a element at that position then traverse the linked list and add at the end
            while(null != hashtable.Next)
            {
                hashtable = hashtable.Next;
            }
            hashtable.Next = newNode;
            size++;
            OperationSuccess();
        }

        // Deleting an element from the hash table at a given key
        public void Delete(string key)
        {
            // Gets position where to delete 
            int position = GetBucketByKey(key);
            var (previous, current) = GetNode(key);
            // If table is empty
            if (size == 0)
            {
                Console.WriteLine("\nHash Table is empty!");
                return;
            }
            // If key not found in hash table
            if(null == current)
            {
                Console.WriteLine("\nNo such Element with key present in the Hash Table");
                return;
            }
            // If key Found at start of linked list i.e. in the array position
            if(null == previous)
            {
                buckets[position] = null;
                buckets[position] = current.Next;
                size--;
                OperationSuccess();
                return;
            }
            // If not at start but in the linked list
            previous.Next = current.Next;
            size--;
            OperationSuccess();
        }

        // To check if given element in hash table using its key
        public bool Contains(string key)
        {
            var (_, element) = GetNode(key);
            return null != element;
        }

        // Method is Used to give index to a new Node i.e. This is the Hashing Funciton of our code
        public int GetBucketByKey(string key)
        {
            return key[0] % buckets.Length;
        }

        // returns the no of elements in the hashtable
        public int Size()
        {
            return size;
        }

        // Prints the hash table
        public void Print()
        {
            // If table has no elements
            if(size == 0)
            {
                Console.WriteLine("\nHash Table is empty!");
                return;
            }
            Console.WriteLine("\nHash Table: (Key) --> (Value)\n");
            foreach(var i in buckets)
            {
                if(i != null)
                {
                    var temp = i;
                    while(temp.Next != null)
                    {
                        Console.WriteLine("{0} --> {1}",temp.Key,temp.Value);
                        temp = temp.Next;
                    }
                    Console.WriteLine("{0} --> {1}", temp.Key, temp.Value);
                }
            }
        }
       
        public void OperationSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nOperation Successful!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Iterator of given hash table
        public void Iterator()
        {
            if (size == 0)
            {
                Console.WriteLine("\nHash Table is empty!");
                return;
            }


            IEnumerable<Node> hashTable = GetIteratedTable();
            Console.WriteLine("\nHash Table using Iterator : (Key) --> (Value)\n ");
            foreach (var i in hashTable)
            {
                Console.WriteLine("{0} -- > {1}",i.Key,i.Value);
            }
        }

        public IEnumerable<Node> GetIteratedTable()
        {
            Node temp;
            foreach (var items in buckets)
            {
                temp = items;
                // Returning the element after every iteration
                if (items != null && items.Next == null)
                {
                    yield return items;
                }
                if (items != null && items.Next != null)
                {
                    while (temp.Next != null)
                    {
                        yield return temp;
                        temp = temp.Next;
                    }
                    yield return temp;
                }
            }
        }

    }
}
