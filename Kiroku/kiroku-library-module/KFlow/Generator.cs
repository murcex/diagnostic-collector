namespace KFlow
{
    using System;
    using System.Collections.Generic;

    public static class Generator
    {
        public static string Execute(int y)
        {
            Random rnd = new Random();

            var charOutput = "";

            for (int x = 1; x <= y; x++)
            {
                var rndNumber = rnd.Next(1, 37);

                charOutput += CharBank[rndNumber];
            }

            return charOutput;
        }

        private static readonly Dictionary<int, string> CharBank = new Dictionary<int, string>
        {
            {1, "a"},
            {2, "b"},
            {3, "c"},
            {4, "d"},
            {5, "e"},
            {6, "f"},
            {7, "g"},
            {8, "h"},
            {9, "i"},
            {10, "j"},
            {11, "k"},
            {12, "l"},
            {13, "m"},
            {14, "n"},
            {15, "o"},
            {16, "p"},
            {17, "q"},
            {18, "r"},
            {19, "s"},
            {20, "t"},
            {21, "u"},
            {22, "v"},
            {23, "w"},
            {24, "x"},
            {25, "y"},
            {26, "z"},
            {27, "0"},
            {28, "1"},
            {29, "2"},
            {30, "3"},
            {31, "4"},
            {32, "5"},
            {33, "6"},
            {34, "7"},
            {35, "8"},
            {36, "9"},
            {37, " "}
        };
    }
}