using System;
using System.Collections.Generic;
using CrossoverSemJournals.Domain.Entities;

namespace CrossoverSemJournals.Domain.Infrastructure
{
	public interface IPaperFileConverter
	{
		List<byte []> Convert (byte [] pdfBytes);
	}
}

