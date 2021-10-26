using System.Collections.Generic;
using System.Linq;
using CSVFileParser.Models;

namespace CSVFileParser
{
    public static class SortingLogic
    {
        public static IList<EnrollmentEntry> SortEntriesByName(IDictionary<string, EnrollmentEntry> entries)
        {
            var sortedEntries = entries.Values.ToList();
            sortedEntries = sortedEntries.OrderBy(x => x.FullName.Last).ThenBy(x => x.FullName.First).ToList();
            
            return sortedEntries;
        }
        
        public static IDictionary<string, IDictionary<string, EnrollmentEntry>> SortDataByCompany (IEnumerable<EnrollmentEntry> entryData)
        {
            var entries = entryData.ToList();
            var companies = entries.Select(x => x.InsuranceCompany).Distinct().ToArray();

            var companiesDictionary = new Dictionary<string, IDictionary<string, EnrollmentEntry>>(companies.Length);
            foreach (var company in companies)
            {
                var companyEntries = entries.Where(x => string.Equals(x.InsuranceCompany, company)).ToList();
                var entriesDictionary = new Dictionary<string, EnrollmentEntry>(companyEntries.Count);

                foreach (var entry in companyEntries)
                {
                    // Check if user already exists
                    if (!entriesDictionary.ContainsKey(entry.UserId))
                    {
                        entriesDictionary.Add(entry.UserId, entry);
                    }
                    else
                    {
                        // Replace user if new version is higher
                        if (entriesDictionary[entry.UserId].Version < entry.Version)
                        {
                            entriesDictionary[entry.UserId] = entry;
                        }
                    }
                }
                
                entriesDictionary.TrimExcess();
                companiesDictionary.Add(company, entriesDictionary);
            }

            return companiesDictionary;
        }
    }
}