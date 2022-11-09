using Microsoft.Win32;

namespace Windows11OldCMenuSetter;

internal class Program
{
    static void Main(string[] args)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        if (Environment.OSVersion.Version.Build < 22000)
        {
            SendError("Operating System is not Windows 11. Cancelling...");
            Console.ReadLine();
            return;
        }
        if (CreateKeys())
            SendSuccess("Keys created");
        else
            SendError("An error occured or User cancelled the task");
        string result = CheckKeys();
        if (result == "")
            SendSuccess("Keys checked. All keys successfully created.");
        else
            SendError("Error. key: " + result + " wasn't created");
        Console.ReadLine();
    }

    static bool CreateKeys()
    {
        Console.WriteLine("Creating Keys...");
        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true);
        if (key != null)
            Console.WriteLine(@"Found Key: " + key.ToString());
        else
        {
            SendError(@"Key: Computer\HKEY_CURRENT_USER\Software\Classes\CLSID wasn't found.");
            Console.Write("Do you want to Create the Key? (y/n)>");
            string result = Console.ReadLine().ToLower();
            if (result == "y" || result == "yes")
                key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID", true);
            else
                return false;
        }
        key = key.CreateSubKey("{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}", true);
        Console.WriteLine("Created Key: " + key);
        key = key.CreateSubKey("InprocServer32", true);
        Console.WriteLine("Created Key: " + key);
        key.SetValue("", "", RegistryValueKind.String);
        Console.WriteLine("Set Default Value of Key InprocServer32 to blank");
        return true;
    }

    static string CheckKeys()
    {
        Console.WriteLine("Checking created Keys...");

        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID");
        if (key == null)
        {
            SendError(@"Key: Computer\HKEY_CURRENT_USER\Software\Classes\CLSID wasn't found.");
            return @"Computer\HKEY_CURRENT_USER\Software\Classes\CLSID";
        }
        SendSuccess("Found Key: " + key.ToString());

        key = key.OpenSubKey("{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}");
        if (key == null)
        {
            SendError(@"Key: {86ca1aa0-34aa-4e8b-a509-50c905bae2a2} wasn't found.");
            return "{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}";
        }
        SendSuccess("Found Key: " + key.ToString());

        key = key.OpenSubKey("InprocServer32");
        if (key == null)
        {
            SendError(@"Key: InprocServer32 wasn't found.");
            return "InprocServer32";
        }
        SendSuccess("Found Key: " + key.ToString());

        string value = key.GetValue("").ToString();
        if (value != "")
        {
            SendError(@"Keyvalue InprocServer32 is: '" + value + "' But should be: ''");
            return "Value wrong";
        }
        SendSuccess(@"Keyvalue InprocServer32 is correct: '" + value + "'");
        return "";
    }

    static void SendError(string message)
    {
        ConsoleColor color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = color;
    }
    static void SendSuccess(string message)
    {
        ConsoleColor color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = color;
    }
}