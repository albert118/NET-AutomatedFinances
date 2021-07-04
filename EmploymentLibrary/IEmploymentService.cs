using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace EmploymentLibrary
{
    public enum EmploymentDBs
    {
        Seek,
        Careerhub
    }

    public delegate IEmploymentService EmplyomentServiceResolver(int key);

    public interface IEmploymentService
    {
        List<IEmploymentDTO> QuickSearcher(string searchTerm, DateTime? lowerDateFilter = null, DateTime? upperDateFilter = null);

        List<IEmploymentDTO> BulkSearcher(List<string> searchTerms, DateTime? lowerDateFilter = null, DateTime? upperDateFilter = null);

        public HttpClient GetClient();

        public void SetClient(HttpClient value);

        public CookieContainer GetCookieContainer();

        public void SetCookieContainer(CookieContainer value);
    }
}
