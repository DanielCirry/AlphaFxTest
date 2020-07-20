using System.Linq;

namespace AlphaFx.Domain.Helpers
{
    public static class  StringManipulation
    {
        public static string ReplaceUnwantedCharacters(this string textFile)
        {
            //Ithink there is a way to use Environment.NewLine, 
            //but probably I needed to change this solution to use it
            char[] separators = new char[] { '\r', '\n', };

            var newString = new string(textFile.Where(c => (
                char.IsLetter(c) 
                || char.IsWhiteSpace(c) 
                || char.IsSeparator(c)) 
                && !char.IsDigit(c))
                .ToArray());
            //Aggregate applies a function to each item in the collection
            return separators.Aggregate(newString, (str, cItem) => str.Replace(cItem, ' '));
        }
    }
}
