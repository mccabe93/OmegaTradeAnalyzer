using CsvHelper;
using System.Globalization;

namespace OmegaPostProcessor
{
    public class FileIO
    {
        public static List<T> ReadCsvFile<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<T>().ToList();
            }
        }
        public static void WriteCsvFile<T>(List<T> data, string filePath, List<string> propertiesToInclude)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(string.Join(',', propertiesToInclude));
                // Write each object to the CSV file
                foreach (var item in data)
                {
                    var type = typeof(T);
                    var propertyValues = propertiesToInclude
                        .Select(propertyName => type.GetProperty(propertyName)?.GetValue(item, null))
                        .ToArray();
                    string asString = string.Join(',', propertyValues.Select(property => property.ToString()));
                    writer.WriteLine(asString);
                }
            }
        }
    }
}
