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

    // Constructor with specific values
    public Matrix(int[,] elements) {

      if (elements == null) {
        throw new MatrixException("Elements array cannot be null");
      }

      if (elements.GetLength(0) != elements.GetLength(1)) {
        throw new MatrixException("Array must be square");
      }

      _randomGenerator = new Random();
      _size = elements.GetLength(0);
      _elements = new int[_size, _size];

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          _elements[rowIndex, columnIndex] = elements[rowIndex, columnIndex];
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

    // Determinant calculation for matrices of any size using recursive method
    public int CalculateDeterminant() {

      if (_size == 1) {
        return _elements[0, 0];
      }

      if (_size == 2) {
        return _elements[0, 0] * _elements[1, 1] - _elements[0, 1] * _elements[1, 0];
      }

      int determinant = 0;
      int sign = 1;

      for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {

        Matrix minorMatrix = CreateMinorMatrix(0, columnIndex);

        determinant += sign * _elements[0, columnIndex] * minorMatrix.CalculateDeterminant();
        sign = -sign;
      }

      return determinant;
    }

    // Helper method to create a minor matrix (matrix without specified row and column)
    private Matrix CreateMinorMatrix(int excludedRow, int excludedColumn) {

      int minorSize;
      int minorRow = 0;
      int minorColumn = 0;

      minorSize = _size - 1;

      int[,] minorElements = new int[minorSize, minorSize];

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {

        if (rowIndex == excludedRow) {
          continue;
        }

        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {

          if (columnIndex == excludedColumn) {
            continue;
          }

          minorElements[minorRow, minorColumn] = _elements[rowIndex, columnIndex];
          ++minorColumn;
        }
        ++minorRow;
      }

      return new Matrix(minorElements);
    }

    // Inverse matrix using adjugate matrix method
    public Matrix CalculateInverseMatrix() {

      int determinant = CalculateDeterminant();
      
      if (determinant == 0) {
        throw new MatrixException("Matrix is singular, inverse does not exist");
      }

      if (_size == 1) {
        Matrix resultMatrix = new Matrix(1);
        resultMatrix[0, 0] = 1 / _elements[0, 0];

        return resultMatrix;
      }

      // Calculate cofactor matrix
      Matrix cofactorMatrix = new Matrix(_size);

      int sign;

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {

          Matrix minorMatrix = CreateMinorMatrix(rowIndex, columnIndex);

          sign = ((rowIndex + columnIndex) % 2 == 0) ? 1 : -1;
          cofactorMatrix[rowIndex, columnIndex] = sign * minorMatrix.CalculateDeterminant();
        }
      }

      // Transpose cofactor matrix to get adjugate matrix
      Matrix adjugateMatrix = cofactorMatrix.Transpose();

      // Divide each element by determinant
      Matrix inverseMatrix = new Matrix(_size);

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          inverseMatrix[rowIndex, columnIndex] = adjugateMatrix[rowIndex, columnIndex] / determinant;
        }
      }

      return inverseMatrix;
    }

    // Transpose matrix
    public Matrix Transpose() {

      Matrix resultMatrix = new Matrix(_size);

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          resultMatrix[columnIndex, rowIndex] = _elements[rowIndex, columnIndex];
        }
      }

      return resultMatrix;
    }

    public override string ToString() {

      StringBuilder stringBuilder = new StringBuilder();

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          stringBuilder.Append(_elements[rowIndex, columnIndex].ToString().PadLeft(4));
        }

        stringBuilder.AppendLine();
      }

      return stringBuilder.ToString();
    }

    public int CompareTo(Matrix otherMatrix) {

      if (otherMatrix == null) {
        return 1;
      }

      return this.CalculateDeterminant().CompareTo(otherMatrix.CalculateDeterminant());
    }

    public override bool Equals(object comparedObject) {

      if (comparedObject == null || !(comparedObject is Matrix)) {
        return false;
      }

      return this == (Matrix)comparedObject;
    }

    public override int GetHashCode() {

      int hashCode = 17;

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          hashCode = hashCode * 23 + _elements[rowIndex, columnIndex].GetHashCode();
        }
      }

      return hashCode;
    }

    // Prototype pattern - deep copy
    public object Clone() {

      return new Matrix(this);
    }
  }
}
