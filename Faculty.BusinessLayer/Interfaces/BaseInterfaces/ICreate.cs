namespace Faculty.BusinessLayer.Interfaces.BaseInterfaces
{
    public interface ICreate<in T> where T: class, new()
    {
        void Create(T model);
    }
}
