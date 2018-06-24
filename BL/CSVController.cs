using BL.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CSVController : IDisposable
    {
        private FileSystemWatcher _fileWatcher;
        private string _targetDirectory;
        private IRepositoryTransfer _repositoryTransfer;

        public CSVController(string folder)
        {
            _repositoryTransfer = new RepositoryTransfer();
            _fileWatcher = new FileSystemWatcher(folder, "*.csv");
            _fileWatcher.Created += OnCreated;
            _targetDirectory = string.Concat(folder, @"\Done");

            try
            {
                DirectoryInfo targetDirectory = Directory.CreateDirectory(_targetDirectory);
            }
            catch
            {
                Console.WriteLine("It can not be created 'Done' folder!.\nProgram stop!");
                System.Threading.Thread.CurrentThread.Abort();
            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Is new file: {0}", e.FullPath);


            var taskCSTToDB = new Task(() =>
            {
                var sales = Parse(e.FullPath);
                foreach (SaleDTO sale in sales)
                {
                    _repositoryTransfer.AddSaleInfo(sale);
                }
                return;
            }
                                      );
            taskCSTToDB.Start();

            var taskMoveFile = taskCSTToDB.ContinueWith((r) =>
            {
                try
                {
                    string targetFile = string.Concat(_targetDirectory, @"\", e.Name);
                    if (File.Exists(targetFile))
                    {
                        File.Delete(targetFile);
                    }
                    File.Move(e.FullPath, targetFile);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Can't move file, because: {0}", exc.ToString());
                }
                finally
                {
                    Console.WriteLine("{0} moved.", e.Name);
                }
            }
                                                       );
        }

        private ICollection<SaleDTO> Parse(string fullPath)
        {
            ICollection<SaleDTO> sales = new List<SaleDTO>();

            string fileName = fullPath.Substring((fullPath.LastIndexOf(@"\") + 1));
            string manager = fileName.Substring(0, fileName.LastIndexOf("_"));

            using (TextReader reader = File.OpenText(fullPath))
            {
                while (reader.Peek() > -1)
                {
                    string readedLine = reader.ReadLine();

                    var splitSaleInfo = readedLine.Split(',');

                    try
                    {
                        SaleDTO s = new SaleDTO() { Date = DateTime.Parse(splitSaleInfo[0]), Manager = manager, Client = splitSaleInfo[1], Product = splitSaleInfo[2], Amount = double.Parse(splitSaleInfo[3]) };
                        sales.Add(s);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Can't parse, because: {0}", exception.ToString());
                    }
                }
            }
            return sales;
        }

        public void Run()
        {
            Console.WriteLine("Start watching folder.");

            _fileWatcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            Console.WriteLine("Watching stopped.");

            _fileWatcher.EnableRaisingEvents = false;
        }

        public void Dispose()
        {
            if (_fileWatcher != null)
            {
                Stop();
                _fileWatcher.Dispose();
                _fileWatcher = null;
            }
        }

        ~CSVController()
        {
            Dispose();
        }

        public void GetAllTables()
        {
            Console.WriteLine("Sales:");
            foreach (var sale in _repositoryTransfer.GetSales())
            {
                Console.WriteLine("{0}, | {1} | {2} | {3} | {4}", sale.Date, sale.Manager, sale.Client, sale.Product, sale.Amount);
            }

            Console.WriteLine("\nManagers:");
            foreach (var manager in _repositoryTransfer.GetManagers())
            {
                Console.WriteLine(manager.SecondName);
            }

            Console.WriteLine("\nClients:");
            foreach (var client in _repositoryTransfer.GetClients())
            {
                Console.WriteLine(client.FullName);
            }

            Console.WriteLine("\nProducts:");
            foreach (var product in _repositoryTransfer.GetProducts())
            {
                Console.WriteLine(product.Name);
            }
        }
    }
}
