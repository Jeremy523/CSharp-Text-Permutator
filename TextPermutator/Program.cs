using System;
using System.Collections.Generic;
using System.IO;

namespace TextPermutator
{
    class Program
    {
        public static List<string> permutations = new List<string>();

        static void Main(string[] args)
        {
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string input = dir + @"\input.txt";
            string output = dir + @"\output.txt";
            StreamReader fileReader = new StreamReader(input);
            StreamWriter fileWriter = new StreamWriter(output);
            string line;

            while ((line = fileReader.ReadLine()) != null)
            {
                if (line.Length == 0)
                    continue;
                fileWriter.WriteLine("Permutations for the word: " + line + "\n");
                fileWriter.WriteLine("Theoretical Distinct Permutations: " + ExactPermutationCount(line));
                Permutate(line.ToCharArray());
                fileWriter.WriteLine("Experimental Distinct Permutations: " + permutations.Count + "\n");
                foreach (var permutation in permutations)
                    fileWriter.WriteLine(permutation);
                fileWriter.WriteLine();
                permutations.Clear();
            }
            fileReader.Close();
        }

        // -------------------------------------------------------------------------- //
        // MATHEMATICAL IMPLEMENTATION                                                //
        // -------------------------------------------------------------------------- //

        // applies the proper formula for permutations
        // ie: (wordLength!)/((individual!)(letter!)(frequencies!))
        static int ExactPermutationCount(string word)
        {
            int length = Factorial(word.Length);
            int frequencies = ArrayFactorialProduct(LetterCount(word));
            return length / frequencies;
        }

        // love me some recursion
        static int Factorial(int n)
        {
            if (n == 0)
                return 1;
            int result = n * Factorial(n - 1);
            return result;
        }

        // takes a list of nums, and multiplies the factorials of each
        // for example, (1!)(2!)(2!)(1!) in the case of "letter"
        static int ArrayFactorialProduct(List<int> nums)
        {
            int result = 1;
            foreach (var num in nums)
                result *= Factorial(num);
            return result;
        }

        // returns array of how many unique letters are in a word and how many of each there are
        // for example, in "letter", it would return {1,2,2,1} for 1 L, 2 E's, 2 T's and 1 R
        static List<int> LetterCount(string word)
        {
            List<int> LetterCounts = new List<int>();
            for (int i = 0; i < word.Length; i++)
                if (i == 0 || word.Substring(0,i).IndexOf(word[i]) < 0)
                    LetterCounts.Add(HowMany(word[i], word));
            return LetterCounts;
        }

        // helper method to return how many of a letter are in a given word
        static int HowMany(char letter, string word)
        {
            int count = 0;
            for (int i = 0; i < word.Length; i++)
                if (letter == word[i])
                    count++;
            return count;
        }

        // -------------------------------------------------------------------------- //
        // EXPERIMENTAL IMPLEMENTATION                                                //
        // -------------------------------------------------------------------------- //

        // NOTE: "^" is an exclusive-OR operator. thus, x ^= y is the same as to x = x ^ y
        // not gonna lie, a bit iffy about this one but it works
        static void Swap(ref char a, ref char b)
        {
            if (a == b)
                return;
            a ^= b;
            b ^= a;
            a ^= b;
        }

        // initializes the permutation recursive method
        static void Permutate(char[] chars)
        {
            int len = chars.Length - 1;
            Permutate(chars, 0, len);
        }

        // what'd you say? more recursion???????
        static void Permutate(char[] chars, int start, int end)
        {
            if (start == end)
            {
                string perm = new string(chars);
                if(!permutations.Contains(perm)) // checks for duplicates
                    permutations.Add(perm);
            }
            else
                for (int i = start; i <= end; i++)
                {
                    Swap(ref chars[start], ref chars[i]);
                    Permutate(chars, start + 1, end);
                    Swap(ref chars[start], ref chars[i]);
                }
        }
    }
}
