using System;
using System.Collections.Generic;
using System.Linq;
using CSVFileParser.Models;
using Xunit;

namespace CSVFileParser.Tests
{
    public class SortingLogicTests
    {
        // Reused test data
        const string Company1 = "PillCo";
        const string Company2 = "Green Cross Green Shield";
        
        [Fact]
        public void SortDataByCompanyShouldSplitByCompany()
        {
            var testData = new List<EnrollmentEntry>
            {
                new EnrollmentEntry
                {
                    UserId = Guid.NewGuid().ToString(),
                    FullName = new UserName("Bob Springfield"),
                    Version = 1,
                    InsuranceCompany = Company1
                },
                new EnrollmentEntry
                {
                    UserId = Guid.NewGuid().ToString(),
                    FullName = new UserName("Alice Pensacola"),
                    Version = 1,
                    InsuranceCompany = Company2
                },
                new EnrollmentEntry
                {
                    UserId = Guid.NewGuid().ToString(),
                    FullName = new UserName("Carol Tampa"),
                    Version = 1,
                    InsuranceCompany = Company1
                }
            };

            var sortedData = SortingLogic.SortDataByCompany(testData);
            
            Assert.Equal(2, sortedData.Keys.Count);
            Assert.Equal(2, sortedData[Company1].Values.Count);
            Assert.Equal(1, sortedData[Company2].Values.Count);
        }
        
        [Fact]
        public void SortDataByCompanyShouldTakeLatestVersion()
        {
            var testData = new List<EnrollmentEntry>
            {
                new EnrollmentEntry
                {
                    UserId = "312",
                    FullName = new UserName("Bob Springfield"),
                    Version = 2,
                    InsuranceCompany = Company1
                },
                new EnrollmentEntry
                {
                    UserId = "312",
                    FullName = new UserName("Bob Springfield"),
                    Version = 1,
                    InsuranceCompany = Company1
                },
                new EnrollmentEntry
                {
                    UserId = "841",
                    FullName = new UserName("Alice Pensacola"),
                    Version = 1,
                    InsuranceCompany = Company1
                },
                new EnrollmentEntry
                {
                    UserId = "743",
                    FullName = new UserName("Carol Tampa"),
                    Version = 1,
                    InsuranceCompany = Company1
                }
            };

            var sortedData = SortingLogic.SortDataByCompany(testData);
            
            Assert.Equal(1, sortedData.Keys.Count);
            Assert.Equal(3, sortedData[Company1].Values.Count);
        }
        
        [Fact]
        public void SortByNameShouldReturnSortedList()
        {
            var testData = new Dictionary<string, EnrollmentEntry>(3)
            {
                {
                    "712", new EnrollmentEntry
                    {
                        UserId = "712",
                        FullName = new UserName("David Springfield"),
                        Version = 1,
                        InsuranceCompany = Company1
                    }
                },
                {
                    "312", new EnrollmentEntry
                    {
                        UserId = "312",
                        FullName = new UserName("Bob Springfield"),
                        Version = 1,
                        InsuranceCompany = Company1
                    }
                },
                {
                    "841", new EnrollmentEntry
                    {
                        UserId = "841",
                        FullName = new UserName("Eve Pensacola"),
                        Version = 1,
                        InsuranceCompany = Company1
                    }
                },
                { "743", new EnrollmentEntry
                    {
                        UserId = "743",
                        FullName = new UserName("Carol Tampa"),
                        Version = 1,
                        InsuranceCompany = Company1
                    } 
                }
            };

            var sortedData = SortingLogic.SortEntriesByName(testData).ToList();
            
            Assert.Equal(testData.Count, sortedData.Count);
            // Verify that entries are sorted by last name, then first name
            Assert.True(sortedData.FindIndex(x => string.Equals(x.FullName.First, "Eve")) 
                        < sortedData.FindIndex(y => string.Equals(y.FullName.First, "Bob")));
            // Verify that entries with same last name are then sorted by first name
            Assert.True(sortedData.FindIndex(x => string.Equals(x.FullName.First, "Bob")) 
                        < sortedData.FindIndex(y => string.Equals(y.FullName.First, "David")));
        }
    }
}