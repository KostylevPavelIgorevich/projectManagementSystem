���� �������� ��������, ��� ������ ������ � �������� �� ���� API (����� ������� �������� �������), ������� �� ����� controllers, � ����� �� ����� ���������, ���, ��������, LogDb � LogConverter (��� ������������ �� ���� ��� ���������), 
����� �� �������� ������� �������� � ��� ����� ����� ������ � ������ ���������� �������.

1- ��� ������� ��������� � ������ ������� ���������� ������� ������� � PostgreSQL. ������� ��� �������� ������ �������� ����.

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

2- ���������������� ������� ������������. ������ �������� ����:
INSERT INTO users (id, name, password, role, login) VALUES (1, 'admin', 'qwe', 'Manager', 'admin1');
�����!!! ����� ������� ������������ �� ����� ������� � ��� ����� ��� ������ �������� � ������������ ������ ������� ������������.
�� ���� ����� �������, ����� ������� ������ ������� ������������. ��� ����� ������ (����� � ������) ����� ����� ������ �������.
������� � Program.cs, ������� 19-� �������: await UtilsPasswordMigrationHelper.HashAndUpdatePasswordForUser(userRepo, "admin1", "qwe", cancellationToken); � ������ ����� ������ ������ "admin1", "qwe".
3- ����� ���������� ������� ��������� ����������� ����� ������. � ��� ������ ������ �������� ���: public const string BaseConnectionStringPg = "Host=127.0.0.1; Database=postgres; Username=postgres; Password=49955;";
4- ��� ������� ��������� ������ ����� � ������ ������������ � ��������� � �����������. ��� ������ ������ ������ �����.

��� �� � ������� ��� �������: ������������ ������� ���������, ��� ��� ��� ���� �� ����������; ���������� SQL-������� �� ����� �����������; 
�������� JOIN-� ��� ����� �������������� ������; ���������� ��� �������������������; ���������� �������������� �� ��������� ���������; �������� ������ ����� ������� (get, add); ��������, �������� UI.


    