using System;
using System.Collections.Generic;


namespace ATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string password;
            float userMoney;
            int count = 3;
            bool isLineText = true;

            CustomeText($"У вас {count} попыток", isLineText);
            CustomeText("Введите ваш пароль: ");

            for (int i = 0; i < count; i++)
            {
                password = Console.ReadLine();

                if (password == "0101")
                {
                    CustomeText("Введите сумму: ");
                    userMoney = Convert.ToSingle(Console.ReadLine());
                    new ATM(userMoney, password);
                    new Operations(userMoney, password).Menu();
                    return;
                }
                else
                {
                    CustomeText($"Вы ввели не корректную пароль у вас осталось {count - (i + 1)} попыток", isLineText);
                    CustomeText("Введите ваш пароль: ");
                }



            }

        }

        static void CustomeText(string text, bool isLine = false)
        {
            if (!isLine)
            {

                Console.Write(text);
            }
            else
            {
                Console.WriteLine(text);
            }
        }

    }
    class ATM
    {
        protected float UserMoney;
        protected string Password;
        protected bool IsOpen = true;
        protected bool isLineText = true;

        public ATM(float userMoney, string password)
        {
            UserMoney = userMoney;
            Password = password;
        }

        protected void CustomText(string text, bool isLine = false)
        {
            if (!isLine)
            {

                Console.Write(text);
            }
            else
            {
                Console.WriteLine(text);
            }
        }

    }


    class Operations : ATM
    {

        public Operations(float userMoney, string password) : base(userMoney, password) { }

        public void Menu()
        {
            List<string> menuTitles = new List<string>() { "Перевод денег", "Изменить пароль", "Обналичить денег", "Внести", "Выйти" };
            string numberOfOperations;

            while (IsOpen)
            {
                CustomText(new String('=', 40), isLineText);
                CustomText("Добро пожаловать в меню\n", isLineText);
                CustomText($"Денег на счету: {UserMoney}\n", isLineText);
                for (int i = 0; i < menuTitles.Count; i++)
                {
                    CustomText($"{i + 1} - {menuTitles[i]}\n", isLineText);
                }
                CustomText(new String('=', 40), isLineText);

                CustomText("Выберите номер для операций: ");
                numberOfOperations = Console.ReadLine();

                MenuOperation(numberOfOperations);

                Console.ReadKey();
                Console.Clear();

            }
        }

        public void MenuOperation(string numberOfOperations)
        {
            switch (numberOfOperations)
            {
                case "1":
                    CustomText($"У вас на счету: {UserMoney} денег", isLineText);
                    CustomText("Сколько вы хотите перевести денег: ");

                    float moneyTransfer = Convert.ToSingle(Console.ReadLine());

                    if (moneyTransfer < 0 || moneyTransfer > UserMoney)
                    {
                        CustomText("Пожалуйста введите корректную сумму: ");
                    }
                    else
                    {
                        UserMoney -= moneyTransfer;
                        CustomText($"Вы перевели {moneyTransfer} у вас осталось на счету {UserMoney}", isLineText);
                        CustomText("Чтоб родолжить нажмите на любую клавишу: ");
                    }
                    break;

                case "2":
                    string isValidPassword;
                    string newPassword;

                    CustomText("Подтвердите свой пароль: ");

                    isValidPassword = Console.ReadLine();

                    if (isValidPassword == Password)
                    {
                        CustomText("Пароль подтвержден", isLineText);
                        CustomText("Введите новый пароль, не больше 4 символов: ");
                        newPassword = Console.ReadLine();

                        if (newPassword.Length == 4)
                        {
                            Password = newPassword;
                            CustomText("Пароль успешно изменино, чтоб продолжить нажмите на любую клавишу ");
                        }
                        else
                        {
                            CustomText("Вы ввели больше или меньше чем 4 символов", isLineText);
                        }
                    }
                    else
                    {
                        CustomText("Попробуйте снова", isLineText);
                    }
                    break;

                case "3":
                    float cashOut;

                    CustomText($"У вас на счету {UserMoney} денег", isLineText);
                    CustomText("Сколько вы хотите обноличить: ");

                    cashOut = Convert.ToSingle(Console.ReadLine());

                    if (cashOut > 0 && cashOut <= UserMoney)
                    {
                        UserMoney -= cashOut;
                        CustomText($"Вы перевели {cashOut}, у вас на счету осталось {UserMoney} денег", isLineText);
                    }
                    else
                    {
                        CustomText("У вас не достаточно средств чтобы выполнить эту операцию", isLineText);
                    }
                    break;

                case "4":
                    float depositMoney;

                    CustomText("Введите сумму: ");

                    depositMoney = Convert.ToSingle(Console.ReadLine());

                    if (depositMoney > 0)
                    {
                        UserMoney += depositMoney;
                        CustomText($"Вы внесли {depositMoney}, у вас на счету {UserMoney} денег", isLineText);
                    }
                    else
                    {
                        CustomText("Вы ввели некорректную сумму", isLineText);
                    }
                    break;

                case "5":
                    IsOpen = false;
                    break;

                default:
                    CustomText("Вы выбрали не правильную операцию", isLineText);
                    break;
            }
        }
    }
}
