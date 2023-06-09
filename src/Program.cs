﻿namespace src;

using System;
using CryptographyLibrary;
using PeopleLibrary;
using SerializationLibrary;
using static System.Console;

class Program
{
    public static List<Person> people;
    public static Serializer sc;
    public static string FirstName;
    public static string LastName;
    public static string Password;

    public static async Task Main(string[] args)
    {
        sc = new(people);
        try {
            people = await sc.Deserialize();
        } catch {
            WriteLine($"Couldn't find local database, project will generate a new one");
            people = new();
        }

        if (people.Count != 1)
            WriteLine($"{people.Count} people have been loaded");
        else
            WriteLine($"1 person has been loaded");

        while (true) {
            ReadCredentials();
            Person? p = lookUpAccount();
            if (p is not null) {
                WriteLine($"The account {p.BankAccount} has logged in successfully!");
            } else {
                WriteLine($"Account with these credentials wasn't found, creating new account...");
                p = GetNewPerson();
                people.Add(p);
                sc.peopleData = people;
                sc.Serialize();
                WriteLine("Login succesfull!");
            }
            WriteLine("\nPress enter to continue ...");
            ReadLine();
        }
    }

    private static Person GetNewPerson()
    {
        // FirstName, LastName and password have been already introduced ...
        Person p = new();
        p.FirstName = FirstName;
        p.LastName = LastName;
        p.BankPassword = Protector.Encrypt(Password, Protector.specialWord);

        decimal Salary;
        bool ok = false;
        do {
            Write("Enter your salary: ");
            try {
                Salary = Decimal.Parse(ReadLine());
                ok = true;
            } catch {
                WriteLine("Invalid salary");
            }
        } while (!ok);

        DateTime DateOfBirth;
        ok = false;
        do {
            Write("Enter your date of birth: ");
            try {
                DateOfBirth = DateTime.Parse(ReadLine());
                ok = true;
            } catch {
                WriteLine("Invalid date of birth");
            }
        } while (!ok);

        int NumberOfChildren = 0;
        ok = false;
        do {
            Write("Enter your number of Children: ");
            try {
                NumberOfChildren = Int32.Parse(ReadLine());
                ok = true;
            } catch {
                WriteLine("Invalid number");
            }
        } while (!ok);

        p.Children = GetChildren(NumberOfChildren);

        // Autoincrement
        p.BankAccount = people.Count + 1;

        return p;
    }

    /// <summary>
    /// Reads children information (FirstName).
    /// </summary>
    /// <param name="numberOfChildren"></param>
    /// <returns>List of Children read</returns>
    private static List<Person>? GetChildren(int numberOfChildren)
    {
        if (numberOfChildren == 0)
            return null;

        List<Person> children = new();
        for (int i = 0; i < numberOfChildren; ++i) {
            string FirstName = String.Empty;
            do {
                Write($"Enter children {i + 1} name: ");
                FirstName = ReadLine();
            } while (FirstName.Length == 0);
            children.Add(new() { FirstName = FirstName });
        }
        return children;
    }

    private static Person? lookUpAccount()
    {
        // good old O(n)
        foreach (var p in people) {
            if (p.FirstName == FirstName && p.LastName == LastName) {
                // Check password
                if (String.Equals(
                        Protector.Decrypt(p.BankPassword, Protector.specialWord),
                        Password)) {
                    return p;
                }
            }
        }
        return null;
    }

    private static void ReadCredentials()
    {
        do {
            Write("Please enter your first name: ");
            FirstName = ReadLine();
        } while (FirstName.Length == 0);

        do {
            Write("Please enter your last name: ");
            LastName = ReadLine();
        } while (LastName.Length == 0);

        do {
            Write("Please enter your password: ");
            Password = getPassword();
        } while (Password.Length == 0);

        WriteLine();
    }

    private static string getPassword()
    {
        System.Text.StringBuilder sb = new();
        ConsoleKeyInfo key;

        do {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace && sb.Length > 0) {
                sb.Remove(sb.Length - 1, 1);
                Console.Write("\b \b");
            }

            // Ignore any key out of range.
            if (!char.IsControl(key.KeyChar)) {
                // Append the character to the password.
                sb.Append(key.KeyChar);
                Console.Write("*");
            }
            // Exit if Enter key is pressed.
        } while (key.Key != ConsoleKey.Enter);

        return sb.ToString();
    }
}
