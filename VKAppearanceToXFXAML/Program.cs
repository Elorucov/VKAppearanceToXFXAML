using System;
using System.IO;

namespace VKAppearanceToXFXAML {
    class Program {
        static void Main(string[] args) {
            Go();
        }

        private async static void Go() {
            Console.WriteLine("VK appearance to Xamarin.Forms XAML converter by ELOR.");
            Console.WriteLine("Enter class name:");
            string className = Console.ReadLine();
            Console.WriteLine("");

            Progress<string> progress = new Progress<string>();
            progress.ProgressChanged += (a, b) => Console.WriteLine(b);
            var files = Parser.DoItAsync(progress, className ?? "YourApp").Result;
            if (files != null) {
                try {
                    string path = Environment.CurrentDirectory + "\\VKAppearance";
                    Directory.CreateDirectory(path);
                    foreach (var file in files) {
                        Console.WriteLine($"Saving {file.Key}...");
                        File.WriteAllText($"{path}\\{file.Key}", file.Value);
                    }
                    Console.WriteLine($"All done! Go to {path}");
                    if (String.IsNullOrEmpty(className)) Console.WriteLine("Don't forget to change the value of \"x:Class\" in the XAML files!!!");
                } catch (Exception ex) {
                    Console.WriteLine($"Error 0x{ex.HResult.ToString("x8")}: {ex.Message}");
                }
            } else {
                Console.WriteLine("No files.");
            }
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
