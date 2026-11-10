-- ១. បង្កើតតារាង users ប្រសិនបើវាមិនទាន់មាន
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='users' and xtype='U')
BEGIN
    CREATE TABLE users(
        id INT PRIMARY KEY IDENTITY(1,1),
        username VARCHAR(MAX) NULL,
        password VARCHAR(MAX) NULL,
        date_create DATE NULL
    );
END
GO

-- ២. បង្កើតតារាង Categories ប្រសិនបើវាមិនទាន់មាន
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categories' and xtype='U')
BEGIN
    CREATE TABLE Categories(
        id INT PRIMARY KEY IDENTITY(1,1),
        category VARCHAR(MAX) NULL,
        type VARCHAR(MAX) NULL,
        status VARCHAR(MAX) NULL,
        date_insert DATE NULL
    );
END
GO

-- ៣. បញ្ចូលប្រភេទ Expense (ការពារកុំឱ្យជាន់គ្នាបើមានរួចហើយ)
IF NOT EXISTS (SELECT * FROM Categories WHERE category = 'Food & Drinks' AND type = 'Expense')
    INSERT INTO Categories (category, type, status, date_insert) VALUES ('Food & Drinks', 'Expense', 'active', GETDATE());

IF NOT EXISTS (SELECT * FROM Categories WHERE category = 'Transportation' AND type = 'Expense')
    INSERT INTO Categories (category, type, status, date_insert) VALUES ('Transportation', 'Expense', 'active', GETDATE());

IF NOT EXISTS (SELECT * FROM Categories WHERE category = 'Shopping' AND type = 'Expense')
    INSERT INTO Categories (category, type, status, date_insert) VALUES ('Shopping', 'Expense', 'active', GETDATE());

IF NOT EXISTS (SELECT * FROM Categories WHERE category = 'Housing & Utilities' AND type = 'Expense')
    INSERT INTO Categories (category, type, status, date_insert) VALUES ('Housing & Utilities', 'Expense', 'active', GETDATE());
GO

-- ៤. បង្កើតតារាង income ប្រសិនបើវាមិនទាន់មាន
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='income' and xtype='U')
BEGIN
    CREATE TABLE income(
        id INT PRIMARY KEY IDENTITY(1,1),
        category VARCHAR(MAX) NULL,
        item VARCHAR(MAX) NULL,
        income VARCHAR(MAX) NULL,
        description TEXT NULL,
        date_income DATE NULL,
        date_insert DATE NULL
    );
END
GO

-- ៥. បង្កើតតារាង expenses ប្រសិនបើវាមិនទាន់មាន
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='expenses' and xtype='U')
BEGIN
    CREATE TABLE expenses(
        id INT PRIMARY KEY IDENTITY(1,1),
        category VARCHAR(MAX) NULL,
        item VARCHAR(MAX) NULL,
        cost VARCHAR(MAX) NULL,
        description TEXT NULL,
        date_expense DATE NULL,
        date_insert DATE NULL
    );
END
GO

-- ៦. មើលលទ្ធផលទិន្នន័យទាំងអស់ក្នុងតារាងនីមួយៗ
SELECT * FROM users;
SELECT * FROM Categories;
SELECT * FROM income;
SELECT * FROM expenses;