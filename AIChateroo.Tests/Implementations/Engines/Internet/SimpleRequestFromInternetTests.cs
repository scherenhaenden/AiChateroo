using AIChateroo.Engines.Internet;
using AIChateroo.Engines.Search.Configuration;

namespace AIChateroo.Tests.Implementations.Engines.Internet;

public class SimpleRequestFromInternetTests
{
    
    [Test]
    public async Task Test1()
    {
        var result = await new SimpleRequestFromInternet().RunWebRequest("https://duckduckgo.com/?t=h_&q=hi&ia=web");
        Assert.That(result, Is.Not.Null);
        
    }
    
    
    [Test]
    public async Task Test2()
    {
        var result = await new RequestFromTheInternetChromeDriverRenderingV1().RunWebRequest("https://duckduckgo.com/?t=h_&q=hi&ia=web");
        Assert.That(result, Is.Not.Null);
        
    }
}