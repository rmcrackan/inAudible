// Decompiled with JetBrains decompiler
// Type: AudibleConvertor.BatchFiles
// Assembly: inAudible, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F68AEA32-E028-47D6-9B0C-D1B7379EFC06
// Assembly location: C:\Program Files (x86)\inAudible197\inAudible.exe

using Inwards;
using System.IO;

namespace AudibleConvertor
{
  public class BatchFiles
  {
    public string[] inputFiles = new string[1]{ "" };
    public string outputPath = "";
    public int maxFileLength = 250;
    public string rootPath = "";
    public string extensionName = "mp3";
    public bool safeToMerge = true;
    public string[] outputFiles;
    public bool[] badPaths;
    public Audible[] myAudibles;
    public bool authorDir;
    public bool titleDir;
    public bool bothDirs;
    public bool fileExtension;
    private bool createNewAudibles;
    public bool oneOff;

    internal bool IsBatch()
    {
      return this.inputFiles.Length > 1;
    }

    public bool AlreadyEdited { get; set; }

    public bool HasErrors { get; set; }

    public void AddInputFiles(string[] input)
    {
      this.oneOff = false;
      this.inputFiles = new string[input.Length];
      for (int index = 0; index < input.Length; ++index)
        this.inputFiles[index] = input[index];
      if (input.Length == 1)
        this.oneOff = true;
      this.outputPath = "";
      this.myAudibles = (Audible[]) null;
      this.rootPath = "";
      this.createNewAudibles = false;
    }

    internal bool ValidateBatch(bool author, bool title, bool both, bool extension, string fileType, bool stripUnabridged)
    {
      this.outputFiles = new string[this.inputFiles.Length];
      this.badPaths = new bool[this.inputFiles.Length];
      this.authorDir = author;
      this.titleDir = title;
      this.bothDirs = both;
      this.fileExtension = extension;
      this.extensionName = fileType;
      this.rootPath = Path.GetDirectoryName(this.outputPath);
      bool flag = true;
      for (int position = 0; position < this.inputFiles.Length; ++position)
      {
        this.outputFiles[position] = this.GenerateOutputFilepath(position, this.inputFiles[position], Path.GetDirectoryName(this.outputPath), author, title, both, extension, fileType, stripUnabridged);
        int length = this.outputFiles[position].Length;
        if (length > this.maxFileLength)
        {
          Audible.diskLogger(this.outputFiles[position] + " is too long: " + (object) length + " characters.");
          this.badPaths[position] = true;
          flag = false;
          this.safeToMerge = false;
        }
        else
          this.badPaths[position] = false;
      }
      if (Utilities.CountDuplicates(this.outputFiles) > 0)
        flag = false;
      return flag;
    }

    private string GenerateOutputFilepath(int position, string inputFile, string outputDir, bool author, bool title, bool both, bool extension, string fileType, bool stripUnabridged)
    {
      string str = outputDir;
      Audible audible = new Audible();
      if (this.myAudibles != null && !this.createNewAudibles)
        audible = this.myAudibles[position];
      else if (this.myAudibles == null)
      {
        this.createNewAudibles = true;
        this.myAudibles = new Audible[this.inputFiles.Length];
        audible.GetMetaDataWithAtomicParsley(inputFile);
        if (audible.title == "")
          audible.GetCustomAAXMetaData(inputFile);
        this.myAudibles[0] = audible;
      }
      else
      {
        audible.GetMetaDataWithAtomicParsley(inputFile);
        if (audible.title == "")
          audible.GetCustomAAXMetaData(inputFile);
        this.myAudibles[position] = audible;
      }
      if (stripUnabridged)
      {
        this.myAudibles[position].title = this.myAudibles[position].title.Replace(" (Unabridged)", "");
        this.myAudibles[position].album = this.myAudibles[position].album.Replace(" (Unabridged)", "");
      }
      if (author)
        str = str + "\\" + this.StripInvalidCharacters(audible.author);
      if (title)
        str = str + "\\" + this.StripInvalidCharacters(audible.title);
      if (both)
        str = str + "\\" + this.StripInvalidCharacters(audible.author) + " - " + this.StripInvalidCharacters(audible.title);
      if (extension)
        str = str + "." + fileType.ToUpper();

      if (oneOff && !string.IsNullOrWhiteSpace(outputPath))
        return str + "\\" + Path.GetFileNameWithoutExtension(outputPath) + "." + fileType;

      return str + "\\" + this.StripInvalidCharacters(audible.title) + "." + fileType;
    }

    private string StripInvalidCharacters(string input)
    {
      if (input == null)
        return "";
      foreach (char ch in new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()))
        input = input.Replace(ch.ToString(), "");
      return input;
    }
  }
}
