using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CopyDrawingsBetweenModels
{
    public static class Functions
    {
        /// <summary>
        /// Парсит готовый xml в DataSet
        /// </summary>
        public static DataSet ReportDataSet(string reportFile)
        {
            XmlDocument document = new XmlDocument
            {
                PreserveWhitespace = false
            };
            document.Load(reportFile);

            // Получаем данные из отчёта в DataSet
            string xmlString = File.ReadAllText(reportFile, Encoding.GetEncoding(1251));
            XElement element = XElement.Parse(xmlString);
            element.TrimWhiteSpaceFromValues();
            DataSet reportDataSet = new DataSet();
            reportDataSet.ReadXml(element.CreateReader());

            return reportDataSet;
        }

        /// <summary>
        /// Удаляет лишние пробелы из тегов
        /// </summary>
        public static void TrimWhiteSpaceFromValues(this XElement element)
        {
            foreach (var descendent in element.Descendants())
            {
                if (!descendent.HasElements)
                    descendent.SetValue(descendent.Value.Trim());
                else
                    descendent.TrimWhiteSpaceFromValues();
            }
        }
    }
}
