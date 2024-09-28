namespace AIChateroo.Engines.Internet;

public interface IRequestFromInternet
{
    Task<string> RunWebRequest(string url);
}