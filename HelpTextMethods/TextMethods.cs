using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Linq;
using pinecone;
using pinecone.Models;

namespace HelperMethods
{
    public static class TextMethods
    {
        public static List<string> PdfReaderAndSplitter(string path)
        {
            // Create a reader for the PDF file
            PdfReader reader = new PdfReader(path);
            string textall = string.Empty;
            // Extract the text from each page of the PDF file
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                textall += PdfTextExtractor.GetTextFromPage(reader, page);
            }

            // Close the reader
            reader.Close();

            List<string> texts = new List<string>();
            int start = 0;
            for (int i = 0; i < textall.Length; i++)
            {
                if (i > 0 && i % 2000 == 0)
                {
                    texts.Add(textall.Substring(start, 2000));
                    start = 2000 + start;
                }
                if (textall.Length - start < 2000)
                {
                    texts.Add(textall.Substring(start, textall.Length - start));
                    break;
                }
            }
            return texts;
        }

        public static async Task<Root> WordEmbeddings(string text)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("authorization", "Bearer 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");
                client.Timeout = TimeSpan.FromMinutes(3);

                var content = new StringContent("{\"model\": \"text-embedding-ada-002\", \"input\": \"" + text + "\"}",
                    Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("https://api.openai.com/v1/embeddings", content).Result;

                string responseString = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine(responseString);
                var responseObject = JsonSerializer.Deserialize<Root>(responseString); //(Root)JsonConvert.DeserializeObject(responseString);

                return responseObject;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return null;

        }

        public static List<double> TextToWordEmbeddings(string text)
        {
            var textWithoutBlankSpaces = text.Replace("\n", "\\n").Replace("\u00AE", "\\u00AE").Replace("\0", "\\0");
            var response = WordEmbeddings(textWithoutBlankSpaces).Result;
            var listToBeFilled = new List<double>();
            foreach (var embd in response.data.FirstOrDefault().embedding)
            {
                listToBeFilled.Add(embd);

            }
            return listToBeFilled;
        }

        public static UpsertRequest FillVectorsToRequest(List<string> texts)
        {
            var upsertRequest = new UpsertRequest();
            upsertRequest.Vectors = new List<pinecone.Models.Vector>();
            for (int i = 0; i < texts.Count; i++)
            {

                var vector = new pinecone.Models.Vector();
                vector.Values = TextToWordEmbeddings(texts[i]);

                vector.Id = (i + 32).ToString();
                
                vector.Metadata = new Dictionary<string, string>();
                vector.Metadata.Add(vector.Id, texts[i]);

                upsertRequest.Vectors.Add(vector);
                Console.WriteLine(i);

            }
            return upsertRequest;
        }
    }
}
