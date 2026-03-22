namespace IGME_106_Exam1_2245
{
    /// <summary>
    /// Generic class to capture unit test information where the
    /// pass flag & result string are based on the comparison of
    /// the expected and actual values of a specific type, T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UnitTest<T> : IUnitTest
    {
        public string ClassName { get; private set; }
        public string Label { get; private set; }
        public string Result { get; private set; }
        public bool Passed { get; private set; }

        public UnitTest(string className, string label, T expected, T actual)
        {
            ClassName = className;
            Label = label;
            if (expected == null)
            {
                Result = $"(E: null, A: {actual})";
                Passed = actual == null;
            }
            else if (actual == null)
            {
                Result = $"(E: {expected}, A: null)";
                Passed = expected == null;
            }
            else
            {
                Result = $"(E: {expected}, A: {actual})";
                Passed = actual.Equals(expected);
            }
        }

        public void Print()
        {
            Console.Write(ClassName.PadRight(10));
            string passFail = "";
            if (Passed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                passFail = "PASS";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                passFail = "** FAIL **";
            }
            Console.WriteLine($"\t{Label.PadRight(25)}\t{Result.PadRight(40)}\t{passFail}");
            Console.ResetColor();
        }
    }
}
