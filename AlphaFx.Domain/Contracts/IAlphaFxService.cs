using AlphaFx.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaFx.Domain.Contracts
{
    public interface IAlphaFxService
    {
        Task<string> ReadFileToStreamAsync(string textFile);
        string RemoveSpecialCharacterFromText(string textFile);
        string[] CreateListOfWords(string textFile);
        List<WordCountModel> CountOccurenceOfWords(string[] words);
    }
}
