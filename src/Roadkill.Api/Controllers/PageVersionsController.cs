﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roadkill.Api.Interfaces;
using Roadkill.Api.Models;
using Roadkill.Core.Models;
using Roadkill.Core.Repositories;

namespace Roadkill.Api.Controllers
{
	[Route("pageversions")]
	public class PageVersionsController : Controller, IPageVersionsService
	{
		private readonly IPageVersionRepository _pageVersionRepository;
		private readonly IPageVersionViewModelConverter _viewModelConverter;

		public PageVersionsController(IPageVersionRepository pageVersionRepository, IPageVersionViewModelConverter viewModelConverter)
		{
			_pageVersionRepository = pageVersionRepository;
			_viewModelConverter = viewModelConverter;
		}

		public async Task<PageVersionViewModel> Add(int pageId, string text, string author, DateTime? dateTime = null)
		{
			PageVersion pageVersion = await _pageVersionRepository.AddNewVersion(pageId, text, author, dateTime);

			return _viewModelConverter.ConvertToViewModel(pageVersion);
		}

		public async Task<PageVersionViewModel> GetById(Guid id)
		{
			PageVersion pageVersion = await _pageVersionRepository.GetById(id);

			return _viewModelConverter.ConvertToViewModel(pageVersion);
		}

		public async Task Delete(Guid id)
		{
			await _pageVersionRepository.DeleteVersion(id);
		}

		public Task Update(PageVersionViewModel version)
		{
			throw new NotImplementedException();
		}

		[Route("UpdateLinksToPage")]
		[HttpPost]
		public async Task UpdateLinksToPage(string oldTitle, string newTitle)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<PageVersionViewModel>> AllVersions()
		{
			IEnumerable<PageVersion> pageVersions = await _pageVersionRepository.AllVersions();
			return pageVersions.Select(_viewModelConverter.ConvertToViewModel);
		}

		public async Task<IEnumerable<PageVersionViewModel>> FindPageVersionsByPageId(int pageId)
		{
			IEnumerable<PageVersion> pageVersions = await _pageVersionRepository.FindPageVersionsByPageId(pageId);
			return pageVersions.Select(_viewModelConverter.ConvertToViewModel);
		}

		public async Task<IEnumerable<PageVersionViewModel>> FindPageVersionsByAuthor(string username)
		{
			IEnumerable<PageVersion> pageVersions = await _pageVersionRepository.FindPageVersionsByAuthor(username);
			return pageVersions.Select(_viewModelConverter.ConvertToViewModel);
		}

		[Route("GetLatestVersion")]
		[HttpGet]
		public async Task<PageVersionViewModel> GetLatestVersion(int pageId)
		{
			PageVersion latestPageVersion = await _pageVersionRepository.GetLatestVersion(pageId);
			if (latestPageVersion == null)
				return null;

			return _viewModelConverter.ConvertToViewModel(latestPageVersion);
		}
	}
}