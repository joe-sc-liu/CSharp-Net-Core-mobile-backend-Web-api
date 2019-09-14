using System.Collections.Generic;
using AppWebApi.Models;

namespace AppWebApi.Interfaces
{
    public interface IToDo
    {
        bool DoesItemExist(string id);

        IEnumerable<ToDoModel> All { get; }

        ToDoModel Find(string id);

        void Insert(ToDoModel item);

        void Update(ToDoModel item);

        void Delete(string id);
    }
}
