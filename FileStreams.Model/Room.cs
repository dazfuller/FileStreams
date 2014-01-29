using System.Collections.Generic;

namespace FileStreams.Model
{
    /// <summary>
    /// Describes a room at a hotel
    /// </summary>
    public class Room : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        public Room()
        {
            Photos = new List<Photo>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the occupancy.
        /// </summary>
        /// <value>
        /// The occupancy.
        /// </value>
        public int Occupancy { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int HotelId { get; set; }

        /// <summary>
        /// Gets or sets the hotel.
        /// </summary>
        /// <value>
        /// The hotel.
        /// </value>
        public Hotel Hotel { get; set; }

        /// <summary>
        /// Gets or sets the photos.
        /// </summary>
        /// <value>
        /// The photos.
        /// </value>
        public IList<Photo> Photos { get; set; } 
    }
}
