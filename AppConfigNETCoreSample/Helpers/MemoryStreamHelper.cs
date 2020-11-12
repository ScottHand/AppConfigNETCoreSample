using System;
using System.IO;
using System.Text;

namespace AppConfigNETCoreSample.Helpers {
  public static class MemoryStreamHelper {
    public static string DecodeMemoryStreamToBase64String(MemoryStream content) {
      string result = string.Empty;
      int count;
      UnicodeEncoding uniEncoding = new UnicodeEncoding();

      using (MemoryStream memoryStream = content) {
        memoryStream.Seek(0, SeekOrigin.Begin);
        byte[] byteArray = new byte[memoryStream.Length];
        count = memoryStream.Read(byteArray, 0, 20);

        while (count < memoryStream.Length) {
          byteArray[count++] = Convert.ToByte(memoryStream.ReadByte());
        }

        char[] charArray = new char[uniEncoding.GetCharCount(byteArray, 0, count)];
        uniEncoding.GetDecoder().GetChars(byteArray, 0, count, charArray, 0);


        byte[] decodedBytes = Convert.FromBase64String(Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(charArray)));
        result = System.Text.Encoding.UTF8.GetString(decodedBytes);
      }
      return result;
    }
  }
}
