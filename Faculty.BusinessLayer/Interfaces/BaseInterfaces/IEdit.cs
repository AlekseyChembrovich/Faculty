namespace Faculty.BusinessLayer.Interfaces.BaseInterfaces
{
    public interface IEdit<T> where T : class, new()
    {
        T GetModel(int id);
        void Edit(T model);
    }
}
