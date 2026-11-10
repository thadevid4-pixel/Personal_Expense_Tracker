# Personal Expense Tracker - requirement checklist

## FIRST: make a copy of `Expense.mdf` before the first run
On the first start the program upgrades the database: it adds the role column and replaces every plain-text
password with a PBKDF2 hash. **Hashing cannot be undone**, so keep a copy of `Expense.mdf` (and `Expense_log.ldf`)
if you want to be able to go back.

## Required features
| Requirement | Where it is |
|---|---|
| Login and logout | `Form1.cs` (login), `MainForm.cs` > `btnLogout_Click` / `SignOut()`, `Session.cs` |
| Two user roles | **Admin** and **Staff** - column `users.user_role`, `Session.IsAdmin`, `Db.Migrate()` (see table below) |
| Add / update / delete / view | Income, Expenses, Categories pages; Users tab (Admin page) |
| Search and filtering | `FilterBar.cs` on the Income and Expenses pages: text search, category filter, date range, Reset |
| Input validation + clear messages | `Db.TryParseAmount` (amounts), required-field checks, password rule (min 8), duplicate category / username checks |
| SQL Server database | LocalDB `Expense.mdf`, `Db.cs` (parameterized queries only) |
| DataGridView | Income, Expenses, Categories, Users, Reports (2 grids) |
| Confirmation messages | Update, Delete, Logout, Close, Role change, Reset password, Delete user, Restore |
| Exception handling | try/catch around every database / file action, friendly message boxes, startup database check in `Program.cs` |
| Summary report | **Reports** page: `ReportsPage.cs` (totals, net balance, income / expenses by category, 6-month chart) |
| Clean consistent interface | same green theme / fonts on every page, new pages built in the same style |

## Optional features chosen
| Feature | Where it is |
|---|---|
| Dashboard statistics | `Dashboard_Form.cs` (today / yesterday / month / year / total, for income and expenses) |
| Charts | `MonthlyChart.cs` - Income vs Expenses per month (Reports page), drawn with GDI+ |
| Export to CSV | Income page, Expenses page (respects the current filter) and the full Reports page - `CsvHelper.cs` |

## Advanced class features chosen
| Feature | Where it is |
|---|---|
| Password hashing | `PasswordHasher.cs` - PBKDF2-SHA256, 100,000 rounds, random 16-byte salt per password, constant-time compare |
| Database backup and restore | Admin page > "Backup and Restore" tab - `BackupService.cs` (xml file, all-or-nothing restore, automatic safety copy) |

(Email notification was not chosen.)

## What each role can do
| | Admin | Staff |
|---|---|---|
| Dashboard, Reports (+ export) | yes | yes |
| Income / Expenses: view, search, add, update | yes | yes |
| Income / Expenses: delete | yes | no ("Only administrators can delete records") |
| Categories: view | yes | yes |
| Categories: add / update / delete | yes | no (buttons are grey) |
| Admin page: users (change role, reset password, delete), backup, restore | yes | no (button is hidden) |

* The **first account ever created** is the Administrator. In your existing database the oldest user becomes Admin;
  everyone else becomes Staff. People who use **Register** become Staff.
* An Admin cannot change or delete their own account (so the system always keeps an administrator).

## Quick test script (for your demo)
1. Log in as your old admin user (the password is unchanged, it is just stored hashed now).
2. Register a new user, log out, log in as that user -> Admin button missing, Delete shows "Only administrators...".
3. Income page: type in Search, pick a Category, tick Date -> the grid filters; "Export CSV" saves what you see.
4. Reports page: pick a date range -> totals, category tables and chart update; "Export CSV".
5. Admin > Users: make the new user Admin, reset a password, delete a user.
6. Admin > Backup and Restore: Backup now -> change some data -> Restore from file -> data is back, you are signed out.
