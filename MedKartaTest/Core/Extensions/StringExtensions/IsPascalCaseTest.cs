using MedKarta.Core.Extensions.StringExtensions;

namespace MedKartaTest.Core.Extensions.StringExtensions;

[TestClass]
public class IsPascalCaseTest
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
    public void IsPascalCaseTest_NotNormal_error(string? testStr)
    {
        Assert.IsFalse(testStr.IsPascalCase());
    }

    [DataTestMethod]
    [DataRow("ViewModel")]
    public void IsPascalCaseTest_Normal_true(string testStr)
    {
        Assert.IsTrue(testStr.IsPascalCase());
    }
}
