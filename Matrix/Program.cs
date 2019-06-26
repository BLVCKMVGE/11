using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Matrix
{
	class Program
	{
		static StringBuilder GetString() {
			StringBuilder str = new StringBuilder("");
			StreamReader reader = new StreamReader("in.txt", Encoding.Default);
			string temp;
			while ((temp = reader.ReadLine()) != null) {
				str.Append(temp);
			}
			reader.Close();
			return str;
		}

		static StringBuilder Encode(StringBuilder str) {
			char[,] textMatrix = new char[11, 11];
			for (int m = 0; m < 11; m++) {
				for (int n = 0; n < 11; n++) {
					textMatrix[m, n] = str[11*m + n];
				}
			}

			StringBuilder encodedText = new StringBuilder();
			encodedText.Append(textMatrix[5, 5]);
			int i = 5;
			int j = 5;
			for (int k = 1; k < 11;) {
				for (int u = 0; u < k; u++)
					encodedText.Append(textMatrix[i, --j]);
				for (int u = 0; u < k; u++)
					encodedText.Append(textMatrix[++i, j]);
				k++;
				for (int u = 0; u < k; u++)
					encodedText.Append(textMatrix[i, ++j]);
				for (int u = 0; u < k; u++)
					encodedText.Append(textMatrix[--i, j]);
				k++;
			}

			for (int u = 0; u < 10; u++)
				encodedText.Append(textMatrix[i, --j]);

			return encodedText;
		}

		static StringBuilder Decode(StringBuilder text) {
			char[,] textMatrix = new char[11, 11];
			
			int i = 5;
			int j = 5;
			textMatrix[5, 5] = text[0];
			int textIter = 0;
			for (int k = 1; k < 11;) {
				for (int u = 0; u < k; u++)
					textMatrix[i, --j] = text[++textIter];
				for (int u = 0; u < k; u++)
					textMatrix[++i, j] = text[++textIter];
				k++;
				for (int u = 0; u < k; u++)
					textMatrix[i, ++j] = text[++textIter];
				for (int u = 0; u < k; u++)
					textMatrix[--i, j] = text[++textIter];
				k++;
			}

			for (int u = 0; u < 10; u++)
				textMatrix[i, --j] = text[++textIter];

			StringBuilder decodedText = new StringBuilder();
			for (int m = 0; m < 11; m++)
				for (int n = 0; n < 11; n++)
					decodedText.Append(textMatrix[m, n]);
			return decodedText;
		}

		static void Main(string[] args)
		{
			StringBuilder startText = GetString();
			StreamWriter writer = new StreamWriter("result.txt", false, Encoding.Default);
			StringBuilder encodedText = Encode(startText);
			writer.WriteLine(encodedText);
			StringBuilder decodedText = Decode(encodedText);
			writer.WriteLine(decodedText);
			writer.Close();
		}
	}
}
