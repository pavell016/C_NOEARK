# Structure

```
namespace <Nom>{
  public class main{
      static void Main(string[] args)
        {
        }
  }
}
```

# Variables

```
int age = 25; // Declare an integer variable
double price = 19.99; // Declare a double variable
string name = "Alice"; // Declare a string variable
bool isStudent = true; // Declare a boolean variable
```


# conditionalls

```
if (condition1)
{
    // Code to execute if condition1 is true
}
else if (condition2)
{
    // Code to execute if condition2 is true
}
else
{
    // Code to execute if no conditions are true
}
```

# for

```
for (initialization; condition; increment)
{
    // Code to repeat
}
```

# while

```
while (condition)
{
    // Code to repeat
}
```

# do while

```
do
{
    // Code to repeat
} while (condition);
```


# functions

```
returnType FunctionName(parameter1, parameter2, ...)
{
    // Code to execute
    return value; // Optional, depending on returnType
}
```
## functions(return types)
## Summary of Return Types

| Return Type       | Description                                                                 | Example                          |
|-------------------|-----------------------------------------------------------------------------|----------------------------------|
| `void`            | No value is returned.                                                       | `void Print(string message)`     |
| Primitive Types   | Returns a basic value (e.g., `int`, `double`, `bool`).                      | `int Add(int a, int b)`          |
| `string`          | Returns a text value.                                                       | `string GetName()`               |
| Arrays            | Returns an array of values.                                                 | `int[] GetNumbers()`             |
| Custom Objects    | Returns an instance of a class or struct.                                   | `Person CreatePerson()`          |
| Nullable Types    | Returns a value or `null`.                                                  | `int? FindNumber()`              |
| Generic Types     | Returns a value of a generic type.                                          | `T GetDefault<T>()`              |
| Tuples            | Returns multiple values of different types.                                 | `(string, int) GetPersonInfo()`  |

> [!NOTE]
>Key Rules for Return Types
>
>    The function must return a value of the specified return type (except for void).
>      If the return type is not void, the function must end with a return statement.
>    The return value must match the declared return type (e.g., returning an int for an int return type).
