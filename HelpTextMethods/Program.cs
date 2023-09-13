using ChatbotMyAxelos.DAL;
using ChatbotMyAxelos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pinecone;
using System;
using System.Reflection.Metadata;

namespace HelperMethods
{
    internal class Program
    {
        public static void Main()
        {
            string filePath = @"C:\Users\eva.avramidou\Downloads\music-help.pdf";
            string path = @"C:\Users\eva.avramidou\Desktop\querypath.json";
            string path2 = @"C:\Users\eva.avramidou\Desktop\result.txt";
            var pineconeClient = new PineconeProvider("f909df8a-53aa-49b7-a25b-f428a9528215", "us-east4-gcp");
            var indexName = "chatbot1-index";
            var projectName = "56a1186";

            var texts = TextMethods.PdfReaderAndSplitter(filePath);
            var upsertRequest = TextMethods.FillVectorsToRequest(texts);
            var result = pineconeClient.Upsert(indexName, projectName, upsertRequest).Result;

            //var result2 = pineconeClient.DescribeIndex(indexName).Result;
            
            Console.WriteLine();
        }
    }
}
