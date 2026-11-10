# What was fixed

## 1. Dashboard did not show new Income / Expenses (your notes file)
- `Dashboard_Form.cs` now reloads **every time the dashboard is shown** (Load + VisibleChanged + the
  Dashboard button). No more "Register or Run Code" to see new data.
- Amounts are stored as TEXT (`VARCHAR(MAX)`) in the database. The old `CAST(... AS DECIMAL(10,2))`
  crashed the whole dashboard if ANY row held text like `abc`, `$50` or `1,000`. Now `TRY_CAST(... DECIMAL(18,2))`
  is used, so one bad value can never break the page (and the larger precision stops overflow).
- Date ranges are now `>= start AND < next day/month/year` (no more missed "last day of the month" rows).
- All 10 numbers use ONE database connection instead of 10.

## 2. Hard-coded database path
- Every file had `D:\Safe Folder\Learning code\...\Expense.mdf`. New file `Db.cs` holds the connection string
  in ONE place and finds `Expense.mdf` automatically, wherever the project is copied (other PC, other drive).
- If the database or SQL Server LocalDB is missing, a clear message is shown instead of a crash.

## 3. App kept running after closing the window / logout piled up hidden windows
- Closing the main window, login window or register window with the X button now exits the app
  (before, the hidden login window kept the process alive; Visual Studio stayed in "debugging").
- Logout really closes the main window; the X button asks for confirmation.

## 4. Income / Expenses forms
- The amount is validated (`25`, `25.50`, `1,250.75`, `$30` accepted; letters, 0, negatives rejected) and saved
  as `25.00`, so the dashboard always gets clean numbers.
- Date picker saves the date only (no time part).
- Clicking a grid row no longer crashes on PCs whose date format is not US (`09-30-2026` is parsed safely).
- Update/Delete check that a row is really selected and tell you if nothing was changed.
- Forms no longer query the database while shown in the Visual Studio designer.

## 5. Category form (data-loss bug)
- `Clear` did not forget the selected row, so typing new values and pressing Delete/Update could delete or
  overwrite the PREVIOUSLY selected category. Fixed (selection is reset; Delete/Update require a selected row).
- "Deleted/Updated successfully" is shown only when a row was really changed.
- Duplicate category (same name + same type) is rejected.

## 6. Login / Register
- Login: empty-field check, Enter key works, error message instead of crash if the database is unavailable.
- Register: safe username handling.

## 7. NuGet restore errors / "cannot start debugging ... .exe is missing"
- The project listed 47 NuGet packages (Azure.Identity, Microsoft.Data.SqlClient.SNI, ...) that the code never
  uses: it only needs `System.Data.SqlClient`, which is already part of .NET Framework 4.7.2.
- All of them were removed from `Personal Expense Tracker.csproj` (plus `packages.config`, the package imports,
  the "restore check" build step and the old binding redirects in `App.config`).
- Result: the project builds with NO package restore and NO internet, the zip is ~5 MB instead of 65 MB, and
  Windows' 260-character path limit can no longer break the build.

## 8. New features (see FEATURES.md)
Roles (Admin / Staff), search + filter, Reports page with chart, CSV export, password hashing, backup and restore.

## Not changed on purpose
- Designer files (`*.Designer.cs`, `*.resx`) were NOT touched, so your layout is exactly the same.

## Files changed
New: `Db.cs`   |   Changed: `Program.cs`, `Form1.cs`, `RigisterForm.cs`, `MainForm.cs`, `Dashboard_Form.cs`,
`IncomeForm.cs`, `ExpensesForm.cs`, `CategoryControlForm.cs`, `IncomeData.cs`, `ExpensesData.cs`,
`CategoryData.cs`, `App.config`, `Personal Expense Tracker.csproj` (includes Db.cs, NuGet removed)
Deleted: `packages.config`
