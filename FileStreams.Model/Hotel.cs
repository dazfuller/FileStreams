using System.Collections.Generic;

namespace FileStreams.Model
{
    /// <summary>
    /// Describes a hotel
    /// </summary>
    public class Hotel : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hotel"/> class.
        /// </summary>
        public Hotel()
        {
            Rooms = new List<Room>();
        }

        /// <summary>
        /// Gets or sets the name of the hotel.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public Location Location { get; set; }

        /// <summary>
        /// Gets or sets the rooms.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        public IList<Room> Rooms { get; set; } 
    }
}
