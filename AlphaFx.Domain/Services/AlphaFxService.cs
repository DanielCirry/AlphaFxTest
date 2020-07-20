using AlphaFx.Domain.Contracts;
using AlphaFx.Domain.Helpers;
using AlphaFx.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AlphaFx.Domain.Services
{
    public class AlphaFxService : IAlphaFxService
    {
        //ReadToEndAsync() does not finish the job and just exit the application when the GetWordCount() method is async
        public async Task<string> ReadFileToStreamAsync(string textFile)
        {
            if (textFile != null)
            {
                try
                {
                    var fileToStream = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\{textFile}.txt";
                    using var fileStream = new FileStream(fileToStream, FileMode.Open, FileAccess.Read);
                    using var streamReader = new StreamReader(fileStream, Encoding.UTF8);

                    return await streamReader.ReadToEndAsync();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return null;
        }

        public string RemoveSpecialCharacterFromText(string textFile)
        {
            if (textFile != null)
            {
                return textFile.ReplaceUnwantedCharacters();
            }

            return null;
        }

        public string[] CreateListOfWords(string textFile)
        {
            return  textFile.ToLowerInvariant().Split(new char[]
            {' '}, StringSplitOptions.RemoveEmptyEntries);

        }

        public List<WordCountModel> CountOccurenceOfWords(string[] words)
        {
            if (words != null)
            {
                SortedDictionary<string, int> stats = new SortedDictionary<string, int>();


                foreach (var word in words)
                {
                    if (word.Length > 1)
                    {
                        if (!stats.ContainsKey(word))
                        {
                            stats.Add(word, 1);
                        }
                        else
                        {
                            stats[word] += 1;
                        }
                    }
                }

                var wordCount = new List<WordCountModel> { };
                foreach (var pair in stats.OrderByDescending(x => x.Value).Take(10).Distinct())
                {
                    wordCount.Add(new WordCountModel
                    {
                        Count = pair.Value,
                        Word = pair.Key,
                    });
                }
                return wordCount;
            }

            return null;
        }
    }
}
