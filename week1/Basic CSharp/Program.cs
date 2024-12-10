// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
// String input = Console.ReadLine() ?? "";
// Console.WriteLine(input.Trim());

// Primatives
/*
Whole Numbers
int 32 bit
long 64 bit
short 16 bit
byte 8 bit

Decimal Numbers
double 64bit
float 32bit 9.654F
decimal biggest bit

Boolean
bool

Single character
char 'c'

String - NOT a primative - it is an object
string myString = "Hello"
*/

// Math
/*
+ +=
- -=
/ /=
* *=
% modular

Comparison
==
!=
.Equals
>
<
>=
<=

Logical Operators
&& and
|| or
! not operator


Bitwise operators - check for myself
*/

//Fizzbuzz exercise.

System.Console.WriteLine("Enter a Number for Fizzbuzz");
string? input = Console.ReadLine();
if (int.TryParse(input, out int n))
{
    for (int i = 1; i <= n; i++)
    {
        if (i % 3 == 0 && i % 5 == 0)
        {
            System.Console.WriteLine("FizzBuzz");
        }
        else if (i % 5 == 0)
        {
            System.Console.WriteLine("Buzz");
        }
        else if (i % 3 == 0)
        {
            System.Console.WriteLine("Fizz");
        }
        else
        {
            System.Console.WriteLine(i);
        }
    }
}
else
{
    Console.WriteLine("Invalid input. Please enter a number.");
}
;
