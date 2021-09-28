using System.Collections.Generic;

namespace Faculty.BusinessLayer.Interfaces.BaseInterfaces
{
    public interface IDisplay<T> where T: class, new()
    {
        IEnumerable<T> GetList();
    }
}
