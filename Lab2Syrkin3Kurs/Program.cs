using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab2Syrkin3Kurs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выполняется лабораторная работа 2:");
            Console.WriteLine("");
            Console.WriteLine("Введите текст, который нужно сохранить:");
            string textInFile = Console.ReadLine();
            string putkPapke; //путь к папке, в которой будем сохранять файл
            string fileName; //имя файла
            bool putProverka = false; //переменная для проверки корректности пути
            bool fileNameProverka = false; //переменная для проверки корректности имени

            do //выполняем ввод пути по заданному условию проверки
            {
                Console.WriteLine("Введите путь к папке для сохранения файла:");
                putkPapke = Console.ReadLine();

                if (!PutProverka(putkPapke)) //корректность пути (если не соответсвует значению, вводим заново)
                {
                    Console.WriteLine("Некорректный путь к папке. Попробуйте снова.");
                }
                else
                {
                    putProverka = true; //ставим метку, что путь корректен и переходим к созданию папки, если нужно
                }
            } while (!putProverka); //выполняем до тех пор, пока не будет пройдена проверка корректности пути

            //проверяем, существует ли папка по введенному пути
            if (!Directory.Exists(putkPapke))
            {
                try
                {
                    //создаем папку по заданному пути, если её не существует
                    Directory.CreateDirectory(putkPapke);
                    Console.WriteLine("Папка успешно создана!"); 
                }
                catch (Exception ex) //выводим сообщение, если произошла ошибка и возвращаемся к созданию
                {
                    Console.WriteLine($"Ошибка при создании папки: {ex.Message}");
                    return;
                }
            }

            do //выполняем ввод имени файла по заданному условию проверки на ошибке знаков в имени файла
            {
                Console.WriteLine("Введите название файла (без расширения):");
                fileName = Console.ReadLine();

                if (!FileNameProverka(fileName)) //проверяем корректность имени файла, если неверно, то вводим заново
                {
                    Console.WriteLine("Некорректное название файла. Попробуйте снова.");
                }
                else
                {
                    fileNameProverka = true; //ставим метку, если имя файла корректно и переходим к выбору кодировки
                }
            } while (!fileNameProverka); //пока проверка не будет пройдена, выполняем ввод имени файла

            Console.WriteLine("Выберите кодировку (1 - UTF-8, 2 - Win1251, 3 - DOC 866):");
            int encodingChoice = int.Parse(Console.ReadLine());
            Encoding encoding;
            switch (encodingChoice)
            {
                case 1:
                    encoding = Encoding.UTF8;
                    break;
                case 2:
                    encoding = Encoding.GetEncoding("windows-1251"); //получаем нужную кодировку через GetEncoding
                    break;
                case 3:
                    encoding = Encoding.GetEncoding("ibm866"); //получаем нужную кодировку через GetEncoding
                    break;
                default:
                    Console.WriteLine("Неверный выбор кодировки. Используется кодировка UTF-8."); //в случае ошибки
                    encoding = Encoding.UTF8; //стави кодировку "по умолчанию"
                    break;
            }

            string fileNameAndRashirenie = Path.ChangeExtension(fileName, "txt"); //добавляем расширение .txt к имени файла
            string fullPutFile = Path.Combine(putkPapke, fileNameAndRashirenie); //задаем полный путь к созданному файлу
            File.WriteAllText(fullPutFile, textInFile, encoding); //записываем текст в файл с выбранной кодировкой
            Console.WriteLine("Файл успешно сохранен!");
            Console.WriteLine("Для закрытия программы нажмите любую кнопку.");
            Console.ReadLine();
        }
        static bool PutProverka(string put) //осуществляем проверку пути через DirectoryInfo
        {
            try
            {
                _ = new DirectoryInfo(put); //cоздаем объект DirectoryInfo для проверки корректности пути
                return true; //если верное, то возвращаем значение и отправляем его в do-while по вводу пути
            }
            catch (Exception) //если ошибка, возвращаем значение false и в цикле do-while выводим сообщение об ошибке
            {
                return false;
            }
        }
        static bool FileNameProverka(string fileName) //осуществляем проверку пути через GetInvalidFileNameChars
        {
            char[] nevernieSimvoly = Path.GetInvalidFileNameChars(); //получаем неверные символы для проверки имени файла
            return !fileName.Any(nevernieSimvoly.Contains); //проверяем, чтобы имя файла не содержало неверных символов
        }

    }
}
