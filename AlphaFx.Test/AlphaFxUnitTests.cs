using AlphaFx.Domain.Contracts;
using AlphaFx.Domain.Models;
using AlphaFx.Domain.Services;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AlphaFx.Test
{
    public class AlphaFxUnitTests
    {
        #region AlphaFxService
       
        [Fact]
        public void ReadFileToStreamAsync_FileExist_ShouldReturnValidOutput()
        {
            //Given
            var iAlphaFxService = GenerateFakeIAlphaFxService();
            var txtFileModel = CreateTxtFileModel();
            A.CallTo(() => iAlphaFxService.ReadFileToStreamAsync(txtFileModel.TextFile)).Returns(
                txtFileModel.TextFile);

            //When
            var fileVariable = iAlphaFxService.ReadFileToStreamAsync(txtFileModel.TextFile);

            //Then
            Assert.Equal(txtFileModel.TextFile, fileVariable.Result);
        }

        [Fact]
        public void ReadFileToStreamAsync_FileDoesntExist_ShouldReturnNull()
        {
            //Given
            var iAlphaFxService = GenerateFakeIAlphaFxService();
            var txtFileModel = CreateTxtFileModel();
            
            A.CallTo(() => iAlphaFxService.ReadFileToStreamAsync(txtFileModel.TextFile)).Returns(Task.FromResult<string>(null));

            //When
            var fileVariable = iAlphaFxService.ReadFileToStreamAsync(txtFileModel.TextFile);

            //Then
            Assert.Null(fileVariable.Result);
        }

        [Fact]
        public void RemoveSpecialCharacterFromText_FileExist_ShouldReturnValidOutput()
        {
            //Given
            var iAlphaFxService = GenerateFakeIAlphaFxService();
            var txtFileModel = CreateTxtFileModel();

            A.CallTo(() => iAlphaFxService.RemoveSpecialCharacterFromText(txtFileModel.TextFile)).Returns(
                txtFileModel.TextFile);

            //When
            var textWithSpecialCharRemoved = iAlphaFxService.RemoveSpecialCharacterFromText(txtFileModel.TextFile);

            //Then
            Assert.Equal(txtFileModel.TextFile, textWithSpecialCharRemoved);
        }

        [Fact]
        public void RemoveSpecialCharacterFromText_FileDoesntExist_ShouldReturnNull()
        {
            //Given
            var iAlphaFxService = GenerateFakeIAlphaFxService();
            var txtFileModel = CreateTxtFileModel();

            A.CallTo(() => iAlphaFxService.RemoveSpecialCharacterFromText(txtFileModel.TextFile)).Returns(
                null);

            //When
            var textWithSpecialCharRemoved = iAlphaFxService.RemoveSpecialCharacterFromText(txtFileModel.TextFile);

            //Then
            Assert.Null(textWithSpecialCharRemoved);
        }

        [Fact]
        public void CreateListOfWords_FileExist_ShouldReturnValidOutput()
        {
            //Given
            var iAlphaFxService = GenerateFakeIAlphaFxService();
            var txtFileModel = CreateTxtFileModel();
            var stringArrayOfModel = new[] { txtFileModel.TextFile };

            A.CallTo(() => iAlphaFxService.CreateListOfWords(txtFileModel.TextFile)).Returns(stringArrayOfModel);

            //When
            var stringArray = iAlphaFxService.CreateListOfWords(txtFileModel.TextFile);

            //Then
            Assert.Equal(stringArrayOfModel, stringArray);
        }

        [Fact]
        public void CreateListOfWords_FileDoesntExist_ShouldReturnNull()
        {
            //Given
            var iAlphaFxService = GenerateFakeIAlphaFxService();
            var txtFileModel = CreateTxtFileModel();

            A.CallTo(() => iAlphaFxService.CreateListOfWords(txtFileModel.TextFile)).Returns(
                null);

            //When
            var stringArray = iAlphaFxService.CreateListOfWords(txtFileModel.TextFile);

            //Then
            Assert.Null(stringArray);
        }

        [Fact]
        public void CountOccurenceOfWords_FileExist_ShouldReturnValidOutput()
        {
            //Given
            var iAlphaFxService = GenerateFakeIAlphaFxService();
            var txtFileModel = CreateTxtFileModel();
            var stringArrayOfModel = new[] { txtFileModel.TextFile };
            var txtFileModelList = new List<WordCountModel>();
            for (int i = 0; i < stringArrayOfModel.Length; i++)
            {
                txtFileModelList.Add(
                new WordCountModel
                {
                    Count = i,
                    Word = stringArrayOfModel[i]
                });
            }

            A.CallTo(() => iAlphaFxService.CountOccurenceOfWords(stringArrayOfModel)).Returns(txtFileModelList);

            //When
            var txtList = iAlphaFxService.CountOccurenceOfWords(stringArrayOfModel);

            //Then
            Assert.Equal(txtFileModelList, txtList);
            Assert.Equal(txtFileModelList.Count, txtList.Count);
            for (int i = 0; i < txtList.Count; i++)
            {
                Assert.Equal(txtFileModelList[i].Count, txtList[i].Count);
            }
            for (int i = 0; i < txtList.Count; i++)
            {
                Assert.Equal(txtFileModelList[i].Word, txtList[i].Word);
            }
        }

        //If I would sort and check the 2 Lists, then I would be able to check item against item,
        //but It seems a lot of effort to convert the Lists to OrderedDictionary. I am not sure if 
        //there is abetter way to do it. 
        [Fact]
        public void CountOccurenceOfWords_FileExist_ShouldReturnExpectedOutput()
        {
            //Given
            var iAlphaFxService = new AlphaFxService();
            var txtFileModel = CreateTxtFileModel();
            
            var stringwithoutspecialChar = iAlphaFxService.RemoveSpecialCharacterFromText(txtFileModel.TextFile);
            var stringArrayOfModel = iAlphaFxService.CreateListOfWords(stringwithoutspecialChar);
            var txtFileModelList = CreateWordCountModel();

            //When
            var txtList = iAlphaFxService.CountOccurenceOfWords(stringArrayOfModel);
            
            //Then
            Assert.Equal(txtFileModelList.Count, txtList.Count);
        }

        [Fact]
        public void CountOccurenceOfWords_FileDoesntExist_ShouldReturnNull()
        {
            //Given
            var iAlphaFxService = GenerateFakeIAlphaFxService();
            var txtFileModel = CreateTxtFileModel();
            var stringArrayOfModel = new[] { txtFileModel.TextFile };

            A.CallTo(() => iAlphaFxService.CountOccurenceOfWords(stringArrayOfModel)).Returns(null);

            //When
            var txtList = iAlphaFxService.CountOccurenceOfWords(stringArrayOfModel);

            //Then
            Assert.Null(txtList);
        }
        #endregion

        #region TestSetup

        public TxtFileModel CreateTxtFileModel() 
        {
            return new TxtFileModel()
            {
                TextFile = "abcdel4893734$%£$56^54*45&(&£5345$45%435£40-_ \r\n L{-}:@:L@:LL:}  l abcde abcde6456 abcde+~_+{ \n 84684'][.;/,.>< 6846846TEST \r ALPHAFX"
            };
        }

        public List<WordCountModel> CreateWordCountModel()
        {
            return new List<WordCountModel>()
            {
                new WordCountModel
                {
                    Word = "abcde",
                    Count = 3
                }, 
                new WordCountModel
                {
                    Word = "abcdel",
                    Count = 1
                },
                new WordCountModel
                {
                    Word = "test",
                    Count = 1
                },
                new WordCountModel
                {
                    Word = "alphafx",
                    Count = 1
                },
                new WordCountModel
                {
                    Word = "llll",
                    Count = 1
                },
            };

        }

public IAlphaFxService GenerateFakeIAlphaFxService()
        {
            return A.Fake<IAlphaFxService>();
        }

        public string[] StringToStringArray(string stringArray)
        {
            return stringArray.ToLowerInvariant().Split(new char[]
            {' '}, StringSplitOptions.RemoveEmptyEntries);
        }
        
        #endregion
    }
}
