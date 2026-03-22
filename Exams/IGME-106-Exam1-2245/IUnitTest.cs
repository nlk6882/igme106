namespace IGME_106_Exam1_2245
{
    /// <summary>
    /// Interface to define the basic structure of a unit test.
    /// </summary>
    public interface IUnitTest
    {
        string Result { get; }
        bool Passed { get; }
        void Print();
    }

}
