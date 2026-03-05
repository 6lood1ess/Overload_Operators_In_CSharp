using System;

namespace MatrixCalculator {

  class MatrixApplication {

    private Matrix _matrixA;
    private Matrix _matrixB;

    public void Run() {

      try {

        Console.WriteLine("=== MATRIX CALCULATOR ===\n");

        _matrixA = new Matrix(3);
        _matrixB = new Matrix(3);

        Console.WriteLine("Matrix A:" +
                          $"{_matrixA}\n" +
                          "Matrix B:" +
                          $"{_matrixB}\n" +
                          "A + B:" +
                          $"{ _matrixA + _matrixB}\n" +
                          "A * B:" +
                          $"{_matrixA * _matrixB}\n" +
                          $"Determinant of A: {_matrixA.CalculateDeterminant()}");
      }

      catch (Exception exception) {
        Console.WriteLine($"Error: {exception.Message}");
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
