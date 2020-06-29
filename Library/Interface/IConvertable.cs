namespace Library.Interface
{
    public interface IConvertable
    {
        string ToJson(bool beautify, int nbTab = 0);
    }
}
