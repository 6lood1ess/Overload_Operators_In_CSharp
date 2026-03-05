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
            
            case "9": { 
              RunTestMode();
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
            Console.WriteLine("\nPress any key to continue...");
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
                        "1. Create new matrices\n" +
                        "2. Display current matrices\n" +
                        "3. Add matrices (A + B)\n" +
                        "4. Multiply matrices (A * B)\n" +
                        "5. Compare matrices (>, <, ==, !=)\n" +
                        "6. Calculate determinants\n" +
                        "7. Calculate inverse matrix\n" +
                        "8. Clone and compare\n" +
                        "9. Run test mode\n" +
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
      
      Console.WriteLine($"\nMatrix A ({matrixSize}x{matrixSize}) created:\n" +
                        $"{_matrixA}\n" +
                        $"Matrix B ({matrixSize}x{matrixSize}) created:\n" +
                        $"{_matrixB}");
    }

    private void DisplayMatrices() {

      if (!CheckMatricesExist()) {
        return;
      }
      
      Console.WriteLine("--- CURRENT MATRICES ---\n\n" +
                        $"Matrix A ({_matrixA.Size}x{_matrixA.Size}):\n" +
                        $"{_matrixA}\n" +
                        $"Matrix B ({_matrixB.Size}x{_matrixB.Size}):\n" +
                        $"{_matrixB}");
    }

    private bool CheckMatricesExist() {

      if (_matrixA == null || _matrixB == null) {
        Console.WriteLine("Matrices not created yet. Please create matrices first (option 1).");

        return false;
      }

      return true;
    }

    private void PerformMatrixOperation(string operationSymbol, string operationName) {

      if (!CheckMatricesExist()) {
        return;
      }
      
      try {

        Console.WriteLine($"--- MATRIX {operationName.ToUpper()} ---\n\n" +
                          $"A {operationSymbol} B:");
        
        Matrix resultMatrix = null;
        
        if (operationSymbol == "+") {
          resultMatrix = _matrixA + _matrixB;

        } else if (operationSymbol == "*") {
          resultMatrix = _matrixA * _matrixB;
        }
        
        Console.WriteLine($"{resultMatrix}");
      }

      catch (MatrixException exception) {
        Console.WriteLine($"Error: {exception.Message}");
      }
    }

    private void CompareMatrices() {

      if (!CheckMatricesExist()) {
        return;
      }
      
      int comparisonResult;
      string comparisonMessage;

      Console.WriteLine("--- COMPARE MATRICES ---\n");
      
      try {

        Console.WriteLine($"A > B:  {_matrixA > _matrixB}\n" +
                          $"A < B:  {_matrixA < _matrixB}\n" +
                          $"A >= B: {_matrixA >= _matrixB}\n" +
                          $"A <= B: {_matrixA <= _matrixB}\n" +
                          $"A == B: {_matrixA == _matrixB}\n" +
                          $"A != B: {_matrixA != _matrixB}");
        
        comparisonResult = _matrixA.CompareTo(_matrixB);

        comparisonMessage = comparisonResult > 0 ? "A is greater than B" : 
          (comparisonResult < 0 ? "A is less than B" : "A is equal to B");
        Console.WriteLine($"\nCompare to result: {comparisonMessage}");
      }

      catch (MatrixException exception) {
        Console.WriteLine($"Error: {exception.Message}");
      }
    }

    private void CalculateDeterminants() {

      if (!CheckMatricesExist()) {
        return;
      }
      
      double determinantA;
      double determinantB;
      double determinantFromCast;

      Console.WriteLine("--- CALCULATE DETERMINANTS ---\n");
      
      try {

        determinantA = _matrixA.CalculateDeterminant();
        determinantB = _matrixB.CalculateDeterminant();
        
        Console.WriteLine($"Determinant of A: {determinantA}\n" +
                          $"Determinant of B: {determinantB}");
        
        // Using implicit conversion
        determinantFromCast = _matrixA;
        Console.WriteLine($"Determinant of A (via conversion): {determinantFromCast}\n");
        
        // Using true/false operators
        if (_matrixA) {
          Console.WriteLine("Matrix A is non-singular (determinant != 0)");

        } else {
          Console.WriteLine("Matrix A is singular (determinant = 0)");
        }
        
        if (_matrixB) {
          Console.WriteLine("Matrix B is non-singular (determinant != 0)");

        } else {
          Console.WriteLine("Matrix B is singular (determinant = 0)");
        }
      }

      catch (MatrixException exception) {
        Console.WriteLine($"Error: {exception.Message}");
      }
    }

    private void CalculateInverseMatrix() {

      if (!CheckMatricesExist()) {
        return;
      }
      
      Console.WriteLine("--- CALCULATE INVERSE MATRIX ---\n");
      
      Console.WriteLine("Choose matrix to invert:\n" +
                        "1. Matrix A\n" +
                        "2. Matrix B\n");
      Console.Write("Your choice: ");
      
      string userChoice = Console.ReadLine();
      Matrix selectedMatrix = userChoice == "1" ? _matrixA : (userChoice == "2" ? _matrixB : null);
      
      if (selectedMatrix == null) {
        Console.WriteLine("Invalid choice.");
        return;
      }
      
      try {

        if (selectedMatrix.CalculateDeterminant() == 0) {
          Console.WriteLine("Cannot calculate inverse: matrix is singular (determinant = 0)");
          return;
        }
        
        Console.WriteLine($"\nOriginal matrix ({(userChoice == "1" ? "A" : "B")}):\n" +
                          $"{selectedMatrix}\n");
        
        Matrix inverseMatrix = selectedMatrix.CalculateInverseMatrix();
        Console.WriteLine("Inverse matrix:\n" +
                          $"{inverseMatrix}");
        
        Console.WriteLine("Verification: Original * Inverse:");
        Matrix verificationMatrix = selectedMatrix * inverseMatrix;
        Console.WriteLine($"{verificationMatrix}" +
                          "(Should be close to identity matrix)");
      }

      catch (MatrixException exception) {
        Console.WriteLine($"Error: {exception.Message}");
      }
    }

    private void CloneAndCompare() {

      if (!CheckMatricesExist()) {
        return;
      }
      
      Console.WriteLine("--- CLONE AND COMPARE ---\n");
      
      Console.WriteLine("Cloning matrix A...\n");
      Matrix clonedMatrix = (Matrix)_matrixA.Clone();
      
      Console.WriteLine("Original matrix A:\n" +
                        $"{_matrixA}\n" +
                        "Cloned matrix:\n" +
                        $"{clonedMatrix}\n" +
                        $"A.Equals(clonedMatrix): {_matrixA.Equals(clonedMatrix)}\n" +
                        $"A == clonedMatrix: {_matrixA == clonedMatrix}\n" +
                        $"HashCode of A: {_matrixA.GetHashCode()}\n" +
                        $"HashCode of clone: {clonedMatrix.GetHashCode()}");
    }

    private void RunTestMode() {

      Console.WriteLine("========================================" +
                        "|              TEST MODE               |" +
                        "========================================\n");
      
      TestMatrixSize(2);
      TestMatrixSize(3);
      TestMatrixSize(4);
    }

    private void TestMatrixSize(int size) {

      Console.WriteLine($"\n{new string('=', 49)}" +
                        $" TESTING {size}x{size} MATRICES " +
                        $"{new string('=', 49)}\n");

      try {

        // Creating matrices
        Console.WriteLine($"Creating matrix A ({size}x{size}):");
        Matrix testMatrixA = new Matrix(size);
        Console.WriteLine($"{testMatrixA}");

        Console.WriteLine($"\nCreating matrix B ({size}x{size}):");
        Matrix testMatrixB = new Matrix(size);
        Console.WriteLine($"{testMatrixB}");

        // Basic operations
        Console.WriteLine("\nA + B:");
        Matrix sumMatrix = testMatrixA + testMatrixB;
        Console.WriteLine($"{sumMatrix}");

        Console.WriteLine("\nA * B:");
        Matrix productMatrix = testMatrixA * testMatrixB;
        Console.WriteLine($"{productMatrix}");

        // Determinants
        double determinantA = testMatrixA.CalculateDeterminant();
        double determinantB = testMatrixB.CalculateDeterminant();
        
        Console.WriteLine($"\nDeterminant of A: {determinantA}\n" +
                          $"Determinant of B: {determinantB}\n");

        // Matrix comparison
        Console.WriteLine($"A > B: {testMatrixA > testMatrixB}\n" +
                          $"A < B: {testMatrixA < testMatrixB}\n" +
                          $"A == B: {testMatrixA == testMatrixB}\n" +
                          $"A != B: {testMatrixA != testMatrixB}\n");

        // Using true/false operators
        if (testMatrixA) {
          Console.WriteLine("Matrix A is non-singular (determinant != 0)");

        } else {
          Console.WriteLine("Matrix A is singular (determinant = 0)");
        }

        // Inverse matrix (if possible)
        try {

          if (testMatrixA.CalculateDeterminant() != 0) {

            Console.WriteLine($"\nInverse matrix for A ({size}x{size}):");
            Matrix inverseMatrix = testMatrixA.CalculateInverseMatrix();
            Console.WriteLine($"{inverseMatrix}");

            Console.WriteLine("Verification: A * A^(-1):\n" +
                              $"{testMatrixA * inverseMatrix}");
          }
        }

        catch (MatrixException exception) {
          Console.WriteLine($"\nCannot calculate inverse: {exception.Message}");
        }

        // Cloning
        Console.WriteLine("Cloning matrix A. . .");
        Matrix clonedMatrix = (Matrix)testMatrixA.Clone();
        Console.WriteLine("\nClone of matrix A:\n" +
                          $"{clonedMatrix}\n" +
                          $"A.Equals(clonedMatrix): {testMatrixA.Equals(clonedMatrix)}");
      }

      catch (Exception exception) {
        Console.WriteLine($"\nError in test: {exception.Message}");
      }
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
