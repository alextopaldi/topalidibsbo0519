using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.IO.Compression;


namespace topalidi
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Person1
    {
        [JsonPropertyName("firstname")]
        public string Name { get; set; }
        [JsonIgnore] public int Age { get; set; }

    }
    class Phone
    {
        public string Name { get; set; }
        public string Price { get; set; }
    }
    class Program
    {

        static async Task Main(string[] args)
        {
            
            
            int num = Convert.ToInt32(Console.ReadLine());
            switch (num)
            {
                case 1:
                    {
                        DriveInfo[] drives = DriveInfo.GetDrives();
                        foreach (DriveInfo drive in drives)
                        {
                            Console.WriteLine($"Название: {drive.Name}");
                            Console.WriteLine($"Тип: {drive.DriveType}");
                            if (drive.IsReady)
                            {
                                Console.WriteLine($"Объем диска: {drive.TotalSize}");
                                Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                                Console.WriteLine($"Метка: {drive.VolumeLabel}");
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine("Чтобы перейти ко второму заданию введите 2");
                        if (Console.ReadLine() == "2")
                            goto case 2;
                        break;
                    }
                case 2:
                    {

                        string path1 = @"D:\alext.txt";
                        FileInfo fileInf = new FileInfo(path1);
                        {
                            if (!fileInf.Exists)
                            {
                                fileInf.Create();
                            }
                        }
                        using (FileStream fstream = File.Create(path1))
                        {
                            string text = "example";
                            byte[] array = System.Text.Encoding.Default.GetBytes(text);
                            fstream.Write(array, 0, array.Length);
                            Console.WriteLine("Текст записан в файл");
                        }
                        using (FileStream fstream = File.OpenRead(path1))
                        {
                            byte[] array = new byte[fstream.Length];
                            fstream.Read(array, 0, array.Length);
                            string textFromFile = System.Text.Encoding.Default.GetString(array);
                            Console.WriteLine($"Текст из файла: {textFromFile}");
                        }
                        Console.WriteLine("Чтобы удалить файл нажмите 1");
                        if (Console.ReadLine() == "1")
                        {
                            FileInfo fileInf1 = new FileInfo(path1);
                            if (fileInf1.Exists)
                            {
                                fileInf.Delete();
                            }
                            Console.WriteLine("Файл удален");
                        }
                        Console.WriteLine("Чтобы перейти ко третьему заданию введите 3");
                        if (Console.ReadLine() == "3")
                            goto case 3;
                        break;
                    }
                case 3:
                    {
                        Person tom = new Person();
                        tom.Name = "Tom";
                        tom.Age = 12;
                        string json = JsonSerializer.Serialize<Person>(tom);
                        Console.WriteLine(json);
                        Person restoredPerson = JsonSerializer.Deserialize<Person>(json);
                        Console.WriteLine(restoredPerson.Name);



                        {
                            // сохранениеданных 
                            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
                            {
                                Person tom1 = new Person()
                                { Name = "Tom", Age = 35 };
                                await JsonSerializer.SerializeAsync<Person>(fs, tom1);
                                Console.WriteLine("Data has been saved to file");
                            }
                            // чтениеданных 
                            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
                            {
                                Person restoredPerson1 = await JsonSerializer.DeserializeAsync<Person>(fs);
                                Console.WriteLine($"Name: {restoredPerson1.Name} Age: {restoredPerson1.Age}");
                            }
                        }

                        {
                            Person1 tom1 = new Person1()
                            { Name = "Tom", Age = 35 };
                            string json1 = JsonSerializer.Serialize<Person1>(tom1);
                            Console.WriteLine(json1);
                            Person1 restoredPerson1 = JsonSerializer.Deserialize<Person1>(json1);
                            Console.WriteLine($"Name: {restoredPerson1.Name} Age: {restoredPerson1.Age}");
                        }
                        Console.WriteLine("Чтобы перейти ко четвертому заданию введите 4");
                        if (Console.ReadLine() == "4")
                            goto case 4;
                        break;

                    }
                case 4:
                    {
                        XDocument xdoc = new XDocument();
                        // создаем первый элемент
                        XElement iphone6 = new XElement("phone");
                        // создаематрибут
                        XAttribute iphoneNameAttr = new XAttribute("name", "iPhone 6");
                        XElement iphoneCompanyElem = new XElement("company", "Apple");
                        XElement iphonePriceElem = new XElement("price", "40000");
                        // добавляем атрибут и элементы в первый элемент
                        iphone6.Add(iphoneNameAttr);
                        iphone6.Add(iphoneCompanyElem);
                        iphone6.Add(iphonePriceElem);

                        // создаемвторойэлемент
                        XElement galaxys5 = new XElement("phone");
                        XAttribute galaxysNameAttr = new XAttribute("name", "Samsung Galaxy S5");
                        XElement galaxysCompanyElem = new XElement("company", "Samsung");
                        XElement galaxysPriceElem = new XElement("price", "33000");
                        galaxys5.Add(galaxysNameAttr);
                        galaxys5.Add(galaxysCompanyElem);
                        galaxys5.Add(galaxysPriceElem);
                        // создаемкорневойэлемент
                        XElement phones = new XElement("phones");
                        // добавляем в корневой элемент
                        phones.Add(iphone6);
                        phones.Add(galaxys5);
                        // добавляем корневой элемент в документ
                        xdoc.Add(phones);
                        //сохраняем документ
                        xdoc.Save("phones.xml");
                        XDocument xdoc1 = XDocument.Load("phones.xml");
                        foreach (XElement phoneElement in xdoc1.Element("phones").Elements("phone"))
                        {
                            XAttribute nameAttribute = phoneElement.Attribute("name");
                            XElement companyElement = phoneElement.Element("company");
                            XElement priceElement = phoneElement.Element("price");

                            if (nameAttribute != null && companyElement != null && priceElement != null)
                            {
                                Console.WriteLine($"Смартфон: {nameAttribute.Value}");
                                Console.WriteLine($"Компания: {companyElement.Value}");
                                Console.WriteLine($"Цена: {priceElement.Value}");
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine("Чтобы перейти ко пятому заданию введите 5");
                        if (Console.ReadLine() == "5")
                            goto case 5;

                        break;
                    }
                case 5:
                    {
                        string sourceFile = "D://test/book.pdf";
                        string compressedFile = "D://test/book.gz";
                        string targetFile = "D://test/book_new.pdf";

                        Compress(sourceFile, compressedFile);

                        Decompress(compressedFile, targetFile);

                        static void Compress(string sourceFile, string compressedFile)
                        {

                            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
                            {

                                using (FileStream targetStream = File.Create(compressedFile))
                                {

                                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                                    {
                                        sourceStream.CopyTo(compressionStream);
                                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1} сжатый размер: {2}.",
                                        sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                                    }
                                }
                            }
                        }
                        static void Decompress(string compressedFile, string targetFile)
                        {

                            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
                            {

                                using (FileStream targetStream = File.Create(targetFile))
                                {

                                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                                    {
                                        decompressionStream.CopyTo(targetStream);
                                        Console.WriteLine("Восстановлен файл: {0}", targetFile);
                                    }
                                }
                            }
                        }
                        break;
                    }

            }
        }
    }
}