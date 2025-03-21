﻿// Main program file that can import and run your different learning examples
using System;
using System.Collections.Generic;

// Import your calculator example
using SimpleCalculator;

namespace CSharpLearning {
    static class Program {

    static readonly List<ExampleProgram> examples = new List<ExampleProgram> {
        new ExampleProgram {
            Id = "1",
            Name = "Simple Calculator",
            Description = "Learn C# fundamentals through a practical example",
            IsEnabled = true,
            RunAction = () => SimpleCalculator.Program.RunCalculator()
        }, 
        new ExampleProgram {
            Id = "2",
            Name = "Simple Todo App",
            Description = "Todo",
            IsEnabled = false,
            RunAction = () => TodoApp.Program.RunTodoApp()
        }
    };
    
    static void Main(string[] args)
    {
        Console.WriteLine("=== C# Learning Journey ===");
        Console.WriteLine("Choose a program to run:");

        foreach(var example in examples) {
            if(example.IsEnabled) {
                Console.WriteLine($"{example.Id}. {example.Name} - {example.Description}");
            }
        }
        
        string? choice = Console.ReadLine();
        
        RunExample(choice);
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
     
     static void RunExample(string? choice) {
        var selectedExample = examples.FirstOrDefault(e => e.Id == choice && e.IsEnabled);

        if(selectedExample != null) {
            selectedExample.RunAction();
        } else {
            Console.WriteLine("Invalid choice!");
        }
     }

   
}
class ExampleProgram {
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public Action RunAction { get; set; } = () => Console.WriteLine("No action defined.");

}



}

