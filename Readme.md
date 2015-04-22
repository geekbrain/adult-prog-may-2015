# Веб сервис проекта Wesite Rating

## Описание

Rest сервис для получения статистических данных, касающихся популярности имен в интернете.

Данные отдаются в формате JSON

Api расположен на хостинге вместе с сайтом. В скором времени появятся реальные адреса методов.
На момент работы с тестовыми данными возможны случаи игнорирования передаваймых параметров. Это временно

## Методы Api

* Получение общей статистики   
       
    урл: <http://mysite.com/stats>  
    метод: GET   
    Параметры: —  
        
* Получение ежедневной статистики    
       
    урл: <http://mysite.com/dailyStats>   
    метод: GET    
    Параметры:    
      * from - дата начала периода   
      * to - дата окночания периода   
      
     Дата в формате dd-mm-YYYY. Данный параметр не обязателен, без него отдастся вся имеющаяся статистика
     
* Получение статистики по имени    
       
    урл: <http://mysite.com/statsByName>   
    метод: GET    
    Параметры:    
      * name - имя   
          
* Получение списка имен    
       
    урл: <http://mysite.com/names>   
    метод: GET    
    Параметры: —       

* Получение списка сайтов    
       
    урл: <http://mysite.com/sites>   
    метод: GET    
    Параметры: —     
    
* Получение списка страниц     
       
    урл: <http://mysite.com/pages>   
    метод: GET    
    Параметры: —   
    
* Получение списка поисковых фраз    
       
    урл: <http://mysite.com/searchPhrases>   
    метод: GET    
    Параметры: —     
    
* Добавление сайта    
       
    урл: <http://mysite.com/site>   
    метод: POST    
    Параметры:    
      * url - урл сайта       
      
     В формате http://host.ru
     
* Добавление имени    
       
    урл: <http://mysite.com/name>   
    метод: POST    
    Параметры:    
      * name - имя
     
* Добавление поисковой фразы    
       
    урл: <http://mysite.com/searchPhrase>   
    метод: POST    
    Параметры:    
      * phrase - фраза для поиска    
      * name - имя к которому эта фраза привязана   