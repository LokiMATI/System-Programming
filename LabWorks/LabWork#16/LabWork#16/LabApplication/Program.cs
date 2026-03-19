using System.CommandLine;
using System.Text;
using Terminal.Gui.App;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

Option<string> loginOption = new("--login");
Option<string> passwordOption = new("--pwd");
Option<bool> uiOption = new("--ui");

RootCommand root = new()
{
    loginOption,
    passwordOption,
    uiOption
};

root.SetAction(parseResult =>
{
    LoginForm(
        parseResult.GetValue(loginOption),
        parseResult.GetValue(passwordOption),
        parseResult.GetValue(uiOption));
});



Command registerCommand = new("register")
{
    loginOption,
    passwordOption
};

registerCommand.SetAction(parseResult => Register(
    parseResult.GetValue(loginOption),
    parseResult.GetValue(passwordOption))
);

root.Subcommands.Add(registerCommand);

root.Parse(args).Invoke();

static void LoginForm(string? login = null, string? password = null, bool isUi = false)
{
    if (isUi)
    {
        AuthTui(login, password);
        return;
    }

    CliAuth(login, password);
}

static void AuthTui(string? login, string? password)
{
    using IApplication app = Application.Create().Init();

    using Window window = new()
    {
        Title = "LabWork"
    };

    Label loginLabel = new()
    {
        Text = "Логин",
        X = Pos.Center(),
        Y = Pos.Top(window)
    };
    TextField loginTextField = new()
    {
        X = Pos.Center(),
        Y = Pos.Bottom(loginLabel),
        Width = Dim.Fill(30),
    };
    if (login is not null)
        loginTextField.Text = login;

    Label passwordLabel = new()
    {
        Text = "Пароль",
        X = Pos.Center(),
        Y = Pos.Bottom(loginTextField) + 1
    };
    TextField passwordTextField = new()
    {
        X = Pos.Center(),
        Y = Pos.Bottom(passwordLabel),
        Width = Dim.Fill(30),
    };
    if (password is not null)
        passwordTextField.Text = password;

    Button loginButtnon = new()
    {
        Text = "Login",
        X = Pos.Center(),
        Y = Pos.Bottom(passwordTextField) + 2,
        Width = Dim.Fill(30),

    };
    loginButtnon.Accepting += (s, e) =>
    {
        login = loginTextField.Text;
        password = passwordTextField.Text;

        using var sr = new StreamReader(@"C:\Users\221\Students\ISPP-31\System-Programming\LabWorks\LabWork#16\users.txt");

        string user;
        while ((user = sr.ReadLine()) != null)
        {
            if (login == user.Trim().Split(',')[0])
            {
                var userPassword = user.Trim().Split(',')[1];

                if (password == userPassword)
                    MessageBox.Query(app!, "Пароль верен!", "Авторизация успешна", "Ок");
                else
                    MessageBox.ErrorQuery(app!, "Пароль не верен!", "Авторизация провалена", "Ок");

                return;
            }
        }
        MessageBox.ErrorQuery(app!, "Пользователь с таким именем не найден", "Авторизация провалена", "Ок");
        return;
    };

    window.Add(loginLabel, loginTextField, passwordLabel, passwordTextField, loginButtnon);

    app.Run(window);
}

static void CliAuth(string? login = null, string? password = null)
{
    Console.WriteLine("Процесс авторизации.");
    while (true)
    {
        if (login is null)
        {
            Console.Write("Введите логин: ");
            login = Console.ReadLine();
        }

        using var sr = new StreamReader(@"C:\Users\221\Students\ISPP-31\System-Programming\LabWorks\LabWork#16\users.txt");

        string user;
        while ((user = sr.ReadLine()) != null)
        {
            if (login == user.Trim().Split(',')[0])
            {
                var userPassword = user.Trim().Split(',')[1];
                if (password is not null)
                {
                    Console.WriteLine(password == userPassword ?
                        "Пароль верен!" : "Пароль не верен!");
                    return;
                }

                Console.Write("Введите пароль: ");
                for (int i = 0; i < 3; i++)
                {
                    password = InputPassword();

                    if (password == userPassword)
                    {
                        Console.WriteLine("Пароль верен!");
                        return;
                    }
                    Console.Write("Вы ввели неверный пароль.\nПопробуйте снова: ");
                }

                Console.WriteLine();
                break;
            }
        }
    }
}

static void Register(string? login = null, string? password = null)
{
    Console.WriteLine("Процесс регистрации.");
    while (true)
    {
        if (login is null)
        {
            Console.Write("Введите логин: ");
            var inputLogin = Console.ReadLine();
        }

        using var sr = new StreamReader(@"C:\Users\221\Students\ISPP-31\System-Programming\LabWorks\LabWork#16\users.txt");

        string user;
        while ((user = sr.ReadLine()) != null)
        {
            if (login == user.Trim().Split(',')[0])
            {
                Console.WriteLine("Поьзователь с таким логином уже существует.");
                return;
            }
        }

        sr.Dispose();
        while (true)
        {
            if (password is null)
            {
                Console.Write("Введите пароль: ");
                password = InputPassword();
                do
                {
                    Console.Write("Введите подтверждение пароля: ");
                }
                while (password != InputPassword());
            }

            using var cw = new StreamWriter(@"C:\Users\221\Students\ISPP-31\System-Programming\LabWorks\LabWork#16\users.txt", true);
            cw.WriteLine($"{login},{password}");
            Console.WriteLine("Вы зарегестрированы!");

            return;
        }
    }
}

static string InputPassword()
{
    StringBuilder inputPassword = new();

    ConsoleKeyInfo key;
    while (true)
    {
        key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.Enter)
            break;

        if (key.Key == ConsoleKey.Backspace && inputPassword.Length > 0)
        {
            inputPassword.Remove(inputPassword.Length - 1, 1);
            continue;
        }

        inputPassword.Append(key.KeyChar);
    }
    Console.WriteLine();
    return inputPassword.ToString(); ;
}
