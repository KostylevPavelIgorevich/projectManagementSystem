Хочу обратить внимание, что данный проект я выполнял на базе API (моего старого учебного проекта), поэтому на папку controllers, а также на такие фрагменты, как, например, LogDb и LogConverter (они присутствуют во всех трёх сущностях), 
можно не обращать особого внимания — они будут нужны только в случае расширения проекта.

1- Для запуска программы в первую очередь необходимо создать таблицы в PostgreSQL. Скрипты для создания табоиц прилагаю ниже.

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name TEXT,
    password TEXT,
    role TEXT,
    login TEXT
);

CREATE TABLE project_tasks (
    id SERIAL PRIMARY KEY,
    title TEXT,
    description TEXT,
    status TEXT,
    assignedUserId INTEGER
);

CREATE TABLE logs (
    id SERIAL PRIMARY KEY,
    task_id INTEGER,
    user_id INTEGER,
    message TEXT,
    createdat TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

2- Зарегистрировать первого пользователя. Скрипт прилагаю ниже:
INSERT INTO users (id, name, password, role, login) VALUES (1, 'admin', 'qwe', 'Manager', 'admin1');
ВАЖНО!!! Лучше создать пользователя по моему скрипту — это нужно для обхода проблемы с хешированием пароля первого пользователя.
Но если очень хочется, можно создать своего первого пользователя. Для этого данные (логин и пароль) нужно будет ввести вручную.
Заходим в Program.cs, находим 19-ю строчку: await UtilsPasswordMigrationHelper.HashAndUpdatePasswordForUser(userRepo, "admin1", "qwe", cancellationToken); и вводим новые данные вместо "admin1", "qwe".
3- Затем необходимо сделать локальное подключение через строку. В моём случае строка выглядит так: public const string BaseConnectionStringPg = "Host=127.0.0.1; Database=postgres; Username=postgres; Password=49955;";
4- При запуске программы вводим логин и пароль пользователя и переходим к функционалу. Для выбора вводим гужную цифру.

Что бы я добавил или изменил: переработать текущую структуру, так как она явно не оптимальна; переписать SQL-запросы на более эффективные; 
добавить JOIN-ы для более информативного вывода; поработать над отказоустойчивостью; полноценно протестировать на различных сценариях; улучшить логику самих методов (get, add); возможно, добавить UI.


    