using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CSVFileParser.Models;

namespace CSVFileParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var introTextBuilder = new StringBuilder();
            introTextBuilder.Append("This console application takes in an absolute path to a .csv file");
            introTextBuilder.Append("\ncontaining a collection of Enrollment Entry records.");
            introTextBuilder.Append("\nThe Enrollment Entry records must be in the form of");
            introTextBuilder.Append("\nuserId, firstName lastName, version, insuranceCompany");
            introTextBuilder.Append("\nExample:\t1001,Alice Smith,1,BCBSF");
            introTextBuilder.Append("\nNote: The field for the user's full name takes in a single value and splits it");
            introTextBuilder.Append("\ninto the first and last name.");

            introTextBuilder.Append("\n\nAfter the .csv file is loaded, it will be parsed and split into one file");
            introTextBuilder.Append("\nper insurance company into a directory named \"output\" within the same directory");
            introTextBuilder.Append("\nthe original .csv file was loaded from.");
            
            Console.WriteLine(introTextBuilder.ToString());

            const string directionText = "\n\nPlease enter the absolute path to the .csv file of Enrollment Entry records...";
            
            Console.WriteLine(directionText);
            var path = Console.ReadLine();

            var fileFound = false;
            while (!fileFound)
            {
                if (!string.IsNullOrEmpty(path) && path.EndsWith(".csv"))
                {
                    fileFound = File.Exists(path);

                    if (fileFound)
                    {
                        continue;
                    }
                }
                
                Console.WriteLine($"\nError: unable to find .csv file at path \"{path}\"");
                Console.WriteLine("Please try again...");
                Console.WriteLine(directionText);
                path = Console.ReadLine();
            }

            var dirPath = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
            
            var data = ReadFromFile(path).ToList();
            Console.WriteLine("Read {0} records from file at \"{1}\"", data.Count, path);

            var sortedData = SortingLogic.SortDataByCompany(data);
        
            WriteToFile(sortedData,  dirPath + Path.DirectorySeparatorChar + "output" + Path.DirectorySeparatorChar);
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
            Console.WriteLine("\nGoodbye!");
        }

        public static IEnumerable<EnrollmentEntry> ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: File not found at path {0}", filePath);    
            }
            
            var enrollmentEntries = new List<EnrollmentEntry>();
            
            using (var streamReader = new StreamReader(filePath))
            {
                var line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    var lineValues = line.Split(',');
                    if (lineValues.Length != 4)
                    {
                        continue;
                    }
                    
                    enrollmentEntries.Add(new EnrollmentEntry
                    {
                        UserId = lineValues[0],
                        FullName = new UserName(lineValues[1]),
                        Version = int.Parse(lineValues[2]),
                        InsuranceCompany = lineValues[3]
                    });
                    
                    // read in next line
                    line = streamReader.ReadLine();
                }
            }

            return enrollmentEntries;
        }

        public static void WriteToFile(IDictionary<string, IDictionary<string, EnrollmentEntry>> data, string outputDirPath)
        {
            foreach (var companyName in data.Keys)
            {
                var companyEntries = data[companyName];
                var enrollmentEntries = SortingLogic.SortEntriesByName(companyEntries);

                var dirExists = Directory.Exists(outputDirPath);
                if (!dirExists)
                {
                    Directory.CreateDirectory(outputDirPath);
                }

                using (var writer = new StreamWriter(outputDirPath + companyName + "_" + DateTime.Now.Date.ToString("yyyy-MMMM-dd") + ".csv"))
                {
                    foreach (var entry in enrollmentEntries)
                    {
                        writer.WriteLine(entry.ToString());
                        writer.Flush();
                    }
                }
            }
        }
    }
}