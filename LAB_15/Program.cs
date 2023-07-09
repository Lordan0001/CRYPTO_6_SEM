using System;
using Aspose.Words;
using System.Collections.Generic;
using System.Text;

namespace SteganographyExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "in.docx";
            string outputFilePath = "out.docx";
            Console.WriteLine("Введитке сообщение для шифрования: ");
            string? message = Console.ReadLine();

            Document document = new Document(inputFilePath);

            
            NodeCollection paragraphs = document.GetChildNodes(NodeType.Paragraph, true);

            double baseSpacing = 10; 
            double minSpacing = -10;
            double maxSpacing = 10; 
            double deltaSpacing = Math.Abs(maxSpacing - minSpacing); 

            string binaryMessage = StringToBinary(message);

            // Проверяем, достаточно ли параграфов 
            if (paragraphs.Count < binaryMessage.Length)
            {
                Console.WriteLine("Недостаточно параграфов для размещения сообщения.");
                return;
            }

            // Проходим по каждому символу
            for (int i = 0; i < binaryMessage.Length; i++)
            {
                // Получаем текущий параграф
                Paragraph paragraph = (Paragraph)paragraphs[i];

                // Получаем текущий символ
                char currentChar = binaryMessage[i];

                // Вычисляем значение апроша на основе текущего символа
                double spacing = baseSpacing;
                if (currentChar == '1')
                {
                    spacing += deltaSpacing;
                }

                // Устанавливаем значение апроша для текущего параграфа
                paragraph.ParagraphFormat.SpaceAfter = spacing;
            }

            // Сохраняем изменения в новый документ
            document.Save(outputFilePath);

            Console.WriteLine("Сообщение успешно скрыто в документе.");

            // Чтение зашифрованной информации из документа "out.docx"
            Document encryptedDocument = new Document(outputFilePath);
            StringBuilder decryptedMessageBuilder = new StringBuilder();

            foreach (Paragraph paragraph in encryptedDocument.GetChildNodes(NodeType.Paragraph, true))
            {
                // Получаем значение апроша параграфа
                double spacing = paragraph.ParagraphFormat.SpaceAfter;

                // Определяем, было ли в данном параграфе скрыто сообщение
                char decryptedChar = spacing > baseSpacing ? '1' : '0';

                // Добавляем расшифрованный символ к сообщению
                decryptedMessageBuilder.Append(decryptedChar);
            }

            // Получаем расшифрованное сообщение
            string decryptedMessage = BinaryToString(decryptedMessageBuilder.ToString());

            Console.WriteLine("Расшифрованное сообщение: " + decryptedMessage);
        }

        // Преобразование строки в двоичное представление
        public static string StringToBinary(string data)
        {
            StringBuilder binaryBuilder = new StringBuilder();
            foreach (char c in data)
            {
                string binaryChar = Convert.ToString(c, 2).PadLeft(8, '0');
                binaryBuilder.Append(binaryChar);
            }
            return binaryBuilder.ToString();
        }

        // Преобразование двоичной строки в текст
        public static string BinaryToString(string data)
        {
            List<byte> byteList = new List<byte>();
            for (int i = 0; i < data.Length; i += 8)
            {
                string binaryByte = data.Substring(i, 8);
                byte byteValue = Convert.ToByte(binaryByte, 2);
                byteList.Add(byteValue);
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
    }
}
