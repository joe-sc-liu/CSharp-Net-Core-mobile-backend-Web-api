# 適用給行動端存取的RESTful Web Api
參考網址<br>
https://docs.microsoft.com/zh-tw/aspnet/core/mobile/native-mobile-backend?view=aspnetcore-2.2<br><br>
此範例記錄，參考上述網址進行實作，感謝Steve Smith的無私教學<br>
使用.net core 2.2 c#，進行開發


## 依賴注入(Dependency Injection)

參考網址，感謝John Wu無私教學<br>
https://ithelp.ithome.com.tw/articles/10193172<br>
* Transient<br>
如預期，每次注入都是不一樣的實例。<br>
* Scoped<br>
在同一個 Requset 中，不論是在哪邊被注入，都是同樣的實例。<br>
->適合資料庫操作的service<br>
* Singleton<br>
不管 Requset 多少次，都會是同一個實例。<br>
->適合比較不容易改變的參數，比如網站的版本<br>
