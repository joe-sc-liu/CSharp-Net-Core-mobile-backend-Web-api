using AppWebApi.Interfaces;
using AppWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace AppWebApi.Services
{
    public class ToDoService : IToDo
    {

        private List<ToDoModel> _toDoList;

        public ToDoService()
        {
            InitializeData();
        }

        /// <summary>
        /// 取得所有ToDo資訊
        /// </summary>
        public IEnumerable<ToDoModel> All
        {
            get { return _toDoList; }
        }

        /// <summary>
        /// 比對傳入的id，在資料庫，有沒有資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DoesItemExist(string id)
        {
            return _toDoList.Any(item => item.ID == id);
        }

        /// <summary>
        /// 取得傳入todo的id，對應的詳細資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ToDoModel Find(string id)
        {
            return _toDoList.FirstOrDefault(item => item.ID == id);
        }

        /// <summary>
        /// 新增一筆ToDo資訊
        /// </summary>
        /// <param name="item"></param>
        public void Insert(ToDoModel item)
        {
            _toDoList.Add(item);
        }

        /// <summary>
        /// 更新一個ToDo資訊
        /// </summary>
        /// <param name="item"></param>
        public void Update(ToDoModel item)
        {
            var todoItem = this.Find(item.ID);
            var index = _toDoList.IndexOf(todoItem);
            _toDoList.RemoveAt(index);
            _toDoList.Insert(index, item);
        }

        /// <summary>
        /// 刪除一筆ToDo資訊
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            _toDoList.Remove(this.Find(id));
        }



        private void InitializeData()
        {
            //在記憶體，給Medel值
            _toDoList = new List<ToDoModel>();

            var todoItem1 = new ToDoModel
            {
                ID = "6bb8a868-dba1-4f1a-93b7-24ebce87e243",
                Name = "Learn app development",
                Notes = "Attend Xamarin University",
                Done = true
            };

            var todoItem2 = new ToDoModel
            {
                ID = "b94afb54-a1cb-4313-8af3-b7511551b33b",
                Name = "Develop apps",
                Notes = "Use Xamarin Studio/Visual Studio",
                Done = false
            };

            var todoItem3 = new ToDoModel
            {
                ID = "ecfa6f80-3671-4911-aabe-63cc442c1ecf",
                Name = "Publish apps",
                Notes = "All app stores",
                Done = false,
            };

            _toDoList.Add(todoItem1);
            _toDoList.Add(todoItem2);
            _toDoList.Add(todoItem3);
        }




    }
    

}
