﻿using System.Collections.Generic;
        
namespace StackOverflow.Models
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> items, int totalPages, int pageNumber)
        {
            this.AddRange(items);
            this.TotalPages = totalPages;
            this.PageNumber = pageNumber;
        }

        public bool HasPrevPage
        {
            get
            {
                return this.PageNumber > 1;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return this.PageNumber < this.TotalPages;
            }
        }
        public int TotalPages { get; }
        public int PageNumber { get; }
    }
}
