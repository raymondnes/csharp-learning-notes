# Arrays

## Introduction

Arrays are fundamental data structures that store multiple values of the same type in a single variable. They provide indexed access to elements, making them essential for working with collections of data.

## What is an Array?

An array is:
- A fixed-size collection of elements of the same type
- Zero-indexed (first element is at index 0)
- Stored in contiguous memory locations

```csharp
// Array of 5 integers
int[] numbers = new int[5];  // [0, 0, 0, 0, 0]

// Access elements by index
numbers[0] = 10;  // First element
numbers[4] = 50;  // Last element
```

## Declaring Arrays

```csharp
// Declaration only
int[] numbers;

// Declaration with size
int[] scores = new int[10];

// Declaration with initialization
int[] values = new int[] { 1, 2, 3, 4, 5 };

// Simplified initialization (type inferred)
int[] data = { 10, 20, 30, 40, 50 };

// Target-typed new (C# 9+)
int[] items = new[] { 1, 2, 3 };
```

## Accessing Elements

```csharp
int[] numbers = { 10, 20, 30, 40, 50 };

// Read elements
int first = numbers[0];   // 10
int third = numbers[2];   // 30
int last = numbers[4];    // 50

// Write elements
numbers[0] = 100;
numbers[2] = 300;

// Length property
int length = numbers.Length;  // 5

// Last element using Length
int lastElement = numbers[numbers.Length - 1];  // 50
```

## Array Bounds

Arrays have fixed bounds. Accessing outside causes an exception:

```csharp
int[] arr = new int[5];  // Valid indices: 0, 1, 2, 3, 4

arr[5] = 10;   // IndexOutOfRangeException!
arr[-1] = 10;  // IndexOutOfRangeException!
```

Always check bounds:
```csharp
int index = 10;
if (index >= 0 && index < arr.Length)
{
    arr[index] = 100;
}
```

## Iterating Arrays

### Using for Loop

```csharp
int[] numbers = { 10, 20, 30, 40, 50 };

// Forward iteration
for (int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine($"Index {i}: {numbers[i]}");
}

// Backward iteration
for (int i = numbers.Length - 1; i >= 0; i--)
{
    Console.WriteLine($"Index {i}: {numbers[i]}");
}
```

### Using foreach Loop

```csharp
int[] numbers = { 10, 20, 30, 40, 50 };

foreach (int num in numbers)
{
    Console.WriteLine(num);
}

// Note: Cannot modify elements with foreach
foreach (int num in numbers)
{
    // num = 100;  // This doesn't change the array!
}
```

## Common Array Operations

### Finding Elements

```csharp
int[] numbers = { 5, 2, 8, 1, 9, 3 };

// Find maximum
int max = numbers[0];
for (int i = 1; i < numbers.Length; i++)
{
    if (numbers[i] > max)
        max = numbers[i];
}
Console.WriteLine($"Max: {max}");  // 9

// Find minimum
int min = numbers[0];
for (int i = 1; i < numbers.Length; i++)
{
    if (numbers[i] < min)
        min = numbers[i];
}
Console.WriteLine($"Min: {min}");  // 1

// Find index of element
int target = 8;
int index = -1;
for (int i = 0; i < numbers.Length; i++)
{
    if (numbers[i] == target)
    {
        index = i;
        break;
    }
}
Console.WriteLine($"Index of {target}: {index}");  // 2
```

### Calculating Sum and Average

```csharp
int[] numbers = { 10, 20, 30, 40, 50 };

int sum = 0;
for (int i = 0; i < numbers.Length; i++)
{
    sum += numbers[i];
}

double average = (double)sum / numbers.Length;

Console.WriteLine($"Sum: {sum}");          // 150
Console.WriteLine($"Average: {average}");  // 30
```

### Reversing an Array

```csharp
int[] numbers = { 1, 2, 3, 4, 5 };

// In-place reversal
for (int i = 0; i < numbers.Length / 2; i++)
{
    int temp = numbers[i];
    numbers[i] = numbers[numbers.Length - 1 - i];
    numbers[numbers.Length - 1 - i] = temp;
}
// numbers is now { 5, 4, 3, 2, 1 }

// Or use built-in method
Array.Reverse(numbers);
```

### Sorting an Array

```csharp
int[] numbers = { 5, 2, 8, 1, 9, 3 };

// Built-in sort (ascending)
Array.Sort(numbers);
// numbers is now { 1, 2, 3, 5, 8, 9 }

// Descending
Array.Sort(numbers);
Array.Reverse(numbers);
// Or use LINQ: numbers.OrderByDescending(x => x).ToArray()
```

## Multi-Dimensional Arrays

### 2D Arrays (Rectangular)

```csharp
// Declaration
int[,] matrix = new int[3, 4];  // 3 rows, 4 columns

// Initialization
int[,] grid = {
    { 1, 2, 3, 4 },
    { 5, 6, 7, 8 },
    { 9, 10, 11, 12 }
};

// Access elements
int element = grid[1, 2];  // Row 1, Column 2 = 7

// Dimensions
int rows = grid.GetLength(0);     // 3
int columns = grid.GetLength(1);  // 4

// Iteration
for (int row = 0; row < rows; row++)
{
    for (int col = 0; col < columns; col++)
    {
        Console.Write($"{grid[row, col],4}");
    }
    Console.WriteLine();
}
```

### Jagged Arrays (Arrays of Arrays)

```csharp
// Declaration
int[][] jagged = new int[3][];

// Each row can have different length
jagged[0] = new int[] { 1, 2 };
jagged[1] = new int[] { 3, 4, 5, 6 };
jagged[2] = new int[] { 7, 8, 9 };

// Access elements
int value = jagged[1][2];  // 5

// Iteration
for (int i = 0; i < jagged.Length; i++)
{
    for (int j = 0; j < jagged[i].Length; j++)
    {
        Console.Write($"{jagged[i][j]} ");
    }
    Console.WriteLine();
}
```

## Passing Arrays to Methods

Arrays are reference types - the reference is passed, not a copy:

```csharp
void DoubleElements(int[] arr)
{
    for (int i = 0; i < arr.Length; i++)
    {
        arr[i] *= 2;  // Modifies original array
    }
}

int[] numbers = { 1, 2, 3 };
DoubleElements(numbers);
// numbers is now { 2, 4, 6 }
```

### Returning Arrays

```csharp
int[] CreateArray(int size, int value)
{
    int[] result = new int[size];
    for (int i = 0; i < size; i++)
    {
        result[i] = value;
    }
    return result;
}

int[] filled = CreateArray(5, 10);  // { 10, 10, 10, 10, 10 }
```

## Array Class Methods

```csharp
int[] numbers = { 5, 2, 8, 1, 9 };

// Sort
Array.Sort(numbers);

// Reverse
Array.Reverse(numbers);

// Clear (set to default values)
Array.Clear(numbers, 0, numbers.Length);

// Copy
int[] copy = new int[5];
Array.Copy(numbers, copy, numbers.Length);

// Find
int index = Array.IndexOf(numbers, 8);  // Returns -1 if not found

// Exists
bool hasLargeNumber = Array.Exists(numbers, n => n > 5);

// Find all matching elements
int[] evens = Array.FindAll(numbers, n => n % 2 == 0);

// Resize (creates new array)
Array.Resize(ref numbers, 10);
```

## String Arrays

```csharp
string[] names = { "Alice", "Bob", "Charlie" };

// Join into single string
string joined = string.Join(", ", names);  // "Alice, Bob, Charlie"

// Split string into array
string text = "apple,banana,cherry";
string[] fruits = text.Split(',');  // { "apple", "banana", "cherry" }
```

## Practical Examples

### Example 1: Grade Calculator

```csharp
double[] grades = { 85.5, 92.0, 78.5, 90.0, 88.5 };

double sum = 0;
double highest = grades[0];
double lowest = grades[0];

for (int i = 0; i < grades.Length; i++)
{
    sum += grades[i];
    if (grades[i] > highest) highest = grades[i];
    if (grades[i] < lowest) lowest = grades[i];
}

double average = sum / grades.Length;

Console.WriteLine($"Grades: {string.Join(", ", grades)}");
Console.WriteLine($"Average: {average:F2}");
Console.WriteLine($"Highest: {highest}");
Console.WriteLine($"Lowest: {lowest}");
```

### Example 2: Bubble Sort Implementation

```csharp
void BubbleSort(int[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - i - 1; j++)
        {
            if (arr[j] > arr[j + 1])
            {
                // Swap
                int temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
}

int[] numbers = { 64, 34, 25, 12, 22, 11, 90 };
BubbleSort(numbers);
Console.WriteLine(string.Join(", ", numbers));
// Output: 11, 12, 22, 25, 34, 64, 90
```

### Example 3: Matrix Operations

```csharp
int[,] matrixA = {
    { 1, 2 },
    { 3, 4 }
};

int[,] matrixB = {
    { 5, 6 },
    { 7, 8 }
};

// Matrix Addition
int[,] result = new int[2, 2];
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 2; j++)
    {
        result[i, j] = matrixA[i, j] + matrixB[i, j];
    }
}

Console.WriteLine("Matrix A + Matrix B:");
for (int i = 0; i < 2; i++)
{
    for (int j = 0; j < 2; j++)
    {
        Console.Write($"{result[i, j],4}");
    }
    Console.WriteLine();
}
// Output:
//    6  8
//   10 12
```

## Common Mistakes

### 1. Off-by-One Errors

```csharp
int[] arr = new int[5];

// Wrong: causes IndexOutOfRangeException
for (int i = 0; i <= arr.Length; i++)  // Should be <, not <=
{
    Console.WriteLine(arr[i]);
}
```

### 2. Confusing Length with Last Index

```csharp
int[] arr = { 1, 2, 3, 4, 5 };
// arr.Length is 5
// Last valid index is 4 (Length - 1)

int last = arr[arr.Length];      // ERROR!
int lastCorrect = arr[arr.Length - 1];  // Correct: 5
```

### 3. Trying to Resize Arrays

```csharp
int[] arr = new int[5];
// arr[10] = 100;  // ERROR: Arrays are fixed size

// Must use Array.Resize or create new array
Array.Resize(ref arr, 15);  // Creates new larger array
```

## Summary

- Arrays store multiple values of the same type
- Zero-indexed (first element at index 0)
- Fixed size (cannot grow or shrink)
- Use `Length` property to get the number of elements
- Use for loops or foreach to iterate
- Arrays are reference types - passed by reference
- 2D arrays can be rectangular or jagged
- `Array` class provides useful static methods

Arrays are fundamental for storing and processing collections of data efficiently.
