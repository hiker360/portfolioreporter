using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

public class ExcelReaderService
{
    private readonly string _filePath;

    public ExcelReaderService(string filePath)
    {
        _filePath = filePath;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set license context
    }

    public List<Account> ReadAccounts()
    {
        var accounts = new List<Account>();
        using (var package = new ExcelPackage(new FileInfo(_filePath)))
        {
            var worksheet = package.Workbook.Worksheets["Accounts"];
            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                accounts.Add(new Account
                {
                    Name = worksheet.Cells[row, 1].Text,
                    Group = worksheet.Cells[row, 2].Text,
                    AccountType = worksheet.Cells[row, 3].Text
                });
            }
        }
        return accounts;
    }

    public List<Balance> ReadBalances()
    {
        var balances = new List<Balance>();
        using (var package = new ExcelPackage(new FileInfo(_filePath)))
        {
            var worksheet = package.Workbook.Worksheets["Balances"];
            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                balances.Add(new Balance
                {
                    Account = worksheet.Cells[row, 1].Text,
                    BalanceDate = DateTime.Parse(worksheet.Cells[row, 2].Text),
                    BalanceAmount = decimal.Parse(worksheet.Cells[row, 3].Text),
                    CashIn = decimal.Parse(worksheet.Cells[row, 4].Text),
                    CashOut = decimal.Parse(worksheet.Cells[row, 5].Text)
                });
            }
        }
        return balances;
    }
}
