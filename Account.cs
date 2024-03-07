using System;
using System.Collections.Generic;

class Account
{
    public string AccountNumber { get; }
    public string Pin { get; }
    public decimal Balance { get; set; }
    public bool IsBlocked { get; set; }

    public Account(string accountNumber, string pin, decimal balance)
    {
        AccountNumber = accountNumber;
        Pin = pin;
        Balance = balance;
        IsBlocked = false;
    }
}

class ATM
{
    private Dictionary<string, Account> accounts;

    public ATM()
    {
        accounts = new Dictionary<string, Account>();
        // Populate with some dummy accounts
        accounts.Add("123456", new Account("123456", "1234", 1000));
        accounts.Add("789012", new Account("789012", "5678", 500));
    }

    public void Run()
    {
        Console.WriteLine("Welcome to the ATM!");

        while (true)
        {
            Console.WriteLine("Enter your account number:");
            string accountNumber = Console.ReadLine();

            if (accounts.ContainsKey(accountNumber))
            {
                Account account = accounts[accountNumber];

                if (account.IsBlocked)
                {
                    Console.WriteLine("Your account is blocked.");
                    break;
                }

                Console.WriteLine("Enter your PIN:");
                string pin = Console.ReadLine();

                if (pin == account.Pin)
                {
                    Console.WriteLine("Login successful!");
                    ShowOptions(account);
                }
                else
                {
                    Console.WriteLine("Incorrect PIN.");
                    HandleIncorrectPin(account);
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
    }

    private void ShowOptions(Account account)
    {
        Console.WriteLine("1. Check balance");
        Console.WriteLine("2. Make deposit");
        Console.WriteLine("3. Make withdrawal");
        Console.WriteLine("4. Transfer money");
        Console.WriteLine("5. Logout");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine($"Your balance: ${account.Balance}");
                break;
            case 2:
                Console.WriteLine("Enter amount to deposit:");
                decimal amountToDeposit = decimal.Parse(Console.ReadLine());
                account.Balance += amountToDeposit;
                Console.WriteLine("Deposit successful.");
                break;
            case 3:
                Console.WriteLine("Enter amount to withdraw:");
                decimal amountToWithdraw = decimal.Parse(Console.ReadLine());
                if (amountToWithdraw <= account.Balance)
                {
                    account.Balance -= amountToWithdraw;
                    Console.WriteLine("Withdrawal successful.");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
                break;
            case 4:
                Console.WriteLine("Enter recipient account number:");
                string recipientAccountNumber = Console.ReadLine();
                if (accounts.ContainsKey(recipientAccountNumber))
                {
                    Console.WriteLine("Enter amount to transfer:");
                    decimal amountToTransfer = decimal.Parse(Console.ReadLine());
                    if (amountToTransfer <= account.Balance)
                    {
                        account.Balance -= amountToTransfer;
                        accounts[recipientAccountNumber].Balance += amountToTransfer;
                        Console.WriteLine("Transfer successful.");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds.");
                    }
                }
                else
                {
                    Console.WriteLine("Recipient account not found.");
                }
                break;
            case 5:
                Console.WriteLine("Logging out...");
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }

        if (choice != 5)
        {
            ShowOptions(account);
        }
    }

    private void HandleIncorrectPin(Account account)
    {
        int attempts = 1;
        while (attempts <= 3)
        {
            Console.WriteLine($"Attempts remaining: {4 - attempts}");
            Console.WriteLine("Enter your PIN:");
            string pin = Console.ReadLine();
            if (pin == account.Pin)
            {
                Console.WriteLine("Login successful!");
                ShowOptions(account);
                return;
            }
            else
            {
                attempts++;
            }
        }
        Console.WriteLine("Too many incorrect attempts. Your account is blocked.");
        account.IsBlocked = true;
    }
}


