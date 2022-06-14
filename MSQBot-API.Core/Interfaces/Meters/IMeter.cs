namespace MSQBot_API.Core.Interfaces.Meters
{
    public interface IMeter
    {
        /// <summary>
        /// Meter unique id
        /// </summary>
        public int MeterId { get; init; }

        /// <summary>
        /// Meter Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Rules of the meter
        /// </summary>
        public string? Rules { get; set; }
    }
}
