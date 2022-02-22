using System.Collections.Generic;

namespace Employees.UnitTests.Validators.v1.Employee
{
    public class EmployeeTestDataGenerator
    {
        public static IEnumerable<object[]> ValidPageAndLimit()
        {
            yield return new object[] { 1, 10 };
            yield return new object[] { 2, 2 };
        }

        public static IEnumerable<object[]> InvalidPageAndLimit()
        {
            yield return new object[] { -1, -1 };
            yield return new object[] { 0, 0 };
        }

        public static IEnumerable<object[]> ValidFirstNameAndLastName()
        {
            yield return new object[] { "aa", "bb" };
            yield return new object[] { "John", "Doe" };
            yield return new object[] { "Jane", "Doe" };
        }

        public static IEnumerable<object[]> InvalidFirstNameAndLastName()
        {
            yield return new object[] { "a", "b" };
            yield return new object[] { null, null };
            yield return new object[] { string.Empty, string.Empty };
        }

        public static IEnumerable<object[]> ValidGenders()
        {
            yield return new object[] { 'm' };
            yield return new object[] { 'M' };
            yield return new object[] { 'f' };
            yield return new object[] { 'F' };
            yield return new object[] { 'o' };
            yield return new object[] { 'O' };
        }

        public static IEnumerable<object[]> InvalidGenders()
        {
            yield return new object[] { '\0' };
            yield return new object[] { 'a' };
            yield return new object[] { 'A' };
        }

        public static IEnumerable<object[]> ValidSalaries()
        {
            yield return new object[] { 81879.39M };
            yield return new object[] { 64616.22M };
            yield return new object[] { 01338.90M };
            yield return new object[] { 75202.66M };
            yield return new object[] { 21239.97M };
            yield return new object[] { 45276.57M };
            yield return new object[] { 56914.24M };
            yield return new object[] { 81155.20M };
            yield return new object[] { 72201.42M };
            yield return new object[] { 61192.38M };
        }

        public static IEnumerable<object[]> InvalidSalaries()
        {
            yield return new object[] { 0.0M };
            yield return new object[] { -0.11M };
            yield return new object[] { -0.111M };
            yield return new object[] { -0.1111M };
        }


        public static IEnumerable<object[]> ValidManagerId()
        {
            yield return new object[] { 1 };
            yield return new object[] { 2 };
            yield return new object[] { default(int?) };
        }

        public static IEnumerable<object[]> InvalidManagerId()
        {
            yield return new object[] { 0 };
            yield return new object[] { -1 };
        }


        public static IEnumerable<object[]> ValidDepartmentId()
        {
            yield return new object[] { 1 };
            yield return new object[] { 2 };
        }

        public static IEnumerable<object[]> InvalidDepartmentId()
        {
            yield return new object[] { 0 };
            yield return new object[] { -1 };
        }
    }
}
