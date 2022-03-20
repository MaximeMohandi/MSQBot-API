using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSQBot_API.Core.Interfaces
{
    public interface IMovie
    {
        /// <summary>
        /// Movie unique id
        /// </summary>
        public int MovieId { get; init; }

        /// <summary>
        /// movie title
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// Movie poster
        /// </summary>
        public string Poster { get; init; }

        /// <summary>
        /// The date when the movie has been added
        /// </summary>
        public DateTime AddedDate { get; init; }

        /// <summary>
        /// Date when the movie has been seen
        /// </summary>
        public DateTime? SeenDate { get; init; }
    }
}
