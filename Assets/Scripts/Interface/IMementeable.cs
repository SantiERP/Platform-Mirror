public interface IMementeable
{
    public void Remember();
    public void Forget();
    public void Save();

    object[] Memories { get; set; }
}
