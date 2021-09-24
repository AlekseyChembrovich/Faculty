namespace Faculty.BusinessLayer.Interfaces.BaseInterfaces
{
    public interface IDelete<T> where T : class, new()
    {
        void Delete(int id);
    }
}
