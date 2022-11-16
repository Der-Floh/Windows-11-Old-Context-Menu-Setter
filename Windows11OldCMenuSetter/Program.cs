using Microsoft.Win32;
using System.Diagnostics;

namespace Windows11OldCMenuSetter;

internal sealed class Program
{
    static void Main(string[] args)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        if (Environment.OSVersion.Version.Build >= 22000)
        {
            char aleradySet = CheckAlreadySet();
            if (aleradySet == 'n' || aleradySet == 'o')
            {
                if (CreateKeys())
                    SendSuccess("Keys created");
                else
                    SendError("An error occured or User cancelled the task");

                string result = CheckKeys();
                if (result == "")
                {
                    SendSuccess("Keys checked. All keys successfully created.");
                    RestartExplorer();
                }
                else
                    SendError("Error. key: " + result + " wasn't created");
            }
        }
        else
            SendError("Operating System is not Windows 11. Cancelling...");
        Console.ReadLine();
    }

    static char CheckAlreadySet()
    {
        string result = "";
        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32");
        if (key != null)
        {
            SendWarning("Key: " + key + " already exists. Checking value...");
            string value = key.GetValue("").ToString();
            if (value == "")
            {
                SendWarning("Value was already set correctly");
                Console.Write("Do you want to Remove it? (y/n)>");
                result = Console.ReadLine();
                if (result == "y" || result == "yes")
                {
                    SendError("Deleting Subkeys...");
                    Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID\", true).DeleteSubKeyTree("{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}");
                    SendError("Successfully deleted Subkeys");
                    return 'd';
                }
                else
                {
                    Console.Write("Do you want to Overwrite it? (y/n)>");
                    result = Console.ReadLine();
                    if (result == "y" || result == "yes")
                        return 'o';
                    else
                    {
                        Console.WriteLine("Exiting...");
                        return 'e';
                    }
                }
            }
            else
            {
                SendError("Value is wrong. Overwriting...");
                return 'o';
            }
        }
        else
            return 'n';
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
            return "value blank";
        }
        SendSuccess(@"Keyvalue InprocServer32 is correct: '" + value + "'");
        return "";
    }

    static void SendError(string message)
    {
        SendInColor(message, ConsoleColor.Red);
    }
    static void SendSuccess(string message)
    {
        SendInColor(message, ConsoleColor.Green);
    }
    static void SendWarning(string message)
    {
        SendInColor(message, ConsoleColor.Yellow);
    }
    static void SendInColor(string message, ConsoleColor color)
    {
        ConsoleColor colorOld = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = colorOld;
    }

    static void RestartExplorer()
    {
        using (Process process = new Process())
        {
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "taskkill.exe",
                Arguments = "-f -im explorer.exe",
                WindowStyle = ProcessWindowStyle.Hidden
            };
            process.Start();
            process.WaitForExit();
            process.StartInfo = new ProcessStartInfo("explorer.exe");
            process.Start();
        }
    }
}