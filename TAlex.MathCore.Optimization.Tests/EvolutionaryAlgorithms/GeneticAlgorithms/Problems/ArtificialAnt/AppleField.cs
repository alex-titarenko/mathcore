using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace TAlex.MathCore.Optimization.Tests.EvolutionaryAlgorithms.GeneticAlgorithms.Problems.ArtificialAnt
{
    public class AppleField
    {
        #region Fields

        private bool[,] _field;

        #endregion

        #region Properties

        public int FoodCount { get; private set; }
        public int FieldHeight { get; private set; }
        public int FieldWidth { get; private set; }
        public bool[,] Field { get { return _field; } }

        public bool this[int x, int y]
        {
            get { return _field[x, y]; }
            set { _field[x, y] = value; }
        }

        #endregion

        #region Methods

        public static AppleField ReadFieldFromStrings(string source)
        {
            var field = new AppleField();
            var lines = Regex.Split(source, "\r\n|\r|\n");

            int height = lines.Length;
            int width = width = lines.Max(x => x.Length);

            field.FieldHeight = height;
            field.FieldWidth = width;
            field._field = new bool[width, height];
            field.FoodCount = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    field._field[i, j] = (lines[j][i] == '*');

                    if (field._field[i, j])
                        field.FoodCount++;
                }
            }

            return field;
        }

        public AppleField Clone()
        {
            return new AppleField
            {
                FoodCount = FoodCount,
                FieldHeight = FieldHeight,
                FieldWidth = FieldWidth,
                _field = (bool[,])_field.Clone()
            };
        }

        #endregion
    }
}
