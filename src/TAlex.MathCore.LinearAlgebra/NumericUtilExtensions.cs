using System;


namespace TAlex.MathCore.LinearAlgebra
{
    public static class NumericUtilExtensions
    {
        /// <summary>
        /// Applies the complex threshold for each element of the complex matrix and returns the result.
        /// </summary>
        /// <param name="value">A complex matrix.</param>
        /// <param name="complexThreshold">An integer representing the complex threshold.</param>
        /// <returns>
        /// The result of applying a complex threshold for each element of the complex matrix value.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// complexThreshold must be between 0 and 307.
        /// </exception>
        public static CMatrix ComplexThreshold(CMatrix value, int complexThreshold)
        {
            CMatrix matrix = new CMatrix(value.RowCount, value.ColumnCount);

            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                    matrix[i, j] = NumericUtil.ComplexThreshold(value[i, j], complexThreshold);
            }

            return matrix;
        }

        /// <summary>
        /// Applies the zero threshold for each element of the complex matrix and returns the result.
        /// </summary>
        /// <param name="value">A complex matrix.</param>
        /// <param name="zeroThreshold">An integer representing the zero threshold.</param>
        /// <returns>
        /// The result of applying a zero threshold for each element of the complex matrix value.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// zeroThreshold must be between 0 and 307.
        /// </exception>
        public static CMatrix ZeroThreshold(CMatrix value, int zeroThreshold)
        {
            CMatrix matrix = new CMatrix(value.RowCount, value.ColumnCount);

            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                    matrix[i, j] = NumericUtil.ZeroThreshold(value[i, j], zeroThreshold);
            }

            return matrix;
        }

        /// <summary>
        /// Applies the complex and zero threshold for each element of the complex matrix and returns the result.
        /// </summary>
        /// <param name="value">A complex matrix.</param>
        /// <param name="complexThreshold">An integer representing the complex threshold.</param>
        /// <param name="zeroThreshold">An integer representing the zero threshold.</param>
        /// <returns>
        /// The result of applying a complex and zero threshold for each element of the complex matrix value.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// complexThreshold and zeroThreshold must be between 0 and 307.
        /// </exception>
        public static CMatrix ComplexZeroThreshold(CMatrix value, int complexThreshold, int zeroThreshold)
        {
            return ZeroThreshold(ComplexThreshold(value, complexThreshold), zeroThreshold);
        }

        public static bool FuzzyEquals(CMatrix value1, CMatrix value2, double relativeTolerance)
        {
            if (value1.RowCount != value2.RowCount || value1.ColumnCount != value2.ColumnCount)
                return false;

            int rows = value1.RowCount;
            int cols = value1.ColumnCount;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (!NumericUtil.FuzzyEquals(value1[i, j], value2[i, j], relativeTolerance))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
