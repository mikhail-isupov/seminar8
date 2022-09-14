using System;

namespace Seminar8
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if (args.Length == 0) Console.WriteLine("Введите номер задачи в качестве параметра командной строки.");
            else switch(args[0])
            {
                case "54": task54(); break;
                case "56": task56(); break;
                case "58": task58(); break;
                case "60": task60(); break;
                case "62": task62(); break;
                default: Console.WriteLine("Такой задачи нет"); break;
            }

        }

        static void task54()
        {
            int[,] matrix = ConstructRandomMatrix();

            Console.WriteLine("Исходная матрица:");
            Console.WriteLine();
            
            PrintMatrix(matrix);
            Console.WriteLine();

            bool descending = true;
            string sortType = descending ? "убыванию" : "возрастанию";
            
            Console.WriteLine($"Матрица с отсортированными строками по {sortType}");
            Console.WriteLine();

            SortRowsOfMatrix(matrix, descending);
            PrintMatrix(matrix);

        }
        static void task56()
        {
            int[,] matrix = ConstructRandomMatrix(rows: 3, columns: 3, minValue: 0, maxValue: 9);

            PrintMatrix(matrix);
            Console.WriteLine();

            bool minRowSum = true;
            int rowNumber = FindRow(matrix, minRowSum);
            string sortType = minRowSum ? "минимальной" : "максимальной";

            Console.WriteLine($"Номер строки с {sortType} суммой = {rowNumber}");

        }

        static void task58()
        {
            int[,] matrixA = ConstructRandomMatrix(rows: 2, columns: 3);
            int[,] matrixB = ConstructRandomMatrix(rows: 3, columns: 4);

            Console.WriteLine("Матрица А: ");
            PrintMatrix(matrixA);
            Console.WriteLine();

            Console.WriteLine("Матрица B: ");
            PrintMatrix(matrixB);
            Console.WriteLine();



            int[,] matrixAB = MatrixMultiplication(matrixA, matrixB);

            if (matrixAB == null) Console.WriteLine("Эти матрицы нельзя умножить! (число столбцов в матрице A должно быть равно числу строк в матрице B)");
            else 
            {
                Console.WriteLine("Матрица AB: ");
                Console.WriteLine();
                PrintMatrix(matrixAB);
            }
        }

        static void task60()
        {
            int x = 3; int y =3; int z = 3; // размер кубического массива

            int[,,] cubicArray = new int[x,y,z];

            int minValue = 10; int maxValue = 99;

            int[] randomNumbers = new int[maxValue - minValue + 1];

            for (int i = 0; i <= maxValue - minValue; i++) randomNumbers[i] = i + minValue;

            Random number = new Random(); int swap; int randomIndex;

            for (int i = 0; i <= maxValue - minValue; i++) 
            {
                randomIndex = number.Next(0, maxValue - minValue + 1);
                swap = randomNumbers[i];
                randomNumbers[i] = randomNumbers[randomIndex];
                randomNumbers[randomIndex] = swap;
            }

            int counter = 0;

            for (int i = 0; i < x; i++) for (int j = 0; j < y; j++) for (int k = 0; k < z; k++) cubicArray[i,j,k] = randomNumbers[counter++];

            for (int i = 0; i < x; i++) for (int j = 0; j < y; j++) for (int k = 0; k < z; k++) Console.WriteLine($"{cubicArray[i,j,k]} ({i}, {j}, {k})");
                

        }

        static void task62()
        {
            int rows = 10; int columns = 9;

            int[,] array = new int[rows, columns];

            int unfilled = 0; int maxTries = 2; int tries = maxTries; 
            
            int row = 0; int column = 0; int nextRow = 0; int nextColumn = 1;
            
            int counter = 10; 

            while (tries != 0)
            {
                if (row >= 0 && row < rows && column >= 0 && column < columns && array[row,column] == unfilled)
                {
                    array[row,column] = counter;
                    counter += 1;
                    row += nextRow;
                    column += nextColumn;
                    tries = maxTries;
                }
                else
                {
                    row -= nextRow;
                    column -= nextColumn;

                    int swap = nextColumn;
                    nextColumn = (-1) * nextRow;
                    nextRow = swap;

                    row += nextRow;
                    column += nextColumn;

                    tries -= 1;
                }
            }

            PrintMatrix(array);




        }

        static int[,] ConstructRandomMatrix(int rows = 10, int columns = 10, int minValue = 10, int maxValue = 99) 
        // создание массива rows x columns заполненного случайными int [minValue, maxValue]
        {
            int[,] matrix = new int[rows, columns];
            
            Random number = new Random();

            for (int i = 0; i < rows; i++) for (int j = 0; j < columns; j++) matrix[i,j] = number.Next(minValue, maxValue + 1);

            return matrix;
        }

        static void PrintMatrix(int[,] matrix)
        // Печать 2D массива
        {
            for (int i = 0; i < matrix.GetLength(0); i++) 
            {
                for (int j = 0; j < matrix.GetLength(1); j++) Console.Write($"{matrix[i,j]} ");
                Console.WriteLine();
            }
        }

        static void SortRowsOfMatrix(int[,] matrix, bool descending)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    int minimaxIndex = j;

                    for (int k = j + 1; k <  matrix.GetLength(1); k++)
                    {
                        if ( (descending && matrix[i,k] > matrix[i,minimaxIndex]) 
                        || (!descending && matrix[i,k] < matrix[i,minimaxIndex]) ) minimaxIndex = k;
                    }

                    int swap;
                    
                    swap = matrix[i, j];
                    matrix[i, j] = matrix[i, minimaxIndex];
                    matrix[i, minimaxIndex] = swap;

                }
            }
        }

        static int FindRow(int[,] matrix, bool minRowSum = true)
        {
            int[] rowSums = new int[matrix.GetLength(0)];
            int minimaxIndex = 0;

            for (int i = 0; i < matrix.GetLength(0); i++) 
            {
                rowSums[i] = 0;
                for (int j = 0; j < matrix.GetLength(1); j++) rowSums[i] += matrix[i,j];
            }
            

            for (int i = 0; i < rowSums.Length; i++)
            {
                if ( (minRowSum && rowSums[i] < rowSums[minimaxIndex]) 
                ||  (!minRowSum && rowSums[i] > rowSums[minimaxIndex]) ) minimaxIndex = i;
            }
            return minimaxIndex + 1;
        }

        static int[,] MatrixMultiplication(int[,] A, int[,] B)
        {
            // Перемножаются матрица А[l x m] и B[m x n]
            
            if (A.GetLength(1) != B.GetLength(0)) return null; // число столбцов A должно совпадать с числом строк B

            // Если совпадает то получается матрица C[l x n]

            int m = A.GetLength(1);
            int l = A.GetLength(0);
            int n = B.GetLength(1);

            int[,] C = new int[l, n];

            for (int i = 0; i < l; i++) 
            {
                for (int j = 0; j < n; j++)
                {
                    C[i,j] = 0;
                    for (int k = 0; k < m; k++) C[i,j] += A[i,k] * B[k,j];
                }
            }
        
        return C;

        }

    }
}

