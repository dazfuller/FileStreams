using System.Collections.Generic;

namespace FileStreams.Model
{
    /// <summary>
    /// An area of the world
    /// </summary>
    public class Location : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the hotels in the area.
        /// </summary>
        /// <value>
        /// The hotels.
        /// </value>
        public IList<Hotel> Hotels { get; set; }
    }
}
