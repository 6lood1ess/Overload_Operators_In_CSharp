using System;

namespace MatrixCalculator {

  // Custom exception for matrix operations
  public class MatrixException : Exception {
    public MatrixException(string message) : base(message) { }
  }
}
