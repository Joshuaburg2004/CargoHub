public class AccessLevel{
    public Access GetAll;
    public bool Get;
    public bool Post;
    public bool Put;
    public bool Delete;

    public AccessLevel(Access GetAll, bool Get, bool Post, bool Put, bool Delete)
    {
        this.GetAll = GetAll;
        this.Get = Get;
        this.Post = Post;
        this.Put = Put;
        this.Delete = Delete;
    }

}

public enum Access
{
    True,
    False,
    Own
}