using Microsoft.Win32;

namespace Windows11OldCMenuSetter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool result = CreateKey();
            if (result)
                Console.WriteLine("Keys succesfully Created");
            else
                Console.WriteLine("An error occured or User cancelled the task");
            Console.ReadLine();
        }

        static bool CreateKey()
        {
            Console.WriteLine("Creating Keys...");
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true);
            if (key != null)
                Console.WriteLine(@"Found Key: " + key.ToString());
            else
            {
                Console.WriteLine(@"Key: Computer\HKEY_CURRENT_USER\Software\Classes\CLSID wasnt found.");/*
                Console.Write("Do you want to Create the Key? (y/n)>");
                string result = Console.ReadLine().ToLower();
                if (result == "y" || result == "yes")
                    key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID", true);
                else*/
                    return(false);
            }
            //key = key.CreateSubKey("{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}", true);
            Console.WriteLine("Created Key: " + key);
            //key = key.CreateSubKey("InprocServer32", true);
            Console.WriteLine("Created Key: InprocServer32");
            //key.SetValue("", "", RegistryValueKind.String);
            Console.WriteLine("Set Default Value of Key InprocServer32 to blank");
            return(true);
        }
    }
}