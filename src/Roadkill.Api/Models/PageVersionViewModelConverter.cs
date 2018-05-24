﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using Roadkill.Core.Models;

namespace Roadkill.Api.Models
{
	public interface IPageVersionViewModelConverter
	{
		PageVersionViewModel ConvertToViewModel(PageVersion pageVersion);
	}

	public class PageVersionViewModelConverter : IPageVersionViewModelConverter
	{
		public PageVersionViewModel ConvertToViewModel(PageVersion pageVersion)
		{
			return new PageVersionViewModel()
			{
				Id = pageVersion.Id,
				Text = pageVersion.Text,
				DateTime = pageVersion.DateTime,
				Author = pageVersion.Author,
				PageId = pageVersion.PageId
			};
		}
	}
}