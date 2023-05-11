using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string password;
            int userMoney;
            int count = 3;

            Console.WriteLine($"У вас {count} попыток");
            Console.Write("Введите ваш пароль: ");

            for (int i = 0; i < count; i++)
            {
                password = Console.ReadLine();

                if (password == "0101")
                {
                    Console.Write("Введите сумму: ");
                    userMoney = Convert.ToInt32(Console.ReadLine());
                    new ATM(userMoney, password);
                    new Operations(userMoney, password).Menu();
                    return;
                }
                else
                {
                    Console.WriteLine($"Вы ввели не корректную пароль у вас осталось {count - (i + 1)} попыток");
                    Console.Write("Введите ваш пароль: ");
                }



            }

        }

    }
    class ATM
    {
        protected int UserMoney;
        protected string Password;
        protected bool IsOpen = true;

        public ATM(int userMoney, string password)
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

        public Operations(int userMoney, string password) : base(userMoney, password) { }

        public void Menu()
        {
            List<string> menuTitles = new List<string>() { "Перевод денег", "Изменить пароль", "Обналичить денег", "Внести", "Выйти" };
            string numberOfOperations;

            while (IsOpen)
            {
                CustomText(new String('=', 40), true);
                CustomText("Добро пожаловать в меню\n", true);
                CustomText($"Денег на счету: {UserMoney}\n", true);
                for (int i = 0; i < menuTitles.Count; i++)
                {
                    CustomText($"{i + 1} - {menuTitles[i]}\n", true);
                }
                CustomText(new String('=', 40), true);

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
                    CustomText($"У вас на счету: {UserMoney} денег", true);
                    CustomText("Сколько вы хотите перевести денег: ");

                    int moneyTransfer = Convert.ToInt32(Console.ReadLine());

                    if (moneyTransfer < 0 || moneyTransfer > UserMoney)
                    {
                        CustomText("Пожалуйста введите корректную сумму: ");
                    }
                    else
                    {
                        UserMoney -= moneyTransfer;
                        CustomText($"Вы перевели {moneyTransfer} у вас осталось на счету {UserMoney}", true);
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
                        CustomText("Пароль подвержден", true);
                        CustomText("Введите новый пароль, не больше 4 символов: ");
                        newPassword = Console.ReadLine();

                        if (newPassword.Length == 4)
                        {
                            Password = newPassword;
                            CustomText("Пароль успешно изменино, чтоб продолжить нажмите на любую клавишу ");
                        }
                        else
                        {
                            CustomText("Вы ввели больше или меньше чем 4 символов", true);
                        }
                    }
                    else
                    {
                        CustomText("Попробуйте снова", true);
                    }
                    break;

                case "3":
                    int cashOut;

                    CustomText($"У вас на счету {UserMoney} денег", true);
                    CustomText("Сколько вы хотите обноличить: ");

                    cashOut = Convert.ToInt32(Console.ReadLine());

                    if (cashOut > 0 && cashOut <= UserMoney)
                    {
                        UserMoney -= cashOut;
                        CustomText($"Вы перевели {cashOut}, у вас на счету осталось {UserMoney} денег", true);
                    }
                    else
                    {
                        CustomText("У вас не достаточно средств чтобы выполнить эту операцию", true);
                    }
                    break;

                case "4":
                    int depositMoney;

                    CustomText("Введите сумму: ");

                    depositMoney = Convert.ToInt32(Console.ReadLine());

                    if (depositMoney > 0)
                    {
                        UserMoney += depositMoney;
                        CustomText($"Вы внесли {depositMoney}, у вас на счету {UserMoney} денег", true);
                    }
                    else
                    {
                        CustomText("Вы ввели не корректную сумму", true);
                    }
                    break;

                case "5":
                    IsOpen = false;
                    break;

                default:
                    CustomText("Вы выбрали не правильную операцию", true);
                    break;
            }
        }
    }


}
