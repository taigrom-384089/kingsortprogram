using System;
using System.Collections.Generic;
using System.Linq;

namespace KingSortProgram
{
    class Program
    {
        static string[] GetSortedList(string[] kings)
        {
            string[] ordinais = new string[kings.Length];
            string actualName = string.Empty;
            string oldName = string.Empty;

            for (int i = 0; i < kings.Length; i++)
            {
                //Each king consists of his actual name, a single space, and a Roman numeral.
                if (kings[i].Split(" ").Length > 2 )
                    throw new Exception("Nome do Rei não pode ter mais de 1 espaço.");

                oldName = actualName;
                actualName = kings[i].Split(" ")[0];
                string ordinal = kings[i].Split(" ")[1];

                //Each actual name of a king will be a string containing between 1 and 20 characters, inclusive.
                if (actualName.Length < 1 || actualName.Length > 20)
                    throw new Exception("Nome do Rei deve ter entre 1 a 20 caracteres.");

                //Each actual name will start by an uppercase letter('A' - 'Z').
                if (!char.IsUpper(actualName[0]))
                    throw new Exception("Nome do Rei deve ter a primeira letra maiúscula.");

                //Each other character in each actual name will be a lowercase letter ('a'-'z').
                for (int j = 1; j < actualName.Length; j++)
                {
                    if (!char.IsLower(kings[i].Split(" ")[0][j]))
                        throw new Exception("Nome do Rei deve ter letra minúscula para as outras letras.");
                }

                //kings will contain between 1 and 50 elements, inclusive.
                if (kings[i].Length < 1 || kings[i].Length > 50)
                    throw new Exception("Nome do Rei deve ter entre 1 a 20 caracteres.");

                //Each element of kings will have the form "ACTUALNAME ORDINAL", where ACTUALNAME is an actual name as defined above, and ORDINAL is a valid Roman numeral representing a number between 1 and 50, inclusive.
                var rnc = new RomanNumbersComparer();
                var number = rnc.ValueOf(ordinal);

                kings[i] = kings[i].Replace(ordinal, number.ToString());
                ordinais[i] = ordinal;
            }

            if (actualName == oldName)
            {
                Array.Sort(ordinais, new RomanNumbersComparer());

                for (int i = 0; i < ordinais.Length; i++)
                    ordinais[i] = string.Format("{0} {1}", actualName, ordinais[i]);

                kings = ordinais;
            }
            else
            {
                Array.Sort(kings);

                for (int i = 0; i < kings.Length; i++)
                {
                    var ordinal = kings[i].Split(" ")[1];
                    kings[i] = kings[i].Replace(ordinal, PrintRomanNumber.printRoman(int.Parse(ordinal)));
                }
            }
            

            return kings;
        }

        static void Print(string[] ordenados)
        {
            foreach (var item in ordenados)
            {
                Console.Write(item + " ");
            }
        }

        static void Main(string[] args)
        {
            //0) { "Louis IX", "Louis VIII"}
            //Returns: { "Louis VIII", "Louis IX" }
            //Louis the 9th should be listed after Louis the 8th.

            Print(GetSortedList(new string[] { "Louis IX", "Louis VIII" }));
            Console.WriteLine();

            //1) { "Louis IX", "Philippe II"}
            //Returns: { "Louis IX", "Philippe II" }
            //Actual names take precedence over ordinal numbers.

            Console.WriteLine();
            Print(GetSortedList(new string[] { "Louis IX", "Philippe II" }));
            Console.WriteLine();

            //2) { "Richard III", "Richard I", "Richard II"}
            //Returns: { "Richard I", "Richard II", "Richard III" }

            Console.WriteLine();
            Print(GetSortedList(new string[] { "Richard III", "Richard I", "Richard II" }));
            Console.WriteLine();

            //3) { "John X", "John I", "John L", "John V"}
            //Returns: { "John I", "John V", "John X", "John L" }

            Console.WriteLine();
            Print(GetSortedList(new string[] { "John X", "John I", "John L", "John V" }));
            Console.WriteLine();

            //4) { "Philippe VI", "Jean II", "Charles V", "Charles VI", "Charles VII", "Louis XI"}
            //Returns:
            //{ "Charles V", "Charles VI", "Charles VII", "Jean II", "Louis XI", "Philippe VI" }
            //These are the French monarchs who ruled between 1328 and 1483.

            Console.WriteLine();
            Print(GetSortedList(new string[] { "Philippe VI", "Jean II", "Charles V", "Charles VI", "Charles VII", "Louis XI" }));
            Console.WriteLine();

            //5) { "Philippe II", "Philip II"}
            //    Returns: { "Philip II", "Philippe II" }

            Console.WriteLine();
            Print(GetSortedList(new string[] { "Philippe II", "Philip II" }));
            Console.WriteLine();



            Console.ReadLine();
        }
    }
}
