using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ChangeHeadphones
{
    internal static class Program
    {
        private static async Task Main()
        {
            const string eqFolder = @"C:\Program Files\EqualizerAPO\config";
            string defaultConfigFile = Path.Combine(eqFolder, "config.txt");
            string sennConfigFile = Path.Combine(eqFolder, "senn.txt");
            string hifimanConfigFile = Path.Combine(eqFolder, "hifiman.txt");

            if (!Directory.Exists(eqFolder))
            {
                Console.WriteLine("EQ Folder not found!");
                Console.ReadLine();
                return;
            }

            if (!File.Exists(defaultConfigFile))
            {
                Console.WriteLine("No configs file found!");
                Console.ReadLine();
                return;
            }

            CurrentProfile profile;
            if (File.Exists(sennConfigFile))
                profile = CurrentProfile.HiFiMan;
            else if (File.Exists(hifimanConfigFile))
                profile = CurrentProfile.Sennheiser;
            else
                return;

            var sb = new StringBuilder("Current profile set is: ");
            sb.Append(profile == CurrentProfile.HiFiMan ? CurrentProfile.HiFiMan.ToString() : CurrentProfile.Sennheiser.ToString());
            Console.WriteLine(sb.ToString());

            sb = new StringBuilder("Do you want to switch to the ");
            sb.Append(profile == CurrentProfile.HiFiMan ? CurrentProfile.Sennheiser.ToString() : CurrentProfile.HiFiMan.ToString());
            sb.Append(" profile?");
            Console.WriteLine(sb.ToString());

            ConsoleKeyInfo userKey;
            Console.WriteLine("Press Y/N to proceed.");
            do
            {
                userKey = Console.ReadKey();
            } while (userKey.Key != ConsoleKey.Y && userKey.Key != ConsoleKey.N);

            Console.WriteLine();

            if (userKey.Key == ConsoleKey.N)
            {
                Console.WriteLine("Exiting...");
                await Task.Delay(1000);
                return;
            }

            if (profile == CurrentProfile.HiFiMan)
            {
                File.Move(defaultConfigFile, hifimanConfigFile);
                File.Move(sennConfigFile, defaultConfigFile);
            }
            else
            {
                File.Move(defaultConfigFile, sennConfigFile);
                File.Move(hifimanConfigFile, defaultConfigFile);
            }

            sb = new StringBuilder("Profile switched from ");
            sb.Append(profile == CurrentProfile.HiFiMan ? CurrentProfile.HiFiMan.ToString() : CurrentProfile.Sennheiser.ToString());
            sb.Append(" to ");
            sb.Append(profile == CurrentProfile.HiFiMan ? CurrentProfile.Sennheiser.ToString() : CurrentProfile.HiFiMan.ToString());
            Console.WriteLine(sb.ToString());

            Console.WriteLine("Exiting...");
            await Task.Delay(1000).ConfigureAwait(false);
        }
        
        private enum CurrentProfile
        {
            HiFiMan,
            Sennheiser
        }
    }
}
