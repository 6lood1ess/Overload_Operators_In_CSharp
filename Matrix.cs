using System;
using System.Text;

namespace MatrixCalculator {
  
  public class Matrix : ICloneable, IComparable<Matrix> {

    private double[,] _matrix;
    private int _size;
    private static readonly Random s_randomGenerator = new Random();

    // Constructor for random generation
    public Matrix(int size) {

      if (size <= 0) {
        throw new MatrixException("Matrix size must be positive");
      }

      _size = size;
      _matrix = new double[_size, _size];

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          _matrix[rowIndex, columnIndex] = s_randomGenerator.Next(1, 10);
        }
      }
    }

    // Copy constructor
    public Matrix(Matrix otherMatrix) {

      if (otherMatrix == null) {
        throw new MatrixException("Source matrix cannot be null");
      }

      _size = otherMatrix._size;
      _matrix = new double[_size, _size];

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          _matrix[rowIndex, columnIndex] = otherMatrix._matrix[rowIndex, columnIndex];
        }
      }
    }

    // Constructor with specific values
    public Matrix(double[,] matrix) {

      if (matrix == null) {
        throw new MatrixException("Elements array cannot be null");
      }

      if (matrix.GetLength(0) != matrix.GetLength(1)) {
        throw new MatrixException("Array must be square");
      }

      _size = matrix.GetLength(0);
      _matrix = new double[_size, _size];

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          _matrix[rowIndex, columnIndex] = matrix[rowIndex, columnIndex];
        }
      }
    }

    // Indexer
    public double this[int rowIndex, int columnIndex] {

      get {

        if (rowIndex < 0 || rowIndex >= _size || columnIndex < 0 || columnIndex >= _size) {
          throw new MatrixException("Index is outside the matrix boundaries");
        }

        return _matrix[rowIndex, columnIndex];
      }

      set {

        if (rowIndex < 0 || rowIndex >= _size || columnIndex < 0 || columnIndex >= _size) {
          throw new MatrixException("Index is outside the matrix boundaries");
        }

        _matrix[rowIndex, columnIndex] = value;
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

          double sumOfProducts = 0;

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
    public static implicit operator double(Matrix matrix) {
      return matrix.CalculateDeterminant();
    }

    // Determinant calculation for matrices of any size using recursive method
    public double CalculateDeterminant() {

      if (_size == 1) {
        return _matrix[0, 0];
      }

      if (_size == 2) {
        return _matrix[0, 0] * _matrix[1, 1] - _matrix[0, 1] * _matrix[1, 0];
      }

      double determinant = 0;
      int sign = 1;

      for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {

        Matrix minorMatrix = CreateMinorMatrix(0, columnIndex);

        determinant += sign * _matrix[0, columnIndex] * minorMatrix.CalculateDeterminant();
        sign = -sign;
      }

      return determinant;
    }

    // Helper method to create a minor matrix (matrix without specified row and column)
    private Matrix CreateMinorMatrix(int excludedRow, int excludedColumn) {

      int minorSize;
      int minorRow = 0;

      minorSize = _size - 1;

      double[,] minorElements = new double[minorSize, minorSize];

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {

        if (rowIndex == excludedRow) {
          continue;
        }

        int minorColumn = 0;

        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {

          if (columnIndex == excludedColumn) {
            continue;
          }

          minorElements[minorRow, minorColumn] = _matrix[rowIndex, columnIndex];
          ++minorColumn;
        }
        
        ++minorRow;
      }

      return new Matrix(minorElements);
    }

    // Inverse matrix using adjugate matrix method
    public Matrix CalculateInverseMatrix() {

      double determinant = CalculateDeterminant();
      
      if (determinant == 0.0) {
        throw new MatrixException("Matrix is singular, inverse does not exist");
      }

      if (_size == 1) {
        Matrix resultMatrix = new Matrix(1);
        resultMatrix[0, 0] = 1.0 / _matrix[0, 0];

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
          resultMatrix[columnIndex, rowIndex] = _matrix[rowIndex, columnIndex];
        }
      }

      return resultMatrix;
    }

    public override string ToString() {

      StringBuilder stringBuilder = new StringBuilder();

      for (int rowIndex = 0; rowIndex < _size; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < _size; ++columnIndex) {
          stringBuilder.Append(_matrix[rowIndex, columnIndex].ToString("F2").PadLeft(8));
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
          hashCode = hashCode * 23 + _matrix[rowIndex, columnIndex].GetHashCode();
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

