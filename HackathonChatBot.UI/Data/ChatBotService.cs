using HackathonChatBotML.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HackathonChatBot.UI.Data
{
    public class ChatBotService
    {
        private const string pathToCsvFile = "CategoryAnswersMap.csv";
        private readonly Dictionary<string, string> CategoryAnswersMap;
        private string PreviousCategory;

        public ChatBotService()
        {
            CategoryAnswersMap = File.ReadLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathToCsvFile))
                .Select(line => line.Split(';')).ToDictionary(line => line[0], line => line[1]);
        }

        public Task<string> GetAnswerAsync(string question)
        {
            ModelInput sampleData = new() { Question = question };

            return Task.Factory.StartNew(() =>
            {
                // maybe add Task.Delay(2000) to emulate human's typing
                var predictionResult = ConsumeModel.Predict(sampleData);
                Console.WriteLine($"\n\nPredicted Category value {predictionResult.Prediction} \nPredicted Category scores: [{string.Join(",", predictionResult.Score)}]\n\n");

                if (predictionResult.Score.FirstOrDefault() < 0.1 && PreviousCategory != "Undefined") 
                {
                    // return general fallback
                    PreviousCategory = "Undefined";
                    return "Can you rephraze your question?";
                }

                if (PreviousCategory == predictionResult.Prediction) 
                { 
                    // return Fallback or something else
                }

                PreviousCategory = predictionResult.Prediction;

                return CategoryAnswersMap[predictionResult.Prediction];
            });
        }
    }
}
