using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CrossoverSemJournals.Domain.Infrastructure;

namespace CrossoverSemJournals.Infrastructure
{
	public class PaperFileConverter: IPaperFileConverter
	{
		public PaperFileConverter ()
		{
		}

		public List<byte []> Convert (byte [] pdfBytes)
		{
			List<byte []> result = new List<byte[]> ();

			string tmpPath = "tmp/";
			string pdfFilename = DateTime.Now.Ticks + ".pdf";
			string pdfFilepath = tmpPath + pdfFilename;
			if (!Directory.Exists (tmpPath)) Directory.CreateDirectory (tmpPath);
			File.WriteAllBytes (pdfFilepath, pdfBytes);

			var startInfo = new ProcessStartInfo ("convert", $"{pdfFilename} paper.jpg") { 
				WorkingDirectory = tmpPath,
				RedirectStandardError = true,
				UseShellExecute = false
			};
			try {
				using (Process proc = Process.Start (startInfo))
				{
					string error = proc.StandardError.ReadToEnd ();
					Console.WriteLine (error);
					proc.WaitForExit ();
				}
			} catch (Exception ex) {
				Console.WriteLine (ex);
				throw ex;
			}


			var imageFilenames = Directory.EnumerateFiles (tmpPath,"paper-*.jpg",SearchOption.TopDirectoryOnly);
			foreach (var imageFilename in imageFilenames) {
				result.Add (File.ReadAllBytes (imageFilename));
				//File.Delete (imageFilename);
			}

			//File.Delete (pdfFilename);

			return result;
		}
	}
}