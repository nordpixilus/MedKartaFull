using MedKarta.Core.Extensions.StringExtensions;

namespace MedKartaTest.Core.Extensions.StringExtensions;

[TestClass]
public class SplitPascalCaseTest
{
    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("   ")]
    [DataRow("Gg9")]
    [DataRow("Тест")]
    [DataRow("jkhkhkjh")]
    [DataRow("view Model")]
    [DataRow("viewModel")]
    [DataRow("Model")]
    public void SplitPascalCase_NotNormalStr_Exception(string? testStr)
    {
        Assert.IsNull(testStr.SplitPascalCase());

    }

    [TestMethod]
    public void SplitPascalCase_Normal_array()
    {
        string testStr = "StartView";
        string? result = testStr.SplitPascalCase();
        Assert.AreEqual("Start", result);
    }
}
