// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.BoyerMooreBinarySearch
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace AudibleConvertor
{
  public class BoyerMooreBinarySearch
  {
    private readonly long[] _badCharacterShift;
    private readonly long[] _goodSuffixShift;
    private readonly long[] _suffixes;
    private readonly byte[] _searchPattern;

    public BoyerMooreBinarySearch(byte[] searchPattern)
    {
      if (searchPattern == null || !((IEnumerable<byte>) searchPattern).Any<byte>())
        throw new ArgumentNullException(nameof (searchPattern));
      this._searchPattern = searchPattern;
      this._badCharacterShift = this.BuildBadCharacterShift(searchPattern);
      this._suffixes = this.FindSuffixes(searchPattern);
      this._goodSuffixShift = this.BuildGoodSuffixShift(searchPattern, this._suffixes);
    }

    public ReadOnlyCollection<long> GetMatchIndexes(byte[] dataToSearch)
    {
      return new ReadOnlyCollection<long>((IList<long>) this.GetMatchIndexes_Internal(dataToSearch));
    }

    public ReadOnlyCollection<long> GetMatchIndexes(FileInfo fileToSearch, int bufferSize = 1048576)
    {
      List<long> longList = new List<long>();
      if (bufferSize <= 0)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), (object) bufferSize, "Size of the file buffer must be greater than zero.");
      int num = int.MaxValue - (this._searchPattern.Length - 1);
      if (bufferSize > num)
        throw new ArgumentOutOfRangeException(nameof (bufferSize), (object) bufferSize, string.Format("Size of the file buffer ({0}) plus the size of the search pattern minus one ({1}) may not exceed Int32.MaxValue ({2}).", (object) bufferSize, (object) (this._searchPattern.Length - 1), (object) int.MaxValue));
      if (fileToSearch != null && fileToSearch.Exists)
      {
        using (FileStream fileStream = fileToSearch.OpenRead())
        {
          if (!fileStream.CanSeek)
            throw new Exception(string.Format("The file '{0}' is not seekable!  Search cannot be performed.", (object) fileToSearch));
          int chunkIndex = 0;
          while (true)
          {
            byte[] nextChunkForSearch = this.GetNextChunkForSearch((Stream) fileStream, chunkIndex, bufferSize);
            if (nextChunkForSearch != null)
            {
              if (((IEnumerable<byte>) nextChunkForSearch).Any<byte>())
              {
                List<long> matchIndexesInternal = this.GetMatchIndexes_Internal(nextChunkForSearch);
                if (matchIndexesInternal != null)
                {
                  int bufferOffset = bufferSize * chunkIndex;
                  longList.AddRange(matchIndexesInternal.Select<long, long>((System.Func<long, long>) (bufferMatchIndex => bufferMatchIndex + (long) bufferOffset)));
                }
                ++chunkIndex;
              }
              else
                break;
            }
            else
              break;
          }
        }
      }
      return new ReadOnlyCollection<long>((IList<long>) longList);
    }

    private long[] BuildBadCharacterShift(byte[] pattern)
    {
      long[] numArray = new long[256];
      long int64 = Convert.ToInt64(pattern.Length);
      for (long index = 0; index < Convert.ToInt64(numArray.Length); ++index)
        numArray[index] = int64;
      for (long index = 0; index < int64 - 1L; ++index)
        numArray[(int) pattern[index]] = int64 - index - 1L;
      return numArray;
    }

    private long[] FindSuffixes(byte[] pattern)
    {
      long num = 0;
      long int64 = Convert.ToInt64(pattern.Length);
      long[] numArray = new long[pattern.Length + 1];
      numArray[int64 - 1L] = int64;
      long index1 = int64 - 1L;
      for (long index2 = int64 - 2L; index2 >= 0L; --index2)
      {
        if (index2 > index1 && numArray[index2 + int64 - 1L - num] < index2 - index1)
        {
          numArray[index2] = numArray[index2 + int64 - 1L - num];
        }
        else
        {
          if (index2 < index1)
            index1 = index2;
          num = index2;
          while (index1 >= 0L && (int) pattern[index1] == (int) pattern[index1 + int64 - 1L - num])
            --index1;
          numArray[index2] = num - index1;
        }
      }
      return numArray;
    }

    private long[] BuildGoodSuffixShift(byte[] pattern, long[] suff)
    {
      long int64 = Convert.ToInt64(pattern.Length);
      long[] numArray = new long[pattern.Length + 1];
      for (long index = 0; index < int64; ++index)
        numArray[index] = int64;
      long index1 = 0;
      for (long index2 = int64 - 1L; index2 >= -1L; --index2)
      {
        if (index2 == -1L || suff[index2] == index2 + 1L)
        {
          for (; index1 < int64 - 1L - index2; ++index1)
          {
            if (numArray[index1] == int64)
              numArray[index1] = int64 - 1L - index2;
          }
        }
      }
      for (long index2 = 0; index2 <= int64 - 2L; ++index2)
        numArray[int64 - 1L - suff[index2]] = int64 - 1L - index2;
      return numArray;
    }

    private byte[] GetNextChunkForSearch(Stream stream, int chunkIndex, int fileSearchBufferSize)
    {
      byte[] numArray = (byte[]) null;
      long offset = Convert.ToInt64(chunkIndex) * Convert.ToInt64(fileSearchBufferSize);
      if (offset < stream.Length)
      {
        stream.Seek(offset, SeekOrigin.Begin);
        int length = this._searchPattern.Length;
        int count = fileSearchBufferSize + (length - 1);
        byte[] buffer = new byte[count];
        long int64 = Convert.ToInt64(stream.Read(buffer, 0, count));
        if (int64 >= (long) length)
        {
          if (int64 < (long) count)
          {
            numArray = new byte[int64];
            Array.Copy((Array) buffer, (Array) numArray, int64);
          }
          else
            numArray = buffer;
        }
      }
      return numArray;
    }

    private List<long> GetMatchIndexes_Internal(byte[] dataToSearch)
    {
      List<long> longList = new List<long>();
      if (dataToSearch == null)
        throw new ArgumentNullException(nameof (dataToSearch));
      long int64_1 = Convert.ToInt64(this._searchPattern.Length);
      long int64_2 = Convert.ToInt64(dataToSearch.Length);
      long num = 0;
      while (num <= int64_2 - int64_1)
      {
        long index = int64_1 - 1L;
        while (index >= 0L && (int) this._searchPattern[index] == (int) dataToSearch[index + num])
          --index;
        if (index < 0L)
        {
          longList.Add(num);
          num += this._goodSuffixShift[0];
        }
        else
          num += Math.Max(this._goodSuffixShift[index], this._badCharacterShift[(int) dataToSearch[index + num]] - int64_1 + 1L + index);
      }
      return longList;
    }
  }
}
