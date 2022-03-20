using MSQBot_API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSQBot_API.Core.DTOs
{

    /// <summary>
    /// Front representation of a movie
    /// </summary>
    public class MovieDto : IMovie
    {
        public int MovieId { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Poster { get; init; } = string.Empty ;
        public DateTime AddedDate { get; init; }
        public DateTime? SeenDate { get; init; }
    }
}
