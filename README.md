# DictionaryService

Приложение представляет собой доступное по сети хранилище (сервис), все данные (пары ключ-значение) которого хранятся в памяти и при перезапуске хранилища исчезают.<br>
<br>
Хранилище позволяет установить значение ключу, прочитать значение ключа, удалить значение ключа, а так же получить список всех ключей, у которых есть значения.<br>
<br>
<b>Для получения списка всех ключей</b>, у которых есть значения, нужно сделать GET запрос по корневому адресу.<br>
Список ключей (в виде строк) будет передан через перевод строки.<br>
<br>
<b>Для добавления пары ключ-значение</b> нужно сделать POST запрос по корневому адресу + / + ключ, в теле запроса в виде текста указать значение.
В ответе вернется код 200 и в теле ответа добавленное значение.<br>
<br>
<b>Для получения значения</b> нужно сделать POST запрос по корневому адресу + / + ключ.<br>
В случае, если ключ существует, в ответе вернется код 200, в теле ответа вернется значение этого ключа.<br>
В случае, если ключа не существует, вернется код 404.<br>
<br>
<b>Для удаления значения ключа</b> требуется сделать DELETE запрос на по корневому адресу + / + ключ.<br>
В случае удачи в ответе вернется код 200, а втеле ответа ключ.<br>
Сам ключ не удаляется, удаляется только значение.<br>