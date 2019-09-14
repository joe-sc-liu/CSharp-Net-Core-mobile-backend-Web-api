using System;
using AppWebApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppWebApi.Models;
using log4net;

namespace AppWebApi.Controllers
{
    /// <summary>
    /// http://ip:port/api/ToDo
    /// Startup.cs的ConfigureServices，控制api的port
    /// 請注意底下「ToDoController」中的Controller，是不能改的哦！請注意！
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly static ILog logger = LogManager.GetLogger(typeof(Program));

        private readonly IToDo _IToDo;


        public ToDoController(IToDo iToDo)
        {
            _IToDo = iToDo;
        }

        public enum ErrorCode
        {
            TodoItemNameAndNotesRequired,
            TodoItemIDInUse,
            RecordNotFound,
            CouldNotCreateItem,
            CouldNotUpdateItem,
            CouldNotDeleteItem
        }


        /// <summary>
        /// 相當於mvc的index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_IToDo.All);
        }

        /// <summary>
        /// api/todo/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ToDoModel GetTodoById(string id)
        {
            try
            {
                //判斷傳入的值是不是空的，且會驗證model裡定義的檢查條件，必須通過
                if (id == null || !ModelState.IsValid)
                {
                    return null;
                }

                return _IToDo.Find(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
        }


        /// <summary>
        /// 新增一筆ToDo，請注意，一定要用「Create」，post過來才接的到
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] ToDoModel item)
        {
            try
            {
                //判斷傳入的值是不是空的，且會驗證model裡定義的檢查條件，必須通過
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
                }
                bool itemExists = _IToDo.DoesItemExist(item.ID);
                if (itemExists)
                {
                    return StatusCode(StatusCodes.Status409Conflict, ErrorCode.TodoItemIDInUse.ToString());
                }
                _IToDo.Insert(item);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
            }

            return Ok(item);
        }


        [HttpPut]
        public IActionResult Edit([FromBody] ToDoModel item)
        {
            try
            {
                //判斷傳入的值是不是空的，且會驗證model裡定義的檢查條件，必須通過
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());//回應 BadRequest 400
                }
                var existingItem = _IToDo.Find(item.ID);
                if (existingItem == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());//會回應NotFound 404給前端
                }

                _IToDo.Update(item);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
            }

            return NoContent();//回應204，空值成功訊息
        }




        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var item = _IToDo.Find(id);
                if (item == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());//會回應NotFound 404給前端
                }
                _IToDo.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotDeleteItem.ToString());
            }

            return NoContent();//回應204，空值成功訊息
        }





    }
}