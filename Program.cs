using System;

namespace MatrixCalculator {

  class MatrixApplication {

    private Matrix _matrixA;
    private Matrix _matrixB;

    public void Run() {

      try {

        bool exitProgram = false;
        
        while (!exitProgram) {

          DisplayMainMenu();
          string userChoice = Console.ReadLine();
          
          Console.WriteLine();
          
          switch (userChoice) {

            case "1": { 
              CreateMatrices();
              break;
            }
              
            case "2": {
              DisplayMatrices();
              break;
            }
              
            case "3": {
              PerformMatrixOperation("+", "Addition");
              break;
            }
              
            case "4": {
              PerformMatrixOperation("*", "Multiplication");
              break;
            }
              
            case "5": {
              CompareMatrices();
              break;
            }
              
            case "6": {
              CalculateDeterminants();
              break;
            }
              
            case "7": {
              CalculateInverseMatrix();
              break;
            }
              
            case "8": {
              CloneAndCompare();
              break;
            }
              
            case "0": {
              exitProgram = true;
              Console.WriteLine("Exiting program. Goodbye!");
              break;
            }
              
            default: {
              Console.WriteLine("Invalid choice. Please select again.");
              break;
            }
          }
          
          if (!exitProgram) {
            Console.WriteLine("\nPress any key to close program...");
            Console.ReadKey();
            Console.Clear();
          }
        }
      }

      catch (Exception exception) {
        Console.WriteLine($"Unexpected error: {exception.Message}");
        Console.ReadKey();
      }
    }

    private void DisplayMainMenu() {

      Console.WriteLine("========================================" +
                        "|           MATRIX CALCULATOR          |" +
                        "========================================\n" +
                        "1. Create new matrices" +
                        "2. Display current matrices" +
                        "3. Add matrices (A + B)" +
                        "4. Multiply matrices (A * B)" +
                        "5. Compare matrices (>, <, ==, !=)" +
                        "6. Calculate determinants" +
                        "7. Calculate inverse matrix" +
                        "8. Clone and compare" +
                        "0. Exit\n");
      Console.Write("Enter your choice: ");
    }
  }


  class Program {

    static void Main(string[] args) {

      MatrixApplication app = new MatrixApplication();

      app.Run();
      Console.ReadKey();
    }
  }
}
