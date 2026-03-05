using System;

namespace MatrixCalculator {
  
  public class Matrix : ICloneable, IComparable<Matrix> {

    private int[,] _elements;
    private int _size;
    private Random _randomGenerator;

    // Constructor for random generation
    public Matrix(int size) {

      if (size <= 0) {
        throw new MatrixException("Matrix size must be positive");
      }

      _randomGenerator = new Random();
      _size = size;
      _elements = new int[_size, _size];

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          _elements[rowIndex, columnIndex] = _randomGenerator.Next(1, 10);
        }
      }
    }

    // Copy constructor
    public Matrix(Matrix otherMatrix) {

      if (otherMatrix == null) {
        throw new MatrixException("Source matrix cannot be null");
      }

      _randomGenerator = new Random();
      _size = otherMatrix._size;
      _elements = new int[_size, _size];

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          _elements[rowIndex, columnIndex] = otherMatrix._elements[rowIndex, columnIndex];
        }
      }
    }

    // Indexer
    public int this[int rowIndex, int columnIndex] {

      get {

        if (rowIndex < 0 || rowIndex >= _size || columnIndex < 0 || columnIndex >= _size) {
          throw new MatrixException("Index is outside the matrix boundaries");
        }

        return _elements[rowIndex, columnIndex];
      }

      set {

        if (rowIndex < 0 || rowIndex >= _size || columnIndex < 0 || columnIndex >= _size) {
          throw new MatrixException("Index is outside the matrix boundaries");
        }

        _elements[rowIndex, columnIndex] = value;
      }
    }

    // Property for getting size
    public int Size {

      get {
        return _size; 
      }
    }

    // Addition operator overload
    public static Matrix operator +(Matrix firstMatrix, Matrix secondMatrix) {

      if (firstMatrix._size != secondMatrix._size) {
        throw new MatrixException("Matrices must have the same size");
      }

      Matrix resultMatrix = new Matrix(firstMatrix._size);

      for (int rowIndex = 0; rowIndex < firstMatrix._size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < firstMatrix._size; ++columnIndex) {
          resultMatrix[rowIndex, columnIndex] = 
            firstMatrix[rowIndex, columnIndex] + secondMatrix[rowIndex, columnIndex];
        }
      }

      return resultMatrix;
    }

    // Multiplication operator overload
    public static Matrix operator *(Matrix firstMatrix, Matrix secondMatrix) {

      if (firstMatrix._size != secondMatrix._size) {
        throw new MatrixException("Matrices must have the same size");
      }

      Matrix resultMatrix = new Matrix(firstMatrix._size);

      for (int rowIndex = 0; rowIndex < firstMatrix._size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < firstMatrix._size; ++columnIndex) {

          int sumOfProducts = 0;

          for (int multiplicationIndex = 0; multiplicationIndex < firstMatrix._size; ++multiplicationIndex) {
            sumOfProducts += firstMatrix[rowIndex, multiplicationIndex] * 
                            secondMatrix[multiplicationIndex, columnIndex];
          }

          resultMatrix[rowIndex, columnIndex] = sumOfProducts;
        }
      }

      return resultMatrix;
    }

    // Comparison operators overload
    public static bool operator >(Matrix firstMatrix, Matrix secondMatrix) {

      if (firstMatrix._size != secondMatrix._size) {
        throw new MatrixException("Matrices must have the same size");
      }

      return firstMatrix.CalculateDeterminant() > secondMatrix.CalculateDeterminant();
    }

    public static bool operator <(Matrix firstMatrix, Matrix secondMatrix) {

      if (firstMatrix._size != secondMatrix._size) {
        throw new MatrixException("Matrices must have the same size");
      }

      return firstMatrix.CalculateDeterminant() < secondMatrix.CalculateDeterminant();
    }

    public static bool operator >=(Matrix firstMatrix, Matrix secondMatrix) {

      if (firstMatrix._size != secondMatrix._size) {
        throw new MatrixException("Matrices must have the same size");
      }

      return firstMatrix.CalculateDeterminant() >= secondMatrix.CalculateDeterminant();
    }

    public static bool operator <=(Matrix firstMatrix, Matrix secondMatrix) {

      if (firstMatrix._size != secondMatrix._size) {
        throw new MatrixException("Matrices must have the same size");
      }

      return firstMatrix.CalculateDeterminant() <= secondMatrix.CalculateDeterminant();
    }

    public static bool operator ==(Matrix firstMatrix, Matrix secondMatrix) {

      if (ReferenceEquals(firstMatrix, null) && ReferenceEquals(secondMatrix, null)) {
        return true;
      }

      if (ReferenceEquals(firstMatrix, null) || ReferenceEquals(secondMatrix, null)) {
        return false;
      }

      if (firstMatrix._size != secondMatrix._size) {
        return false;
      }

      for (int rowIndex = 0; rowIndex < firstMatrix._size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < firstMatrix._size; ++columnIndex) {

          if (firstMatrix[rowIndex, columnIndex] != secondMatrix[rowIndex, columnIndex]) {
            return false;
          }
        }
      }

      return true;
    }

    public static bool operator !=(Matrix firstMatrix, Matrix secondMatrix) {
      return !(firstMatrix == secondMatrix);
    }

    // true/false operator overload
    public static bool operator true(Matrix matrix) {
      return matrix.CalculateDeterminant() != 0;
    }

    public static bool operator false(Matrix matrix) {
      return matrix.CalculateDeterminant() == 0;
    }

    // Implicit conversion to int (returns determinant)
    public static implicit operator int(Matrix matrix) {
      return matrix.CalculateDeterminant();
    }

    // Determinant calculation (simplified for 2x2 and 3x3 matrices)
    public int CalculateDeterminant() {

      if (_size == 1) {
        return _elements[0, 0];
      }

      if (_size == 2) {
        return _elements[0, 0] * _elements[1, 1] - _elements[0, 1] * _elements[1, 0];
      }

      if (_size == 3) {
        return _elements[0, 0] * _elements[1, 1] * _elements[2, 2] +
               _elements[0, 1] * _elements[1, 2] * _elements[2, 0] +
               _elements[0, 2] * _elements[1, 0] * _elements[2, 1] -
               _elements[0, 2] * _elements[1, 1] * _elements[2, 0] -
               _elements[0, 0] * _elements[1, 2] * _elements[2, 1] -
               _elements[0, 1] * _elements[1, 0] * _elements[2, 2];
      }

      throw new MatrixException("Determinant is calculated only for 1x1, 2x2 and 3x3 matrices");
    }
  }
}
