using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.Tests;

[TestFixture]
public class TXTReaderTests
{
    private TxtReader _txtReader;
    private string _filePath = "TestData";
    
    [SetUp]
    public void Setup()
    {
        _txtReader = new TxtReader();
    }
    
    [Test]
    public void TXTReader_ReadFile_Should_CorrectlyReadText()
    {
        var expectedData = new List<string> { "odin" };
        
        var data = _txtReader.ReadFile(Path.Combine(_filePath,
                "TextReader_ReadFile_Should_CorrectlyReadText.txt"));
        
        expectedData.Should().BeEquivalentTo(data);
    }
    
    [Test]
    public void TXTReader_ReadFile_Should_Not_ModifyText()
    {
        var expectedData = new List<string> { "слово", "скучное", "в", "СЛОВО" };
        
        var data = _txtReader.ReadFile(Path.Combine(_filePath,
            "TXTReader_ReadFile_Should_IgnoreTextCase.txt"));
        
        expectedData.Should().BeEquivalentTo(data);
    }
}