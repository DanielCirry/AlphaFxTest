using AlphaFx.Domain.Contracts;
using AlphaFx.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaFx.Views
{
    public class AlphaFxApplication
    {
        private readonly IConfigurationRoot _config;
        private readonly IAlphaFxService _service;

        public AlphaFxApplication(IConfigurationRoot config, IAlphaFxService service)
        {
            _config = config;
            _service = service;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    //Not able to await GetWordCount(), the result comes out empty if I do.
                    //The streamReader seems unable to finish the process, 
                    //probably is because the file is large,
                    //.Result works but is not the right solution.
                    var wordCount = GetWordCount();
                    if (wordCount.Result != null)
                    {
                        foreach (var item in wordCount.Result)
                        {
                            Console.WriteLine($"The word \"{item.Word}\" was seen {item.Count} in the file.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There was an error: " + ex.Message);
                }

                Environment.Exit(0);
            }
        }

        public async Task<List<WordCountModel>> GetWordCount()
        {
            var txtFileModel = _config.GetSection("fileName").Value.ToString();

            if (txtFileModel != null)
            {
                try
                {
                    var fileToVariable = await _service.ReadFileToStreamAsync(txtFileModel);

                    if (fileToVariable != null)
                    {
                        var cleanedTxtFile = _service.RemoveSpecialCharacterFromText(fileToVariable);

                        if (cleanedTxtFile != null)
                        {
                            var words = _service.CreateListOfWords(cleanedTxtFile);

                            if (words != null)
                                return _service.CountOccurenceOfWords(words);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There was an error: " + ex.Message);
                }
            }
            return null;
        }
    }
}