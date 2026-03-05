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
  }
}
