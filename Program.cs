using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class staticVariables
    {
        public static int reachedColumn { get; set; }
        public static bool done { get; set; }
        public static int startSquareRow { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string sNumOfpieces, sRows, sCols, sValue;
            int iNumOfpieces;
            int iRows, iCols;
            staticVariables.startSquareRow = 0;
            sNumOfpieces = Console.ReadLine();
            iNumOfpieces = Convert.ToInt32(sNumOfpieces);
            int[][,] arrayOfPieces = new int[iNumOfpieces][,];
            for (int i = 0; i < iNumOfpieces; i++)
            {
                sRows = Console.ReadLine();
                iRows = Convert.ToInt32(sRows);
                sCols = Console.ReadLine();
                iCols = Convert.ToInt32(sCols);
                arrayOfPieces[i] = new int[iRows, iCols];
                for (int counter1 = 0; counter1 < iRows; counter1++)
                {
                    for (int counter2 = 0; counter2 < iCols; counter2++)
                    {
                        sValue = Console.ReadLine();
                        arrayOfPieces[i][counter1, counter2] = Convert.ToInt32(sValue);
                    }
                }
            }
            Console.ReadKey();
            int [,]square = new int[4, 4] {
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0},
                {0,0,0,0}
            };
            Display3D(iNumOfpieces, arrayOfPieces);
            for(int i= 0; i< iNumOfpieces; i++)
            {
                staticVariables.reachedColumn = 0;
                staticVariables.done = false;
                suitablePlaceInSquare(square, arrayOfPieces[i], (i+1));
            }
            /////// Just to print out the 4*4 square for tracing ////////
            Console.WriteLine("________________________");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("{0} ", square[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("________________________");



            ///////// some examples of how to use functions below as the WORD file does assuming numOfPieces is 4/////////
            /* arrayOfPieces[3]= rotateMatrix90Grades(arrayOfPieces[3]);
             HorizontalFlip(arrayOfPieces[3]);
             Display3D(iNumOfpieces, arrayOfPieces);*/
        }

        public static int[,] rotateMatrix90Grades(int[,] oldMatrix)
        {
            int[,] newMatrix = new int[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
            int newColumn, newRow = 0;
            for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--) {
                newColumn = 0;
                for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++) {
                    newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return newMatrix;
        }
        public static void HorizontalFlip(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i <= rows - 1; i++) {
                int j = 0;
                int k = cols - 1;
                while (j < k) {
                    int temp = matrix[i, j];
                    matrix[i, j] = matrix[i, k];
                    matrix[i, k] = temp;
                    j++;
                    k--;
                }
            }
        }
        public static void VerticalFlip(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i <= cols - 1; i++) {
                int j = 0;
                int k = rows - 1;
                while (j < k) {
                    int temp = matrix[j, i];
                    matrix[j, i] = matrix[k, i];
                    matrix[k, i] = temp;
                    j++;
                    k--;
                }
            }
        }

        public static void Display3D(int numOfPieces, int[][,] matrix)
        {
            int rows, cols;
            for (int s = 0; s < numOfPieces; s++) {
                rows = matrix[s].GetLength(0);
                cols = matrix[s].GetLength(1);
                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < cols; j++) {
                        Console.Write("{0} ", matrix[s][i, j]);
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("_____________________");
            }
        }

        public static int arrayFull(int[,] a)
        {
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    if (a[i, j] == 0) {
                        return 0;
                    }
                }
            }
            return 1;
        }


        public static void suitablePlaceInSquare(int[,] square, int[,] piece, int numOfPieceinArray)
        {
            if (staticVariables.done == false) {
                if (numOfPieceinArray == 1) {
                int rows = piece.GetLength(0), cols = piece.GetLength(1);
                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < cols; j++) {
                        square[i, j] = piece[i, j];
                    }
                }
            }/*
                /// Was for the 4th piece ///
                else if(numOfPieceinArray == 4)  {
                    int rows = piece.GetLength(0), cols = piece.GetLength(1);
                    int rowIndex, colIndex, rowsNum, colsNum=0;
                    for(int i=0; i<4; i++) {        
                        for(int j=0; j<4; j++) {    
                           if( square[i,j].Equals(0)) {
                                colsNum++;
                                rowIndex = i;       
                                colIndex = j;      
                                rowsNum = 1;        
                                for(int x=0, temp= i; x< (3-i); x++) { 
                                    if (square[temp + 1, j].Equals(0)) { 
                                        rowsNum++;
                                    }              
                                }
                            }
                        }
                    }
                }*/ else {
                if (staticVariables.reachedColumn <= 3) {
                    int rows = piece.GetLength(0), cols = piece.GetLength(1);
                    for (int squareRow = staticVariables.startSquareRow, r = 0; r < rows;) {
                        for (int c = 0, squareColumn = staticVariables.reachedColumn; c < cols;) {
                            if (piece[r, c] == 0) {
                                if (square[squareRow, squareColumn] == 1 || square[squareRow, squareColumn] == 0) {
                                    c++;
                                    squareColumn++;
                                }
                                else {
                                    r += 4;
                                    squareRow += 4;
                                    break;
                                }
                            }
                            else if (piece[r, c] == 1) {
                                if (square[squareRow, squareColumn] == 0)  {
                                    c++;
                                    squareColumn++;
                                    if (c >= cols) {
                                        r++;
                                        squareRow++;
                                        if (r == rows) {
                                            staticVariables.done = true;
                                            r += 4;
                                            break;

                                        }
                                    }
                                }
                                else {
                                    if ((4 - (squareRow + 1)) < rows) {
                                        r += 4;
                                        squareRow += 4;
                                        break;
                                    }
                                    else {
                                        r += 4;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else {
                    staticVariables.reachedColumn = (-1);
                    staticVariables.startSquareRow++;
                }
                if (staticVariables.done == true) {
                        putInSquare(square, piece);
                }
                else {
                    staticVariables.reachedColumn++;
                    suitablePlaceInSquare(square, piece, numOfPieceinArray);
                }
            }
        }
        }

        public static void putInSquare(int[,] square, int[,] piece)
        {
            int rows = piece.GetLength(0), cols = piece.GetLength(1);
            for (int squareRow= staticVariables.startSquareRow, r=0; r<rows; r++, squareRow++) {
                for(int c=0, squareColumn= staticVariables.reachedColumn; c<cols; c++, squareColumn++) {
                    if(square[squareRow, squareColumn] == 1) { }
                    else {
                        square[squareRow, squareColumn] = piece[r, c];
                    }
                }
            }

        }


    }
}
