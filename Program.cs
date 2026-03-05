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

    private void CreateMatrices() {

      Console.WriteLine("--- CREATE NEW MATRICES ---\n");
      
      Console.Write("Enter matrix size (e.g., 2 for 2x2, 3 for 3x3): ");

      if (!int.TryParse(Console.ReadLine(), out int matrixSize) || matrixSize <= 0) {
        Console.WriteLine("Invalid size. Using default size 2.");

        matrixSize = 2;
      }
      
      _matrixA = new Matrix(matrixSize);
      _matrixB = new Matrix(matrixSize);
      
      Console.WriteLine($"\nMatrix A ({matrixSize}x{matrixSize}) created:");
      Console.WriteLine(_matrixA);
      
      Console.WriteLine($"Matrix B ({matrixSize}x{matrixSize}) created:");
      Console.WriteLine(_matrixB);
    }

    private void DisplayMatrices() {

      if (!CheckMatricesExist()) {
        return;
      }
      
      Console.WriteLine("--- CURRENT MATRICES ---\n" +
                        $"Matrix A ({_matrixA.Size}x{_matrixA.Size}):" +
                        $"{_matrixA}\n" +
                        $"Matrix B ({_matrixB.Size}x{_matrixB.Size}):" +
                        $"{_matrixB}");
    }

    private bool CheckMatricesExist() {

      if (_matrixA == null || _matrixB == null) {
        Console.WriteLine("Matrices not created yet. Please create matrices first (option 1).");

        return false;
      }

      return true;
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
